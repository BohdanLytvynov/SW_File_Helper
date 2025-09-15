using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.BL.Net.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessorWrappers.Base;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace SW_File_Helper.BL.Net.TCPListeners
{
    public class TCPListener : TCPBase<TcpListener>, ITCPListener
    {
        private Task m_listenerTask;
        private CancellationTokenSource m_cancellationTokenSource;
        
        public IPEndPoint Endpoint { get; set; }
        public INetworkStreamProcessorWrapper NetworkStreamProcessor { get; set; }

        public TCPListener(ILogger logger, INetworkStreamProcessorWrapper networkStreamProcessor, 
            string clientName)
            : base(logger, clientName)
        {
            NetworkStreamProcessor = networkStreamProcessor ?? throw new NullReferenceException(nameof(networkStreamProcessor));
        }

        public override void Dispose()
        {
            var instance = GetInstance();

            if (instance == null)
            {
                Logger.Error($"Instance {ClientName} wasn't initialized!");
            }

            try
            {
                Logger.Info($"Releasing {ClientName} resources...");
                instance = GetInstance();
                if (instance != null)
                {
                    instance.Dispose();
                    instance = null;
                    Logger.Ok($"{ClientName} resources released...");
                }
            }
            catch (Exception ex)
            {
                Logger.Info($"Error on releasing {ClientName} resources! Error: {ex}");
            }
        }

        public override void Init()
        {
            Logger.Info($"Initializing {ClientName}...");
            var instance = GetInstance();

            if (instance == null)
            {
                try
                {
                    instance = new TcpListener(Endpoint);
                    SetInstance(instance);
                    Logger.Ok($"{ClientName} initialized.");
                }
                catch (Exception ex)
                {
                    Logger.Error($"Error on initialization of the {ClientName}! Error: {ex}");
                }
                
            }

            m_listenerTask = new Task(StartListening);
            m_cancellationTokenSource = new CancellationTokenSource();
            
        }

        public void Start()
        {
            try
            {
                Logger.Info($"Starting {ClientName}...");

                var instance = GetInstance();

                instance.Start();

                Logger.Ok($"{ClientName} started... \n{ClientName} listening to {Endpoint.Port} Port");

                m_listenerTask.Start();
            }
            catch (Exception ex)
            {
                Logger.Error($"Error on Starting {ClientName}! Error: {ex}");
            }
        }

        public void Stop()
        {
            Logger.Info("Stop command recieved");

            if(m_cancellationTokenSource != null)
                m_cancellationTokenSource.Cancel();
            
            Dispose();
        }

        private void StartListening()
        {
            NetworkStream? netStream = null;
            TcpClient? sender = null;
            var instance = GetInstance();
            try
            {
                while (true)
                {
                    if (m_cancellationTokenSource.Token.IsCancellationRequested)
                        break;

                    sender = instance.AcceptTcpClient();

                    if (sender == null) continue;

                    var senderIP = (sender.Client.RemoteEndPoint as IPEndPoint).Address;

                    Logger.Info($"Connection established with: {senderIP}");

                    netStream = sender.GetStream();

                    if (netStream == null)
                    {
                        Logger.Warn("Connection established with client, but there was no network stream!");
                        continue;
                    }

                    NetworkStreamProcessor.ProcessNetworkStream(netStream, senderIP.ToString());

                    sender.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Error during recieving Data! Error: {ex}");
            }
            finally 
            {
                Logger.Info("Releasing client sender resources...");
                sender?.Close();
                sender = null;
                Logger.Info("Client sender resources released...");
            }
        }
    }
}

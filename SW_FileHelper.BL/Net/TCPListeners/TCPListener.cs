using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.BL.Net.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.Base;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace SW_File_Helper.BL.Net.TCPListeners
{
    public class TCPListener<TNetworkStreamProcessor> : TCPBase<TcpListener>,
        ITCPListener<TNetworkStreamProcessor>
        where TNetworkStreamProcessor : INetworkStreamProcessor
    {
        private Task m_listenerTask;
        private CancellationTokenSource m_cancellationTokenSource;
        
        public IPEndPoint Endpoint { get; set; }
        public TNetworkStreamProcessor NetworkStreamProcessor { get; set; }

        public TCPListener(ILogger logger) : base(logger)
        {
            
        }

        public override void Dispose()
        {
            var instance = GetInstance();
            if (instance != null)
            {
                instance.Dispose();
            }
        }

        public override void Init()
        {
            var instance = GetInstance();

            if (instance == null)
            {
                instance = new TcpListener(Endpoint);
                SetInstance(instance);
            }

            m_listenerTask = new Task(StartListening);
            m_cancellationTokenSource = new CancellationTokenSource();
        }

        public void Start()
        {
            try
            {
                Logger.Info("Starting Server...");

                var instance = GetInstance();

                instance.Start();

                Logger.Info($"Server started... \nServer listening to {Endpoint.Address.ToString()} ...");

                m_listenerTask.Start();
            }
            catch (Exception ex)
            {
                Logger.Error($"Error on Starting TCP Listener! Error: {ex}");
            }
        }

        public void Stop()
        {
            Logger.Info("Stop command recieved");

            if(m_cancellationTokenSource != null)
                m_cancellationTokenSource.Cancel();
            try
            {
                Logger.Info("Releasing server resources...");
                var instance = GetInstance();
                instance.Dispose();
                instance = null;
                Logger.Info("Server resources released...");
            }
            catch (Exception ex)
            {
                Logger.Info($"Error on releasing server resources! Error: {ex}");
            }
            
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

                    Logger.Info($"Connection established with: {(sender.Client.RemoteEndPoint as IPEndPoint).Address}");

                    netStream = sender.GetStream();

                    if (netStream == null)
                    {
                        Logger.Warn("Connection established with client, but there was no network stream!");
                        continue;
                    }

                    Logger.Info($"NetworkStream with {netStream.Length} recieved...");

                    NetworkStreamProcessor.ProcessNetworkStream(netStream);
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
                Logger.Info("Resources released...");
            }
        }
    }
}

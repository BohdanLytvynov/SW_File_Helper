using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.BL.Net.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.Base;
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

            if (instance == null)
            {
                Logger.Error($"Instance wasn't initialized!");
            }

            try
            {
                Logger.Info("Releasing server resources...");
                instance = GetInstance();
                instance.Dispose();
                instance = null;
                Logger.Ok("Server resources released...");
            }
            catch (Exception ex)
            {
                Logger.Info($"Error on releasing server resources! Error: {ex}");
            }
        }

        public override void Init()
        {
            Logger.Info("Initializing Server...");
            var instance = GetInstance();

            if (instance == null)
            {
                instance = new TcpListener(Endpoint);
                SetInstance(instance);
            }

            m_listenerTask = new Task(StartListening);
            m_cancellationTokenSource = new CancellationTokenSource();
            Logger.Ok("Server initialized.");
        }

        public void Start()
        {
            try
            {
                Logger.Info("Starting Server...");

                var instance = GetInstance();

                instance.Start();

                Logger.Ok($"Server started... \nServer listening to {Endpoint.Address.ToString()}:{Endpoint.Port}");

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

                    Logger.Info($"NetworkStream with {netStream.Length} recieved...");

                    NetworkStreamProcessor.ProcessNetworkStream(netStream, senderIP.Address.ToString());
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

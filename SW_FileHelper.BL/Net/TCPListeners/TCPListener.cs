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
                instance = new TcpListener(Endpoint);

            m_listenerTask = new Task(StartListening);
            m_cancellationTokenSource = new CancellationTokenSource();
        }

        public void Start()
        {
            try
            {
                var instance = GetInstance();

                instance.Start();

                m_listenerTask.Start();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error on Starting TCP Listener! Error: {ex}");
            }
        }

        public void Stop()
        {
            if(m_cancellationTokenSource != null)
                m_cancellationTokenSource.Cancel();

            var instance = GetInstance();
            instance.Dispose();
            instance = null;
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

                    netStream = sender.GetStream();

                    if(netStream == null) continue;

                    NetworkStreamProcessor.ProcessNetworkStream(netStream);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during recieving Data! Error: {ex}");
            }
            finally 
            {
                sender?.Close();
                sender = null;
            }
        }
    }
}

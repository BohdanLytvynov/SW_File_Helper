using Newtonsoft.Json.Linq;
using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.BL.Net.Base;
using System.Net.Sockets;
using System.Text;

namespace SW_File_Helper.BL.Net.TCPClients
{
    public sealed class TCPClient : TCPBase<TcpClient>, ITCPClient
    {
        #region Properties
        public int SendingBufferSize { get; set; }
        #endregion

        #region Ctor
        public TCPClient(ILogger logger) : base(logger)
        {
        }
        #endregion

        #region Methods

        public override void Dispose()
        {
            var instance = GetInstance();
            if (instance != null)
            {
                instance.Close();
            }
        }

        public override void Init()
        {
            Logger.Info("Client Initizlization started...");

            var instance = GetInstance();

            if (instance == null)
            {
                instance = new TcpClient(Endpoint);
                SetInstance(instance);
            }

            if (SendingBufferSize == 0)
                SendingBufferSize = 1024;

            Logger.Info($"Sending Buffer size was set to: {SendingBufferSize} Bytes");
        }

        public async Task SendFileAsync(FileStream fs)
        {
            var tcpInstance = GetInstance();
            if (tcpInstance == null)
            {
                throw new Exception("Tcp Instance wasn't initialized!");
            }
        }

        public void SendMessage(string msg)
        {
            var tcpInstance = GetInstance();
            if (tcpInstance == null)
            {
                Logger.Error("Tcp instance wasn't initialized!");
            }

            try
            {
                Logger.Info($"Sending message: {msg}");

                var networkStream = tcpInstance.GetStream();
                var bytesToSend = Encoding.UTF8.GetBytes(msg);

                Logger.Info($"Calculated amount of Bytes to send: {bytesToSend} Bytes");

                networkStream.Write(bytesToSend, 0, bytesToSend.Length);

                Logger.Info("Message was send successfuly.");
            }
            catch (Exception ex)
            {
                Logger.Error($"Error occured during message sending! Error: {ex}");
            }
        }

        public async Task SendMessageAsync(string msg)
        {
            var tcpInstance = GetInstance();
            if (tcpInstance == null)
            {
                Logger.Error("Tcp instance wasn't initialized!");
            }

            try
            {
                Logger.Info($"Sending message: {msg}");

                var networkStream = tcpInstance.GetStream();
                var bytesToSend = Encoding.UTF8.GetBytes(msg);

                Logger.Info($"Calculated amount of Bytes to send: {bytesToSend} Bytes");

                await networkStream.WriteAsync(bytesToSend, 0, bytesToSend.Length);

                Logger.Info("Message was send successfuly.");
            }
            catch (Exception ex)
            {
                Logger.Error($"Error occured during message sending! Error: {ex}");
            }
        }

        #endregion
    }
}

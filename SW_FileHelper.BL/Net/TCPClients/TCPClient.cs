using Newtonsoft.Json.Linq;
using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.BL.Net.Base;
using SW_File_Helper.DAL.Helpers;
using SW_File_Helper.DAL.Models.TCPModels;
using System.Net.Sockets;
using System.Text;

namespace SW_File_Helper.BL.Net.TCPClients
{
    public sealed class TCPClient : TCPBase<TcpClient>, ITCPClient
    {
        #region Properties
        public int SendingBufferSize { get; set; }
        public string ClientName { get; init; }
        #endregion

        #region Ctor
        public TCPClient(ILogger logger, string clientName = "My Client") : base(logger)
        {
            ClientName = clientName;
        }
        #endregion

        #region Methods

        public override void Dispose()
        {
            this.Logger.Info($"Releasing {ClientName} resources...");

            var instance = GetInstance();
            if (instance != null)
            {
                try
                {
                    instance.Close();

                    SetInstance(null);

                    this.Logger.Ok($"{ClientName} Resources released.");
                }
                catch (Exception ex)
                {
                    this.Logger.Error($"Error while releasing {ClientName} Resources! Error: {ex}");
                }
            }
        }

        public override void Init()
        {
            Logger.Info($"{ClientName} Initialization started...");

            var instance = GetInstance();

            if (instance == null)
            {
                try
                {
                    instance = new TcpClient();
                    instance.Connect(Endpoint);
                    SetInstance(instance);
                    Logger.Ok($"{ClientName} initialized successfuly.");
                }
                catch (Exception ex)
                {
                    Logger.Error($"Error on initialization of the {ClientName}! Error: {ex}");
                }                
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
                throw new Exception($"Tcp Instance of the {ClientName} wasn't initialized!");
            }
        }

        public void SendMessage(string msg)
        {
            var tcpInstance = GetInstance();
            if (tcpInstance == null)
            {
                Logger.Error($"Tcp instance of the {ClientName} wasn't initialized!");
            }

            try
            {
                Logger.Info($"Sending message: {msg}");

                var message = new Message() { Text = msg };

                var jsonStr = JsonHelper.SerializeWithNonFormatting(message);

                var networkStream = tcpInstance.GetStream();
                var bytesToSend = Encoding.UTF8.GetBytes(jsonStr);

                Logger.Info($"Calculated amount of Bytes to send: {bytesToSend.Length} Bytes");

                networkStream.Write(bytesToSend, 0, bytesToSend.Length);

                Logger.Ok("Message was send successfuly.");
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
                Logger.Error($"Tcp instance of the {ClientName} wasn't initialized!");
            }

            try
            {
                Logger.Info($"Sending message: {msg}");

                var networkStream = tcpInstance.GetStream();
                var bytesToSend = Encoding.UTF8.GetBytes(msg);

                Logger.Info($"Calculated amount of Bytes to send: {bytesToSend.Length} Bytes");

                await networkStream.WriteAsync(bytesToSend, 0, bytesToSend.Length);

                Logger.Ok("Message was send successfuly.");
            }
            catch (Exception ex)
            {
                Logger.Error($"Error occured during message sending! Error: {ex}");
            }
        }

        #endregion
    }
}

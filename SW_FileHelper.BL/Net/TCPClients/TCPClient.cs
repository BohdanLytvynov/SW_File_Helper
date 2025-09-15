using Newtonsoft.Json.Linq;
using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.BL.Net.Base;
using SW_File_Helper.DAL.Helpers;
using SW_File_Helper.DAL.Models.TCPModels;
using SW_File_Helper.DAL.Models.TCPModels.Enums;
using SW_File_Helper.DAL.Models.TCPModels.FileMetadata;
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
        public TCPClient(ILogger logger, string clientName) : base(logger, clientName)
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

            Logger.Info($"Sending Buffer size for {ClientName} was set to: {SendingBufferSize} Bytes");
        }

        public async Task SendFileAsync(FileStream fs)
        {
            var tcpInstance = GetInstance();
            if (tcpInstance == null)
            {
                throw new Exception($"Tcp Instance of the {ClientName} wasn't initialized!");
            }
        }

        public void SendCommand(Command command)
        {
            byte[] sendBuffer = null;
            var tcpInstance = GetInstance();
            if (tcpInstance == null)
            {
                Logger.Error($"Tcp instance of the {ClientName} wasn't initialized!");
            }

            try
            {
                Logger.Info($"Sending {command.CommandType} to the Listener");
                var networkStream = tcpInstance.GetStream();

                if (networkStream == null)
                {
                    Logger.Error($"Error on getting NetworkStream!");
                    return;
                }
                SendMessageType(MessageType.Command, networkStream);

                string commandStr = JsonHelper.SerializeWithNonFormatting(command);
                sendBuffer = Encoding.UTF8.GetBytes(commandStr);
                
                SendMessageSize(sendBuffer, networkStream);

                networkStream.Write(sendBuffer, 0, sendBuffer.Length);
                Logger.Ok($"{command.CommandType} was sent successfuly.");
            }
            catch (Exception ex)
            {
                Logger.Error($"Error occured during message sending! Error: {ex}");
            }
        }

        public void SendMessage(string msg)
        {

            byte[] sendBuffer = null;
            var tcpInstance = GetInstance();
            if (tcpInstance == null)
            {
                Logger.Error($"Tcp instance of the {ClientName} wasn't initialized!");
            }
            var b = tcpInstance.Connected;
            try
            {
                Logger.Info($"Sending message: {msg}");
                var networkStream = tcpInstance.GetStream();

                if (networkStream == null)
                {
                    Logger.Error($"Error on getting NetworkStream!");
                    return;
                }
                SendMessageType(MessageType.Message, networkStream);

                sendBuffer = Encoding.UTF8.GetBytes(msg);
                SendMessageSize(sendBuffer, networkStream);

                networkStream.Write(sendBuffer, 0, sendBuffer.Length);
                Logger.Ok($"Message: {msg} was sent successfuly.");
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

        public void SendFile(string path)
        {
            byte[] sendingBuffer = null;
            try
            {
                var instance = GetInstance();
                if (instance == null)
                {
                    Logger.Error("TcpClient instance wasn't initialized!");
                    return;
                }

                using (var fs = File.Open(path, FileMode.Open, FileAccess.Read))
                {
                    var networkStream = instance.GetStream();

                    if (instance == null)
                    {
                        Logger.Error("Failed to get NetworkStream!");
                        return;
                    }

                    SendMessageType(MessageType.File, networkStream);
                    
                    long fileSize = fs.Length;
                    string fileName = Path.GetFileName(path);
                    int NoOfPackets = CalculateAmountOfPackets(fileSize, SendingBufferSize);
                    Logger.Info($"Sending {fileName} to the client.");
                    Logger.Info($"Calculated amount of packets: {NoOfPackets}");
                    FileMetadata fileMetadata = new FileMetadata(fileSize, fileName, NoOfPackets);
                    string jsonFileMetadata = JsonHelper.Serialize(fileMetadata);
                    sendingBuffer = Encoding.UTF8.GetBytes(jsonFileMetadata);

                    SendMessageSize(sendingBuffer, networkStream);
                    networkStream.Write(sendingBuffer, 0, sendingBuffer.Length);
                    int position = 0;
                    //Send file 
                    for (int i = 0; i < NoOfPackets; i++)
                    {
                        sendingBuffer = new byte[SendingBufferSize];
                        fs.Read(sendingBuffer, 0, sendingBuffer.Length);
                        networkStream.Write(sendingBuffer, 0, sendingBuffer.Length);
                        position += SendingBufferSize;
                        fs.Position = position;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Error happened during sending file! Error: {ex}");
            }
        }

        private int CalculateAmountOfPackets(long fileLength, int bufferSize)
        {
            return Convert.ToInt32(Math.Ceiling(Convert.ToDouble(fileLength) / Convert.ToDouble(bufferSize)));
        }

        private void SendMessageType(MessageType type, NetworkStream networkStream)
        {
            int typeInt = (int)type;
            byte[] sendBuffer = BitConverter.GetBytes(typeInt);
            networkStream.Write(sendBuffer, 0, sendBuffer.Length);
        }

        private void SendMessageSize(byte[] buffer, NetworkStream networkStream)
        {
            byte[] sendBuffer = BitConverter.GetBytes(buffer.Length);
            networkStream.Write(sendBuffer, 0, sendBuffer.Length);
        }

        #endregion
    }
}

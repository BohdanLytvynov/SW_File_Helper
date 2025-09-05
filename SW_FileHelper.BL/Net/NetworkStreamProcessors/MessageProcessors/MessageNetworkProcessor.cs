using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.BL.Net.MessageProcessors.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.Base;
using System.Net.Sockets;
using System.Text;

namespace SW_File_Helper.BL.Net.NetworkStreamProcessors.MessageProcessors
{
    public sealed class MessageNetworkProcessor : NetworkProcessorBase, IMessageNetworkProcessor
    {
        private IMessageProcessor m_messageProcessor;
        public IMessageProcessor MessageProcessor 
        { get => m_messageProcessor; set => m_messageProcessor = value; }

        public MessageNetworkProcessor(IMessageProcessor messageProcessor, ILogger logger) : this()
        {
            if (messageProcessor == null)
                throw new ArgumentNullException(nameof(messageProcessor));

            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            m_messageProcessor = messageProcessor;

            Logger = logger;
        }

        public MessageNetworkProcessor() : base()
        {
            
        }

        public override void ProcessNetworkStream(NetworkStream networkStream, string clientIp)
        {
            string dataRecieved = string.Empty;

            byte[] recieveBuffer = new byte[BufferSize];

            int bytesRead = 0;

            try
            {
                Logger.Info("Starting NetworkStream processing...");
                
                Logger.Info($"Recieve Buffer allocated, size: {BufferSize}");

                bytesRead = networkStream.Read(recieveBuffer, 0, BufferSize);

                if (bytesRead > 0)
                {
                    dataRecieved = UTF8Encoding.UTF8.GetString(recieveBuffer);

                    Logger.Ok($"{bytesRead} Bytes was read from the NetworkStream.");
                }
                else
                {
                    Logger.Error("Unable to read Data from the NetworkStream!");
                }

                if (string.IsNullOrEmpty(dataRecieved))
                    Logger.Error("Unable to Cast read bytes To String");

                MessageProcessor.ProcessMessage(dataRecieved, clientIp);
            }
            catch (Exception ex)
            {
                Logger.Error($"Error during Network Stream Processing occured! Error: {ex}");
                throw;
            }
        }
    }
}

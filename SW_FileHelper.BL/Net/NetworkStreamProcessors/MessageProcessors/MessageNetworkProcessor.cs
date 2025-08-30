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

        public MessageNetworkProcessor(IMessageProcessor messageProcessor) : this()
        {
            if (messageProcessor == null)
                throw new ArgumentNullException(nameof(messageProcessor));

            m_messageProcessor = messageProcessor;
        }

        public MessageNetworkProcessor() : base()
        {
            
        }

        public override void ProcessNetworkStream(NetworkStream networkStream)
        {
            string dataRecieved = string.Empty;

            byte[] recieveBuffer = new byte[BufferSize];

            int bytesRead = networkStream.Read(recieveBuffer, 0, BufferSize);

            if (bytesRead > 0)
            {
                dataRecieved = UTF8Encoding.UTF8.GetString(recieveBuffer);
            }
            else
            {
                throw new Exception("Unable to read Data!");
            }

            if (string.IsNullOrEmpty(dataRecieved))
                throw new Exception("Unable to Cast Data To String");

            MessageProcessor.ProcessMessage(dataRecieved);
        }
    }
}

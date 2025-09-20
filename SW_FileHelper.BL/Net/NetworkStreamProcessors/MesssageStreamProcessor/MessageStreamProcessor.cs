using SW_File_Helper.BL.Extensions.NetworkStreams;
using SW_File_Helper.BL.Net.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.Base;
using SW_File_Helper.DAL.Models.TCPModels.Enums;
using System.Net.Sockets;
using System.Text;

namespace SW_File_Helper.BL.Net.NetworkStreamProcessors.MesssageStreamProcessor
{
    public class MessageStreamProcessor : NetworkStreamProcessorBase, IMessageStreamProcessor
    {
        public OnProcessed<string>? OnProcess { get; set; }

        public MessageStreamProcessor()
        {
            MessageType = MessageType.Message;
        }

        public override void Process(MessageType type, NetworkStream networkStream, string clientIp)
        {
            base.Process(type, networkStream, clientIp);

            int messageSize = networkStream.ReadMessageSize();
            
            byte[] buffer = new byte[messageSize];

            networkStream.ReadBytes(messageSize, buffer);

            string message = Encoding.UTF8.GetString(buffer);
            OnProcess?.Invoke(message, clientIp);
        }
    }
}

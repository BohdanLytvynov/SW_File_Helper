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

        public override void Process(MessageType type, NetworkStream networkStream, string clientIp)
        {
            base.Process(type, networkStream, clientIp);

            int messageSize = GetDataSize(networkStream);
            
            byte[] buffer = new byte[messageSize];

            ReadBytes(networkStream, messageSize, buffer);

            string message = Encoding.UTF8.GetString(buffer);
            OnProcess?.Invoke(message, clientIp);
        }
    }
}

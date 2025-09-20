using Newtonsoft.Json.Linq;
using SW_File_Helper.BL.Extensions.NetworkStreams;
using SW_File_Helper.BL.Net.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.Base;
using SW_File_Helper.DAL.Models.TCPModels.Enums;
using System.Net.Sockets;
using System.Text;

namespace SW_File_Helper.BL.Net.NetworkStreamProcessors.CommandStreamProcessors
{
    public class CommandStreamProcessor : NetworkStreamProcessorBase, ICommandStreamProcessor
    {
        public OnProcessed<JObject>? OnProcess { get; set; }

        public CommandStreamProcessor()
        {
            MessageType = MessageType.Command;
        }

        public override void Process(MessageType type, NetworkStream networkStream, string clientIp)
        {
            base.Process(type, networkStream, clientIp);

            int messageSize = networkStream.ReadMessageSize();
            byte[] buffer = new byte[messageSize];

            networkStream.ReadBytes(messageSize, buffer);

            OnProcess?.Invoke(JObject.Parse(Encoding.UTF8.GetString(buffer)), clientIp);
        }
    }
}

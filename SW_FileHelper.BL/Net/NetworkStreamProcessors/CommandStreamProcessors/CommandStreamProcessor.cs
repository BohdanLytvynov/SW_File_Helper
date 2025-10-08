using Newtonsoft.Json.Linq;
using SW_File_Helper.BL.Extensions.NetworkStreams;
using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.BL.Net.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.Base;
using SW_File_Helper.DAL.Models.TCPModels.Enums;
using System.Net.Sockets;
using System.Text;

namespace SW_File_Helper.BL.Net.NetworkStreamProcessors.CommandStreamProcessors
{
    public class CommandStreamProcessor : NetworkStreamProcessorBase, ICommandStreamProcessor
    {
        public OnProcessed<string>? OnProcess { get; set; }

        public CommandStreamProcessor(ILogger logger) : base(logger)
        {
            MessageType = MessageType.Command;
        }

        public override void Process(MessageType type, NetworkStream networkStream, string clientIp)
        {
            base.Process(type, networkStream, clientIp);
            if (!m_processed)
            {
                int messageSize = networkStream.ReadMessageSize();
                byte[] buffer = new byte[messageSize];

                networkStream.ReadBytes(messageSize, buffer);
                OnProcess?.Invoke(Encoding.UTF8.GetString(buffer), clientIp);
                m_processed = true;
            }
        }
    }
}

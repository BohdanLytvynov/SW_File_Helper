using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.DAL.Models.TCPModels.Enums;
using System.Net.Sockets;

namespace SW_File_Helper.BL.Net.NetworkStreamProcessors.Base
{
    public abstract class NetworkStreamProcessorBase : INetworkStreamProcessor
    {
        public INetworkStreamProcessor Next { get; set; }
        public MessageType MessageType { get; init; }

        public ILogger Logger { get; set; }

        protected NetworkStreamProcessorBase(ILogger logger)
        {
            MessageType = MessageType.None;

            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual void Process(MessageType type, NetworkStream networkStream, string clientIp)
        {
            if(MessageType != type)
                Next?.Process(type, networkStream, clientIp);
            return;
        }
    }
}

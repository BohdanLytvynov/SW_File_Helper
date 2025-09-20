using SW_File_Helper.DAL.Models.TCPModels.Enums;
using System.Net.Sockets;

namespace SW_File_Helper.BL.Net.NetworkStreamProcessors.Base
{
    public abstract class NetworkStreamProcessorBase : INetworkStreamProcessor
    {
        public INetworkStreamProcessor Next { get; set; }
        public MessageType MessageType { get; init; }

        protected NetworkStreamProcessorBase()
        {
            MessageType = MessageType.None;
        }

        public virtual void Process(MessageType type, NetworkStream networkStream, string clientIp)
        {
            if(MessageType != type)
                Next?.Process(type, networkStream, clientIp);
            return;
        }
    }
}

using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.DAL.Models.TCPModels.Enums;
using System.Net.Sockets;

namespace SW_File_Helper.BL.Net.NetworkStreamProcessors.Base
{
    public abstract class NetworkStreamProcessorBase : INetworkStreamProcessor
    {
        protected bool m_processed;
        public INetworkStreamProcessor Next { get; set; }
        public MessageType MessageType { get; init; }

        public ILogger Logger { get; set; }

        protected NetworkStreamProcessorBase(ILogger logger)
        {
            MessageType = MessageType.None;
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            m_processed = false;
        }

        public virtual void Process(MessageType type, NetworkStream networkStream, string clientIp)
        {
            if(MessageType != type && !m_processed)
                Next?.Process(type, networkStream, clientIp);
            return;
        }

        public void Reset()
        {
            m_processed = false;
        }
    }
}

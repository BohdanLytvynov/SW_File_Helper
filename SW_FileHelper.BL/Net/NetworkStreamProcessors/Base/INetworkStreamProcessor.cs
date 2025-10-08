using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.DAL.Models.TCPModels.Enums;
using System.Net.Sockets;

namespace SW_File_Helper.BL.Net.NetworkStreamProcessors.Base
{
    public interface INetworkStreamProcessor
    {
        public ILogger Logger { get; set; }

        public INetworkStreamProcessor Next { get; set; }
        MessageType MessageType { get; init; }

        void Process(MessageType type, NetworkStream networkStream, string clientIp);

        void Reset();
    }
}

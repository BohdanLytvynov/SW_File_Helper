using SW_File_Helper.DAL.Models.TCPModels.Enums;
using System.Net.Sockets;

namespace SW_File_Helper.BL.Net.NetworkStreamProcessors.Base
{
    public interface INetworkStreamProcessor
    {
        public int RecieveBufferSize { get; set; }

        public INetworkStreamProcessor Next { get; set; }
        MessageType MessageType { get; init; }

        void Process(MessageType type, NetworkStream networkStream, string clientIp);
    }
}

using SW_File_Helper.BL.Loggers.Base;
using System.Net.Sockets;

namespace SW_File_Helper.BL.Net.NetworkStreamProcessors.Base
{
    public interface INetworkStreamProcessor
    {
        public int BufferSize { get; set; }

        public ILogger Logger { get; set; }

        void ProcessNetworkStream(NetworkStream networkStream, string clientIp);
    }
}

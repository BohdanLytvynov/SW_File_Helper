using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.Base;
using System.Net.Sockets;

namespace SW_File_Helper.BL.Net.NetworkStreamProcessorWrappers.Base
{
    public interface INetworkStreamProcessorWrapper
    {
        public INetworkStreamProcessor Processor { get; set; }
        public ILogger Logger { get; set; }
        void ProcessNetworkStream(NetworkStream networkStream, string clientIp);
    }
}

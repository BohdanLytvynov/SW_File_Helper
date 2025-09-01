using System.Net.Sockets;

namespace SW_File_Helper.BL.Net.NetworkStreamProcessors.Base
{
    public abstract class NetworkProcessorBase : INetworkStreamProcessor
    {
        public int BufferSize { get; set; }

        public NetworkProcessorBase()
        {
            BufferSize = 1024;
        }

        public abstract void ProcessNetworkStream(NetworkStream networkStream);
    }
}

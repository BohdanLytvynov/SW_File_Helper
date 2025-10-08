using SW_File_Helper.BL.Net.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessorWrappers.Base;
using System.Net.Sockets;

namespace SW_File_Helper.BL.Net.TCPListeners
{
    public interface ITCPListener : ITCPBase<TcpListener>
    {
        public INetworkStreamProcessorWrapper NetworkStreamProcessorWrapper { get; set; }

        void Start();

        void Stop();
    }
}

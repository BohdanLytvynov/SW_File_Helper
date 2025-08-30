using SW_File_Helper.BL.Net.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.Base;
using System.Net.Sockets;

namespace SW_File_Helper.BL.Net.TCPListeners
{
    public interface ITCPListener<TNetworkStreamProcessor> : ITCPBase<TcpListener>
        where TNetworkStreamProcessor : INetworkStreamProcessor
    {
        public TNetworkStreamProcessor NetworkStreamProcessor { get; set; }

        void Start();

        void Stop();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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

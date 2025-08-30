using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.BL.Net.NetworkStreamProcessors.Base
{
    public interface INetworkStreamProcessor
    {
        public int BufferSize { get; set; }

        void ProcessNetworkStream(NetworkStream networkStream);
    }
}

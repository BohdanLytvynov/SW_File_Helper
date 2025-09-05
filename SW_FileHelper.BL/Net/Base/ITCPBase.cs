using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.BL.Net.Base
{
    public interface ITCPBase<TCPInstance> : IDisposable
        where TCPInstance : IDisposable
    {
        public IPEndPoint Endpoint { get; set; }

        void Init();
    }
}

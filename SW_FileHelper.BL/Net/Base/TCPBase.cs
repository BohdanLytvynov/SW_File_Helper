using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.BL.Net.Base
{
    public abstract class TCPBase<TCPInstance> : ITCPBase<TCPInstance>, IDisposable
        where TCPInstance : IDisposable
    {
        #region Fields
        private TCPInstance m_instance;

        public IPEndPoint Endpoint { get; set; }

        protected TCPInstance GetInstance()
        {
            return m_instance;
        }

        public abstract void Init();

        public abstract void Dispose();
        #endregion
    }
}

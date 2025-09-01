using SW_File_Helper.BL.Loggers.Base;
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

        public ILogger Logger { get; set; }
        #endregion

        #region Properties
        public IPEndPoint Endpoint { get; set; }
        #endregion

        #region Ctor
        protected TCPBase(ILogger logger)
        {
            if (logger != null)
                throw new ArgumentNullException(nameof(logger));

            Logger = logger;
        }
        #endregion

        #region Methods

        protected TCPInstance GetInstance()
        {
            return m_instance;
        }

        protected void SetInstance(TCPInstance instance)
        {
            m_instance = instance;
        }

        public abstract void Init();

        public abstract void Dispose();

        #endregion
    }
}

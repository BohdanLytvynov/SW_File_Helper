using SW_File_Helper.BL.Loggers.Base;
using System.Net;

namespace SW_File_Helper.BL.Net.Base
{
    public abstract class TCPBase<TCPInstance> : ITCPBase<TCPInstance>
        where TCPInstance : IDisposable
    {
        #region Fields
        private TCPInstance m_instance;

        public ILogger Logger { get; set; }
        #endregion

        #region Properties
        public IPEndPoint Endpoint { get; set; }

        public string ClientName { get; init; }
        #endregion

        #region Ctor
        protected TCPBase(ILogger logger, string clientName)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            ClientName = clientName;
        }
        #endregion

        #region Methods

        protected TCPInstance GetInstance() => m_instance;

        protected void SetInstance(TCPInstance instance)
        {
            m_instance = instance;
        }

        public abstract void Init();

        public abstract void Dispose();

        #endregion
    }
}

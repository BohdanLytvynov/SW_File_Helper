using SW_File_Helper.BL.Factories.Base;
using SW_File_Helper.BL.Factories.TCPClientFactories;
using SW_File_Helper.BL.Net.TCPClients;
using System.Net;

namespace SW_File_Helper.BL.Builders
{
    public class TCPClientBuilder : ITCPClientBuilder
    {
        private ITCPClient m_client;

        public TCPClientBuilder(ITCPClientFactory tCPClientFactory)
        {
            Factory = tCPClientFactory ?? throw new ArgumentNullException(nameof(tCPClientFactory));
        }

        public IAbstractFactory<ITCPClient> Factory { get; init; }

        public ITCPClientBuilder CreateClient()
        {
            m_client = Factory.Create();
            return this;
        }

        public ITCPClient GetObject()
        {
            return m_client;
        }

        public ITCPClientBuilder InitClient()
        {
            m_client.Init();
            return this;
        }

        public ITCPClientBuilder SetIpEndpoint(IPEndPoint serverEndpoint)
        {
            m_client.Endpoint = serverEndpoint;
            return this;
        }
    }
}

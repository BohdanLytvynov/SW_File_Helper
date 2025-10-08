using SW_File_Helper.BL.Builders.Base;
using SW_File_Helper.BL.Net.TCPClients;
using System.Net;

namespace SW_File_Helper.BL.Builders
{
    public interface ITCPClientBuilder : IBuilder<ITCPClient>
    {
        ITCPClientBuilder SetIpEndpoint(IPEndPoint serverEndpoint);
        ITCPClientBuilder InitClient();
        ITCPClientBuilder CreateClient();
    }
}

using SW_File_Helper.BL.Builders;
using SW_File_Helper.BL.Directors.Base;
using SW_File_Helper.BL.Net.TCPClients;
using System.Net;

namespace SW_File_Helper.BL.Directors
{
    public interface ITCPClientDirector : IDirector<ITCPClientBuilder, ITCPClient>
    {
        ITCPClient GetTCPClient(IPEndPoint serverEndpoint);
    }
}

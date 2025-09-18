using SW_File_Helper.BL.Builders;
using SW_File_Helper.BL.Net.TCPClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.BL.Directors
{
    public class TCPClientDirector : ITCPClientDirector
    {
        public ITCPClientBuilder Builder { get; init; }

        public TCPClientDirector(ITCPClientBuilder tCPClientBuilder)
        {
            Builder = tCPClientBuilder ?? throw new ArgumentNullException(nameof(tCPClientBuilder));
        }

        public ITCPClient GetTCPClient(IPEndPoint serverEndpoint)
        {
            return Builder.CreateClient().SetIpEndpoint(serverEndpoint).InitClient().GetObject();
        }
    }
}

using SW_File_Helper.BL.Factories.Base;
using SW_File_Helper.BL.Net.TCPClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.BL.Factories.TCPClientFactories
{
    public interface ITCPClientFactory : IAbstractFactory<ITCPClient>
    {
    }
}

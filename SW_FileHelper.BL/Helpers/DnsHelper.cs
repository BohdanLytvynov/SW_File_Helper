using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.BL.Helpers
{
    public static class DnsHelper
    {
        public static string GetHostname()
        { 
            return Dns.GetHostName();
        }

        public static IPAddress[] GetHostIPs()
        {
            return Dns.GetHostAddresses(GetHostname());
        }
    }
}

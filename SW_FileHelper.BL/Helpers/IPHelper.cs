using System.Net;

namespace SW_File_Helper.BL.Helpers
{
    public static class IPHelper
    {
        public static IPEndPoint CreateEndPoint(string ipAddress, int port)
        {
            return new IPEndPoint(IPAddress.Parse(ipAddress), port);
        }
    }
}

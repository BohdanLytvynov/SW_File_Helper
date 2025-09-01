using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.MessageProcessors;
using SW_File_Helper.BL.Net.TCPListeners;
using SW_File_Helper.BL.Net.TCPMessageListener;

namespace SW_File_Helper.BL.Net.TCPCommandListener
{
    public sealed class TCPMessageListener : TCPListener<IMessageNetworkProcessor>, ITCPMessageListener
    {
        public TCPMessageListener(ILogger logger) : base(logger)
        {
            
        }
    }
}

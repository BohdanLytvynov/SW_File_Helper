using SW_File_Helper.BL.Net.NetworkStreamProcessors.MessageProcessors;
using SW_File_Helper.BL.Net.TCPListeners;

namespace SW_File_Helper.BL.Net.TCPMessageListener
{
    public interface ITCPMessageListener : ITCPListener<IMessageNetworkProcessor>
    {
    }
}

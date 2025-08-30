using SW_File_Helper.BL.Net.NetworkStreamProcessors.FileProcessors;
using SW_File_Helper.BL.Net.TCPListeners;

namespace SW_File_Helper.BL.Net.TCPFileListener
{
    public sealed class TCPFileListener : TCPListener<IFileNetworkProcessor>, ITCPFileListener
    {}
}

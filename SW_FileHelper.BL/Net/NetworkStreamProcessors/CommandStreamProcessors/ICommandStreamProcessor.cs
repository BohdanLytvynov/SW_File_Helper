using SW_File_Helper.BL.Net.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.Base;

namespace SW_File_Helper.BL.Net.NetworkStreamProcessors.CommandStreamProcessors
{
    public interface ICommandStreamProcessor : INetworkStreamProcessor, INotifyOnProcessed<string>
    {

    }
}

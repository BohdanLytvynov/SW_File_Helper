using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.BL.Net.MessageProcessors.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.Base;

namespace SW_File_Helper.BL.Net.NetworkStreamProcessors.MessageProcessors
{
    public interface IMessageNetworkProcessor : INetworkStreamProcessor
    {
        IMessageProcessor MessageProcessor { get; set; }
    }
}

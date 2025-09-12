using Newtonsoft.Json.Linq;
using SW_File_Helper.BL.Net.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.Base;
using SW_File_Helper.DAL.Models.TCPModels;

namespace SW_File_Helper.BL.Net.NetworkStreamProcessors.CommandStreamProcessors
{
    public interface ICommandStreamProcessor : INetworkStreamProcessor, INotifyOnProcessed<JObject>
    {

    }
}

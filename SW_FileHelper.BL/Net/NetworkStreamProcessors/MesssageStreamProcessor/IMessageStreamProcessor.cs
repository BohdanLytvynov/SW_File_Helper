using SW_File_Helper.BL.Net.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.BL.Net.NetworkStreamProcessors.MesssageStreamProcessor
{
    public interface IMessageStreamProcessor : INetworkStreamProcessor, INotifyOnProcessed<string>
    {
    }
}

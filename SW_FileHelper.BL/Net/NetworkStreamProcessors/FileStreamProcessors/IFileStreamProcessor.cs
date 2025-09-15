using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.Base;

namespace SW_File_Helper.BL.Net.NetworkStreamProcessors.FileStreamProcessors
{
    public interface IFileStreamProcessor : INetworkStreamProcessor
    {
        public ILogger Logger { get; set; }

        public string PathToTemp { get; init; }
    }
}

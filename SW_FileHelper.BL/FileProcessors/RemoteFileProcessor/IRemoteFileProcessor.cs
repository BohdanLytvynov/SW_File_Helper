using SW_File_Helper.BL.Factories.TCPClientFactories;
using SW_File_Helper.BL.Net.TCPClients;

namespace SW_File_Helper.BL.FileProcessors.RemoteFileProcessor
{
    public interface IRemoteFileProcessor : IFileProcessor
    {
        public ITCPClientFactory TCPClientFactory { get; set; }

        void AbortOperation();
    }
}

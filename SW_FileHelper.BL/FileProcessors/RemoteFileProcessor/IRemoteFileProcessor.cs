using SW_File_Helper.BL.Net.TCPClients;

namespace SW_File_Helper.BL.FileProcessors.RemoteFileProcessor
{
    public interface IRemoteFileProcessor : IFileProcessor
    {
        public ITCPClient TCPClient { get; set; }

        void AbortOperation();
    }
}

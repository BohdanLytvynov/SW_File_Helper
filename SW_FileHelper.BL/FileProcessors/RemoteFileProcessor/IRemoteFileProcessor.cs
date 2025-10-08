using SW_File_Helper.BL.Directors;
using SW_File_Helper.BL.Factories.TCPClientFactories;
using SW_File_Helper.BL.Net.TCPClients;
using SW_File_Helper.DAL.DataProviders.Settings;

namespace SW_File_Helper.BL.FileProcessors.RemoteFileProcessor
{
    public interface IRemoteFileProcessor : IFileProcessor
    {
        public ITCPClientDirector TCPClientDirector { get; set; }

        public ISettingsDataProvider SettingsDataProvider { get; set; }

        void AbortOperation();
    }
}

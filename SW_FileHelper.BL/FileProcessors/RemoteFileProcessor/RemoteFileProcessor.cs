using SW_File_Helper.BL.Net.TCPClients;
using SW_File_Helper.DAL.DataProviders.Settings;
using SW_File_Helper.DAL.Helpers;
using SW_File_Helper.DAL.Models;

namespace SW_File_Helper.BL.FileProcessors.RemoteFileProcessor
{
    public class RemoteFileProcessor : FileProcessor, IRemoteFileProcessor
    {
        public RemoteFileProcessor(ISettingsDataProvider settingsDataProvider, ITCPClient tCPClient) 
            : base(settingsDataProvider)
        {
            TCPClient = tCPClient;
        }

        public ITCPClient TCPClient { get; set; }

        public void Process(List<FileModel> fileModels)
        {
            m_settingsDataProvider.LoadData();

            var ext = m_settingsDataProvider.GetData().FileExtensionForReplace;

            foreach (FileModel fileModel in fileModels)
            {
                var srcPath = fileModel.PathToFile;

                var filename = Path.GetFileName(srcPath);

                foreach (var destPath in fileModel.PathToDst)
                {
                    IOHelper.RenameFile(destPath, filename, filename + "." + ext);

                    IOHelper.Copy(srcPath, destPath + Path.DirectorySeparatorChar + filename);
                }
            }
        }
    }
}

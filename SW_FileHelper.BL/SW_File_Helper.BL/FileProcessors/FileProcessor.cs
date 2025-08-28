using SW_File_Helper.DAL.DataProviders.Settings;
using SW_File_Helper.DAL.Helpers;
using SW_File_Helper.DAL.Models;

namespace SW_File_Helper.BL.FileProcessors
{
    public class FileProcessor : IFileProcessor
    {
        ISettingsDataProvider m_settingsDataProvider;

        public FileProcessor(ISettingsDataProvider settingsDataProvider)
        {
            m_settingsDataProvider = settingsDataProvider ?? throw new ArgumentNullException(nameof(settingsDataProvider));
        }

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

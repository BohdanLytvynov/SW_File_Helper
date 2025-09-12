using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.DAL.DataProviders.Settings;
using SW_File_Helper.DAL.Helpers;
using SW_File_Helper.DAL.Models;

namespace SW_File_Helper.BL.FileProcessors
{
    public class FileProcessor : IFileProcessor
    {
        protected ILogger m_logger;

        public FileProcessor(ILogger logger)
        {
            m_logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual void Process(List<FileModel> fileModels, string newExtension)
        {
            foreach (FileModel fileModel in fileModels)
            {
                var srcPath = fileModel.PathToFile;

                var filename = Path.GetFileName(srcPath);

                foreach (var destPath in fileModel.PathToDst)
                {
                    IOHelper.RenameFile(destPath, filename, filename + "." + newExtension);

                    IOHelper.Copy(srcPath, destPath + Path.DirectorySeparatorChar + filename);
                }
            }
        }
    }
}

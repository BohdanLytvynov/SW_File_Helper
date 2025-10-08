using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.DAL.Helpers;
using SW_File_Helper.DAL.Models;

namespace SW_File_Helper.BL.FileProcessors
{
    public class FileProcessor : IFileProcessor
    {
        protected ILogger m_logger;

        protected string m_pathToTemp;

        public FileProcessor(ILogger logger)
        {
            m_logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public FileProcessor(ILogger logger, string pathToTemp)
        {
            m_logger = logger ?? throw new ArgumentNullException(nameof(logger));
            m_pathToTemp = Environment.CurrentDirectory + Path.DirectorySeparatorChar + pathToTemp;
        }

        public virtual void Process(List<FileModel> fileModels, string newExtension)
        {
            Task t = new Task(() =>
            {
                foreach (FileModel fileModel in fileModels)
                {
                    var srcPath = fileModel.PathToFile;

                    var filename = Path.GetFileName(srcPath);

                    foreach (var destPath in fileModel.PathToDst)
                    {
                        IOHelper.RenameFile(destPath, filename, filename + "." + newExtension);

                        IOHelper.Copy(srcPath, destPath + Path.DirectorySeparatorChar + filename);

                        IOHelper.RemoveFile(m_pathToTemp + Path.DirectorySeparatorChar + filename);
                    }
                }
            });

            t.ContinueWith(t => t.Dispose());
            t.Start();
        }
    }
}

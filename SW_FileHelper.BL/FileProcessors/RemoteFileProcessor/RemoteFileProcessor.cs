using SW_File_Helper.BL.Factories.TCPClientFactories;
using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.DAL.Models;

namespace SW_File_Helper.BL.FileProcessors.RemoteFileProcessor
{
    public class RemoteFileProcessor : FileProcessor, IRemoteFileProcessor
    {
        public RemoteFileProcessor(
            ILogger logger, 
            ITCPClientFactory tCPClient) : base(logger)
        {
            TCPClientFactory = tCPClient ?? throw new ArgumentNullException(nameof(tCPClient));
            m_cancelToken = new CancellationTokenSource();
        }

        private CancellationTokenSource m_cancelToken;

        public ITCPClientFactory TCPClientFactory { get; set; }

        public override void Process(List<FileModel> fileModels, string newExtension)
        {
            Task t = new Task(() =>
            {
                try
                {
                    foreach (FileModel fileModel in fileModels)
                    {
                        //if (m_cancelToken.IsCancellationRequested)
                        //    break;

                        //var srcPath = fileModel.PathToFile;

                        //var filename = Path.GetFileName(srcPath);

                        //TCPClientFactory.SendFile(srcPath);

                        //ProcessFilesCommand processFilesCommand = new ProcessFilesCommand();
                        //processFilesCommand.Src = srcPath;
                        //processFilesCommand.NewFileExtension = newExtension;
                        //foreach (var dest in fileModel.PathToDst)
                        //{
                        //    processFilesCommand.Dest.Add(dest);
                        //}
                        //TCPClientFactory.SendCommand(processFilesCommand);
                    }

                    //TCPClient.SendMessage();
                }
                catch (Exception ex)
                {
                    m_logger.Error($"Error occured during sending files! Error: {ex}");
                }
            });

            t.Start();
        }

        public void AbortOperation()
        {
            m_cancelToken.Cancel();
            m_cancelToken = new CancellationTokenSource();
        }
    }
}

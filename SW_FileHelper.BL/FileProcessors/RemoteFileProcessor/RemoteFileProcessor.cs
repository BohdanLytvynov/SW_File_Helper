using Newtonsoft.Json.Linq;
using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.BL.Net.TCPClients;
using SW_File_Helper.DAL.DataProviders.Settings;
using SW_File_Helper.DAL.Helpers;
using SW_File_Helper.DAL.Models;
using SW_File_Helper.DAL.Models.TCPModels;

namespace SW_File_Helper.BL.FileProcessors.RemoteFileProcessor
{
    public class RemoteFileProcessor : FileProcessor, IRemoteFileProcessor
    {
        public RemoteFileProcessor(
            ILogger logger, 
            ITCPClient tCPClient) : base(logger)
        {
            TCPClient = tCPClient ?? throw new ArgumentNullException(nameof(tCPClient));
            m_cancelToken = new CancellationTokenSource();
        }

        private CancellationTokenSource m_cancelToken;

        public ITCPClient TCPClient { get; set; }

        public override void Process(List<FileModel> fileModels, string newExtension)
        {
            Task t = new Task(() =>
            {
                try
                {
                    foreach (FileModel fileModel in fileModels)
                    {
                        if (m_cancelToken.IsCancellationRequested)
                            break;

                        var srcPath = fileModel.PathToFile;

                        var filename = Path.GetFileName(srcPath);

                        foreach (var destPath in fileModel.PathToDst)
                        {

                            if (m_cancelToken.IsCancellationRequested)
                            {
                                TCPClient.SendMessage("Sending of files was aborted by the client!");
                                break;
                            }

                            TCPClient.SendFile(destPath + Path.DirectorySeparatorChar + filename);
                        }

                        ProcessFilesCommand processFilesCommand = new ProcessFilesCommand();
                        processFilesCommand.Src = srcPath;
                        processFilesCommand.NewFileExtension = newExtension;
                        foreach (var dest in fileModel.PathToDst)
                        {
                            processFilesCommand.Dest.Add(dest);
                        }
                        string data = JsonHelper.SerializeWithNonFormatting(processFilesCommand);
                        TCPClient.SendMessage(data);
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

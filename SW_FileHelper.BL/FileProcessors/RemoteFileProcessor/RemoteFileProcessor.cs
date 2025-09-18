using SW_File_Helper.BL.Directors;
using SW_File_Helper.BL.Factories.TCPClientFactories;
using SW_File_Helper.BL.Helpers;
using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.DAL.DataProviders.Settings;
using SW_File_Helper.DAL.Models;
using SW_File_Helper.DAL.Models.TCPModels;
using System.Net;

namespace SW_File_Helper.BL.FileProcessors.RemoteFileProcessor
{
    public class RemoteFileProcessor : FileProcessor, IRemoteFileProcessor
    {
        public RemoteFileProcessor(
            ILogger logger, 
            ITCPClientDirector tcpClientDirector,
            ISettingsDataProvider settingsDataProvider) : base(logger)
        {
            TCPClientDirector = tcpClientDirector ?? throw new ArgumentNullException(nameof(tcpClientDirector));
            m_cancelToken = new CancellationTokenSource();
            SettingsDataProvider = settingsDataProvider ?? throw new ArgumentNullException(nameof(settingsDataProvider));
        }

        private CancellationTokenSource m_cancelToken;

        public ITCPClientDirector TCPClientDirector { get; set; }
        public ISettingsDataProvider SettingsDataProvider { get; set; }

        public override void Process(List<FileModel> fileModels, string newExtension)
        {
            Task t = new Task(() =>
            {
                try
                {
                    SettingsDataProvider.LoadData();
                    var settings = SettingsDataProvider.GetData();

                    foreach (FileModel fileModel in fileModels)
                    {
                        if (m_cancelToken.IsCancellationRequested)
                            break;

                        ProcessFilesCommand processFilesCommand = new ProcessFilesCommand();

                        using (var tcpClient = TCPClientDirector.GetTCPClient(
                            IPHelper.CreateEndPoint(settings.HostIPAddress, settings.TCPListenerPort)))
                        {
                            var srcPath = fileModel.PathToFile;

                            tcpClient.SendFile(srcPath);
                            
                            processFilesCommand.Src = srcPath;
                            processFilesCommand.NewFileExtension = newExtension;
                            foreach (var dest in fileModel.PathToDst)
                            {
                                processFilesCommand.Dest.Add(dest);
                            }

                            tcpClient.SendCommand(processFilesCommand);
                        }
                    }
                }
                catch (Exception ex)
                {
                    m_logger.Error($"Error occured during sending files! Error: {ex}");
                }
            });

            t.ContinueWith(t => t.Dispose());
            t.Start();
        }

        public void AbortOperation()
        {
            m_cancelToken.Cancel();
            m_cancelToken = new CancellationTokenSource();
        }
    }
}

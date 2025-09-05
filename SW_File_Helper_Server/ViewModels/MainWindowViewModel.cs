using SW_File_Helper.BL.Helpers;
using SW_File_Helper.BL.Net.TCPMessageListener;
using SW_File_Helper.DAL.Models.TCPModels;
using SW_File_Helper.Loggers;
using SW_File_Helper.ViewModels.Base.VM;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Input;
using SW_File_Helper.ViewModels.Models.Logs.Base;
using UICommand = SW_File_Helper.ViewModels.Base.Commands.Command;
using SW_File_Helper.BL.Net.MessageProcessors.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.MessageProcessors;
using SW_File_Helper.BL.Net.MessageProcessors.MessageProcessor;
using SW_File_Helper.BL.Net.MessageProcessors.ProcessFilesCommandProcessors;
using SW_File_Helper.ServiceWrappers;
using Microsoft.Extensions.DependencyInjection;
using System.CodeDom;
using SW_File_Helper.BL.Net.TCPFileListener;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.FileProcessors;

namespace SW_File_Helper_Server.ViewModels
{
    internal class MainWindowViewModel : ValidatableViewModel
    {
        #region Fields
        private string m_title;
        private string m_hostname;
        private ObservableCollection<string> m_IPAddresses;
        private string m_MessageServerPortString;
        private string m_FileServerPortString;
        private int m_SelectedIPIndex;
        private bool m_StartStop; //Start = false Stop = true;
        private string m_StartButtonContent;
        private LogViewModel m_message;

        private IConsoleLogger m_ConsoleLogger;
        private ResourceDictionary m_CommonResources;
        #region Servers

        private IMessageNetworkProcessor m_messageNetworkProcessor;
        private IFileNetworkProcessor m_fileNetworkProcessor;
        private ITCPMessageListener m_MessageListener;
        private ITCPFileListener m_FileListener;

        #endregion

        #endregion

        #region Commands
        public ICommand OnStartServersButtonPressed { get; }
        #endregion

        #region IDataError
        public override string this[string columnName]
        {
            get 
            {
                string error = string.Empty;

                switch (columnName)
                {
                    case nameof(MessageServerPortString):
                        SetValidArrayValue(0, 
                            ValidationHelpers
                            .IsIntegerNumberValid(MessageServerPortString, out error));
                        break;

                    case nameof(FileServerPortString):
                        SetValidArrayValue(1,
                            ValidationHelpers
                            .IsIntegerNumberValid(FileServerPortString, out error));
                        break;
                }

                return error;
            }
        }
        #endregion

        #region Properties
        public string Title 
        { get => m_title; set=> Set(ref m_title, value); }

        public string HostName 
        { get=> m_hostname; set=> Set(ref m_hostname, value); }

        public ObservableCollection<string> IPAddresses
        { get=> m_IPAddresses; set=> m_IPAddresses = value; }

        public string MessageServerPortString 
        {
          get=> m_MessageServerPortString; 
          set => Set(ref m_MessageServerPortString, value); 
        }

        public string FileServerPortString 
        {
            get => m_FileServerPortString; 
            set=> Set(ref m_FileServerPortString, value); 
        }

        public int SelectedIPIndex 
        { get=> m_SelectedIPIndex; set=> Set(ref m_SelectedIPIndex, value); }

        public string StartButtonContent 
        { get => m_StartButtonContent; set => Set(ref m_StartButtonContent, value); }

        public LogViewModel Message 
        {
            get=> m_message;
            set=> Set(ref m_message, value);
        }
        #endregion

        #region Ctor
        
        private void M_ConsoleLogger_OnLogProcessed(object arg1, string arg2, SW_File_Helper.BL.Loggers.Enums.LogType arg3)
        {
            Message = (LogViewModel)arg1 ?? throw new InvalidCastException("Unable to cast log message to LogViewModel!");
        }

        public MainWindowViewModel(ServiceWrapper serviceWrapper)
        {
            #region Init Fields
            m_ConsoleLogger = serviceWrapper.GetRequiredService<IConsoleLogger>();

            if(m_ConsoleLogger == null)
                throw new ArgumentNullException(nameof(m_ConsoleLogger));

            m_CommonResources = new ResourceDictionary();
            m_CommonResources.Source = new Uri("/SW_File_Helper;component/Resources/ViewsCommon.xaml", UriKind.RelativeOrAbsolute);
            m_message = new LogViewModel("Console loaded...", m_CommonResources["consoleMsg"] as Style);
            m_StartStop = false;
            m_StartButtonContent = String.Empty;
            m_MessageServerPortString = String.Empty;
            m_FileServerPortString = String.Empty;
            InitValidArray(2);
            m_title = "SolarWinds File Helper Server V 1.0.0.0";
            m_SelectedIPIndex = -1;
            m_hostname = DnsHelper.GetHostname();
            var iPAddresses = DnsHelper.GetHostIPs();
            m_IPAddresses = new ObservableCollection<string>();
            foreach (var iPAddress in iPAddresses)
            {
                m_IPAddresses.Add(iPAddress.ToString());
            }
            CalculateStartButtonContent();
            #endregion

            #region Allocate Server

            ConfigureNetworkProcessors();

            m_MessageListener = new TCPMessageListener(m_ConsoleLogger, m_messageNetworkProcessor);
            m_FileListener = new ITCPFileListener(m_ConsoleLogger, );

            #endregion

            #region Init Commands
            OnStartServersButtonPressed = new UICommand
                (
                OnStartServersButtonPressedExecute,
                CanOnStartServersButtonPressedExecute
                );
            #endregion
        }
        #endregion

        #region Methods
        #region On Start Servers Button Pressed

        private bool CanOnStartServersButtonPressedExecute(object p)
            => m_SelectedIPIndex >= 0 && ValidateFields(0, GetLastIndexOfValidArray());

        private void OnStartServersButtonPressedExecute(object p)
        {
            m_StartStop = !m_StartStop;
            
            CalculateStartButtonContent();

            if (m_StartStop)
            {
                StartServers();
            }
            else
            { 
                StopServers();
            }
        }

        #endregion

        private void CalculateStartButtonContent()
        {
            if (!m_StartStop)// Start
                StartButtonContent = "Start";
            else
                StartButtonContent = "Stop";
        }

        private void StartServers()
        {
            m_MessageListener.Endpoint
                = new IPEndPoint(
                    IPAddress.Parse(IPAddresses[SelectedIPIndex]),
                    int.Parse(MessageServerPortString));

            m_MessageListener.Init();
            m_MessageListener.Start();
        }

        private void StopServers()
        {
            m_MessageListener.Stop();
        }

        public void OnMessageRecieved(Message message, string clientIp)
        {
            var msgStyle = m_CommonResources["consoleMsg"] as Style;
            var clientIpStyle = m_CommonResources["ClietIPStyle"] as Style;

            Message = new ConsoleMessageViewModel(clientIp,
                clientIpStyle, message.Text, msgStyle);
        }

        public void OnFileRecieved(Message message, string clientIp)
        {
            var file = (ProcessFilesCommand)message;
        }

        private void ConfigureNetworkProcessors()
        {
            MessageProcessor messageProcessor = new MessageProcessor();
            messageProcessor.OnProcessed += OnMessageRecieved;

            var processfileCommandProcessor = new ProcessFilesCommandProcessor();
            processfileCommandProcessor.OnProcessed += OnFileRecieved;

            messageProcessor.AddMessageProcessor(processfileCommandProcessor);

            m_messageNetworkProcessor = new MessageNetworkProcessor(messageProcessor, m_ConsoleLogger);

            m_ConsoleLogger.OnLogProcessed += M_ConsoleLogger_OnLogProcessed;
        }
        #endregion
    }
}

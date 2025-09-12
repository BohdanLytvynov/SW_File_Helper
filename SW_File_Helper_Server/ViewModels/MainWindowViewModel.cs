using SW_File_Helper.BL.Helpers;
using SW_File_Helper.DAL.Models.TCPModels;
using SW_File_Helper.Loggers;
using SW_File_Helper.ViewModels.Base.VM;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using SW_File_Helper.ViewModels.Models.Logs.Base;
using UICommand = SW_File_Helper.ViewModels.Base.Commands.Command;
using SW_File_Helper.ServiceWrappers;
using SW_File_Helper.BL.FileProcessors;
using SW_File_Helper.Converters;
using SW_File_Helper.DAL.Models;
using SW_File_Helper.BL.Net.TCPListeners;
using System.Net;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.MesssageStreamProcessor;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.CommandStreamProcessors;
using Newtonsoft.Json.Linq;
using Microsoft.VisualBasic;

namespace SW_File_Helper_Server.ViewModels
{
    internal class MainWindowViewModel : ValidatableViewModel
    {
        #region Fields
        private string m_title;
        private string m_hostname;
        private ObservableCollection<string> m_IPAddresses;
        private string m_ListenerPortString;
        private int m_SelectedIPIndex;
        private bool m_StartStop; //Start = false Stop = true;
        private string m_StartButtonContent;
        private LogViewModel m_message;

        private IConsoleLogger m_ConsoleLogger;
        private ResourceDictionary m_CommonResources;
        private ITCPListener m_listener;

        #region Converters
        IProcessFilesCommandToFileModelConverter m_processFilesCommandToFileModelConverter;
        #endregion

        #region File Processor
        IFileProcessor m_fileProcessor;
        #endregion

        #region Servers

        

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
                    case nameof(ListenerPortString):
                        SetValidArrayValue(0, 
                            ValidationHelpers
                            .IsIntegerNumberValid(ListenerPortString, out error));
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

        public string ListenerPortString 
        {
          get=> m_ListenerPortString; 
          set => Set(ref m_ListenerPortString, value); 
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
            m_fileProcessor = serviceWrapper.GetRequiredService<FileProcessor>();
            m_processFilesCommandToFileModelConverter = serviceWrapper.GetRequiredService<IProcessFilesCommandToFileModelConverter>();
            m_CommonResources = new ResourceDictionary();
            m_CommonResources.Source = new Uri("/SW_File_Helper;component/Resources/ViewsCommon.xaml", UriKind.RelativeOrAbsolute);
            m_message = new LogViewModel("Console loaded...", m_CommonResources["consoleMsg"] as Style);
            m_StartStop = false;
            m_StartButtonContent = String.Empty;
            m_ListenerPortString = String.Empty;
            InitValidArray(1);
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

            m_ConsoleLogger.OnLogProcessed += M_ConsoleLogger_OnLogProcessed;

            #region Get Server
            m_listener = serviceWrapper.GetRequiredService<ITCPListener>();
            #endregion

            #region Subscribe to Events

            serviceWrapper.GetRequiredService<IMessageStreamProcessor>().OnProcess += OnMessageRecieved;
            serviceWrapper.GetRequiredService<ICommandStreamProcessor>().OnProcess += OnCommandRecieved;

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
            string ip = IPAddresses[SelectedIPIndex];
            m_listener.Endpoint = new IPEndPoint(IPAddress.Parse(ip), int.Parse(ListenerPortString));
            m_listener.Init();
            m_listener.Start();
        }

        public void StopServers()
        {
            m_listener.Stop();
        }

        public void OnMessageRecieved(string message, string clientIp)
        {
            var msgStyle = m_CommonResources["consoleMsg"] as Style;
            var clientIpStyle = m_CommonResources["ClietIPStyle"] as Style;
            QueueJobToDispatcher(() =>
            {
                Message = new ConsoleMessageViewModel(clientIp,
                    clientIpStyle, message, msgStyle);
            });
        }

        private void OnCommandRecieved(JObject value, string clientIp)
        {
            string commandType = value["CommandType"].ToString();
            string commandText = value["Text"].ToString();

            if (commandType.Equals(commandType)
                && commandText.Equals(SW_File_Helper.Common.Constants.PROCESS_FILES_COMMAND))
            {
                //To do
            }
        }
        #endregion
    }
}

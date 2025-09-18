using Microsoft.Extensions.DependencyInjection;
using SW_File_Helper.BL.Directors;
using SW_File_Helper.BL.FileProcessors.RemoteFileProcessor;
using SW_File_Helper.BL.Helpers;
using SW_File_Helper.Converters;
using SW_File_Helper.DAL.DataProviders.Settings;
using SW_File_Helper.DAL.Models;
using SW_File_Helper.DAL.Repositories.Favorites;
using SW_File_Helper.Interfaces;
using SW_File_Helper.Loggers;
using SW_File_Helper.ServiceWrappers;
using SW_File_Helper.ViewModels.Base.Commands;
using SW_File_Helper.ViewModels.Base.VM;
using SW_File_Helper.ViewModels.Models;
using SW_File_Helper.ViewModels.Models.Logs.Base;
using SW_File_Helper.Views;
using SW_File_Helper.Views.Pages;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using static SW_File_Helper.Controls.EditableListView;

namespace SW_File_Helper.ViewModels.Views
{
    internal class MainWindowViewModel : ViewModelBase, IManualDrawable
    {
        #region Events

        public EventHandler WindowClosed;

        #endregion

        #region Fields
        private float m_drawChildrenOnDelayValue;

        private bool m_ManualDraw;

        private string m_title;

        private string m_currentAssembly;

        private string m_ListViewFileViewModelTypeName;

        private ObservableCollection<CustomListViewItem> m_files;

        private SettingsPage m_settingsPage;

        private ServiceWrapper? m_serviceWrapper;

        private IListViewFileViewModelToFileModelConverter? m_fileViewModelToFileModelConverter;

        private IRemoteFileProcessor? m_remoteFileProcessor;

        private OnShowFavoritesFired m_OnShowFavoritesFired;

        private OnAddToFavoritesFired m_OnAddToFavoritesFired;

        private IListViewFileViewModelToFileModelConverter? m_listViewFileViewModelToFileModelConverter;

        private IFavoritesRepository? m_favoritesRepository;

        private ISettingsDataProvider? m_settingsDataProvider;

        private IConsoleLogger m_consoleLogger;

        private LogViewModel m_LogMessage;

        private ResourceDictionary m_commonResourceDictionary;

        private ITCPClientDirector m_TcpClientDirector;
        #endregion

        #region Properties
        public ObservableCollection<CustomListViewItem> Files
        { get => m_files; set => m_files = value; }
        public string Title
        { get => m_title; set => Set(ref m_title, value); }
        public string CurrentAssembly
        { get => m_currentAssembly; set => Set(ref m_currentAssembly, value); }
        public string ListViewFileViewModel
        { get => m_ListViewFileViewModelTypeName; set => Set(ref m_ListViewFileViewModelTypeName, value); }
        public SettingsPage SettingsPage
        { get => m_settingsPage; set => Set(ref m_settingsPage, value); }
        public OnShowFavoritesFired ShowFavoritesFired
        { get => m_OnShowFavoritesFired; set => Set(ref m_OnShowFavoritesFired, value); }
        public OnAddToFavoritesFired AddToFavoritesFired
        { get => m_OnAddToFavoritesFired; set => Set(ref m_OnAddToFavoritesFired, value); }
        public bool ManualDraw
        { get => m_ManualDraw; set => Set(ref m_ManualDraw, value); }
        public LogViewModel LogMessage
        { get=> m_LogMessage; set => Set(ref m_LogMessage, value); }

        #endregion

        #region Commands
        public ICommand OnProcessButtonPressed { get; }

        public ICommand OnQuiteButtonPressed { get; }
        #endregion

        #region Ctor
        public MainWindowViewModel(ServiceWrapper serviceWrapper,
            IListViewFileViewModelToFileModelConverter listViewFileViewModelToFileModelConverter,
            IFavoritesRepository favoritesRepository,
            ISettingsDataProvider settingsDataProvider,
            IConsoleLogger consoleLogger) : this()
        {
            if (serviceWrapper == null)
                throw new ArgumentNullException(nameof(serviceWrapper));

            if (listViewFileViewModelToFileModelConverter == null)
                throw new ArgumentNullException(nameof(listViewFileViewModelToFileModelConverter));

            if (favoritesRepository == null)
                throw new ArgumentNullException(nameof(favoritesRepository));

            if (settingsDataProvider == null)
                throw new ArgumentNullException(nameof(settingsDataProvider));

            if (consoleLogger == null)
                throw new ArgumentNullException(nameof(consoleLogger));

            m_serviceWrapper = serviceWrapper;

            m_settingsPage = m_serviceWrapper.Services.GetRequiredService<SettingsPage>();
            m_fileViewModelToFileModelConverter = m_serviceWrapper
               .Services.GetRequiredService<IListViewFileViewModelToFileModelConverter>();

            m_favoritesRepository = favoritesRepository;
            m_listViewFileViewModelToFileModelConverter = listViewFileViewModelToFileModelConverter;
            m_settingsDataProvider = settingsDataProvider;

            m_consoleLogger = consoleLogger;
            m_consoleLogger.OnLogProcessed += M_consoleLogger_OnLogProcessed;
            m_TcpClientDirector = serviceWrapper.GetRequiredService<ITCPClientDirector>();
            m_remoteFileProcessor = serviceWrapper.GetRequiredService<IRemoteFileProcessor>();
        }

        private void M_consoleLogger_OnLogProcessed(object arg1, string arg2, BL.Loggers.Enums.LogType arg3)
        {
            this.QueueJobToDispatcher(() =>
            {
                LogMessage = (arg1 as LogViewModel) ?? throw new InvalidCastException("Unable to cast log message to LogViewModel!");
            });
        }

        public void OnCheckClientButtonPressed()
        {
            var settings = m_settingsDataProvider.GetData();

            using (var tcpClient = m_TcpClientDirector.GetTCPClient(
                    IPHelper.CreateEndPoint(settings.HostIPAddress, settings.TCPListenerPort))
                )
            {
                tcpClient.SendMessage("Testing Connection");
            }
        }

        public MainWindowViewModel()
        {
            #region Field Initialization
            m_commonResourceDictionary = new ResourceDictionary();
            m_commonResourceDictionary.Source = new Uri("/SW_File_Helper;component/Resources/ViewsCommon.xaml", UriKind.RelativeOrAbsolute);

            m_LogMessage = new LogViewModel("Console loaded...", m_commonResourceDictionary["consoleMsg"] as Style);
            m_drawChildrenOnDelayValue = 0.2f;
            m_ManualDraw = false;
            m_files = new ObservableCollection<CustomListViewItem>();
            m_title = "SW File Helper v 1.0.0.0";
            m_currentAssembly = Assembly.GetEntryAssembly().Location;
            m_ListViewFileViewModelTypeName = typeof(ListViewFileViewModel).FullName;

            m_OnShowFavoritesFired = new OnShowFavoritesFired((string favoritesType) =>
            {
                var favoritesWindow = m_serviceWrapper.Services.GetRequiredService<FavoritesWindow>();
                favoritesWindow.OnFavoritesSelected += FavoritesWindow_OnFavoritesSelected;
                favoritesWindow.Topmost = true;
                favoritesWindow.SetTypeName(favoritesType);
                favoritesWindow.ShowDialog();
            });

            m_OnAddToFavoritesFired = new OnAddToFavoritesFired((items) =>
            {
                foreach (var item in items)
                {
                    m_favoritesRepository
                    .Add(m_listViewFileViewModelToFileModelConverter.Convert(item as ListViewFileViewModel));
                }
            });

            #endregion

            #region Init Commands

            OnProcessButtonPressed = new Command(
                OnProcessButtonPressedExecute,
                CanOnProcessButtonPressedExecute
                );

            OnQuiteButtonPressed = new Command(
                OnQuiteButtonPressedExecute,
                CanOnQuiteButtonPressedExecute
                );

            #endregion
        }
        #endregion

        #region Methods

        private void FavoritesWindow_OnFavoritesSelected(List<Guid> ids)
        {
            var files = m_favoritesRepository.GetAll(ids);

            Task refreshChildrenTask = new Task(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(m_drawChildrenOnDelayValue));
                foreach (var item in Files)
                {
                    (item as IManualDrawable)?.Draw();
                }
            });

            foreach (var file in files)
            {
                var element = m_listViewFileViewModelToFileModelConverter.ReverseConvert((FileModel)file);
                element.Number = Files.Count + 1;
                Files.Add(element);
            }
            Draw();

            //refreshChildrenTask.ContinueWith(t => t.Dispose());
            refreshChildrenTask.Start();
        }

        #region On Process Button Pressed

        private bool CanOnProcessButtonPressedExecute(object p)
        {
            if (Files.Count == 0)
                return false;

            foreach (var item in Files)
            {
                (item as ListViewFileViewModel)!.Validate();

                if (!item.IsValid && item.IsEnabled)
                    return false;
            }

            return true;
        }

        private void OnProcessButtonPressedExecute(object p)
        {
            var filesToProcess = Files.Where(x => x.IsValid && x.IsEnabled).Select(x => x as ListViewFileViewModel);
            var settings = m_settingsDataProvider;
            settings.LoadData();
            var ext = settings.GetData().FileExtensionForReplace;
            List<FileModel> fileModels = new List<FileModel>();

            foreach (var file in filesToProcess)
            {
                fileModels.Add(m_fileViewModelToFileModelConverter.Convert(file));
            }

            m_remoteFileProcessor.Process(fileModels, ext);
        }

        #endregion

        #region On Quite Button Pressed
        private bool CanOnQuiteButtonPressedExecute(object p) => true;

        private void OnQuiteButtonPressedExecute(object p)
        {
            WindowClosed?.Invoke(this, EventArgs.Empty);
        }

        public void Draw()
        {
            ManualDraw = !ManualDraw;
        }
        #endregion

        #endregion
    }
}

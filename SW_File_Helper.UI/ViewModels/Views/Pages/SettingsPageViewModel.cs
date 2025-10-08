using SW_File_Helper.BL.Factories.TCPClientFactories;
using SW_File_Helper.BL.Helpers;
using SW_File_Helper.DAL.DataProviders.Settings;
using SW_File_Helper.DAL.Models;
using SW_File_Helper.DAL.Repositories.Favorites;
using SW_File_Helper.ViewModels.Base.Commands;
using SW_File_Helper.ViewModels.Base.VM;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SW_File_Helper.ViewModels.Views.Pages
{
    public class SettingsPageViewModel : ValidatableViewModel
    {
        #region Events
        public event Action OnCheckClientPressed;
        #endregion

        #region Fields
        private string m_fileExtensionForReplace;

        private bool m_remoteModeEnabled;

        private string m_hostIpAddress;

        private string m_ListenerPortString;

        private ObservableCollection<string> m_FavoriteIps;

        private bool m_clientStatus;//do we need this?

        private bool m_showPopup;

        private string m_SelectedIPAddress;

        private ISettingsDataProvider m_dataProvider;

        private IFavoritesRepository m_favoritesRepository;
        
        #endregion

        #region Properties
        public string FileExtensionForReplace
        { 
            get=>m_fileExtensionForReplace;
            set=>Set(ref m_fileExtensionForReplace, value);
        }

        public bool RemoteModeEnabled
        { 
            get => m_remoteModeEnabled;
            set
            {
                Set(ref m_remoteModeEnabled, value);
                m_dataProvider.GetData().EnableRemoteMode = value;
                m_dataProvider.SaveData();
            }
        }

        public string HostIPAddress 
        { get=> m_hostIpAddress; set => Set(ref m_hostIpAddress, value); }

        public string ListenerPortString
        { get=> m_ListenerPortString; set => Set(ref m_ListenerPortString, value); }

        public ObservableCollection<string> FavoriteIPs
        { get=> m_FavoriteIps; set=> m_FavoriteIps = value; }

        public bool ClientStatus//do we need this?
        { get=> m_clientStatus; set=> Set(ref m_clientStatus, value); }

        public bool ShowPopup
        { get => m_showPopup; set=> Set(ref m_showPopup, value); }

        public string SelectedIpAddress
        { 
            get => m_SelectedIPAddress;
            set
            {
                Set(ref m_SelectedIPAddress, value);
                HostIPAddress = SelectedIpAddress;
                ShowPopup = false;
            }
        }

        #endregion

        #region IDataErrorInfo
        public override string this[string columnName]
        {
            get 
            {
                string error = string.Empty;
                bool isValid = false;
                var settings = m_dataProvider.GetData();
                switch (columnName)
                {
                    case nameof(FileExtensionForReplace):
                        isValid = !ValidationHelpers.IsTextEmpty(FileExtensionForReplace, out error);
                        SetValidArrayValue(0, isValid);
                        if (isValid)
                        { 
                            settings.FileExtensionForReplace = FileExtensionForReplace;
                            m_dataProvider.SaveData();
                        }
                        break;
                    case nameof(HostIPAddress):
                        isValid = ValidationHelpers.IsIPAddressValid(HostIPAddress, out error);
                        SetValidArrayValue(1, isValid);
                        if (isValid)
                        { 
                            settings.HostIPAddress = HostIPAddress;
                            m_dataProvider.SaveData();
                        }
                        break;
                    case nameof(ListenerPortString):
                        isValid = ValidationHelpers.IsIntegerNumberValid(ListenerPortString, out error);
                        SetValidArrayValue(2, isValid);
                        if (isValid)
                        {
                            settings.TCPListenerPort = int.Parse(ListenerPortString);
                            m_dataProvider.SaveData();
                        }
                        break;
                }

                return error;
            }
        }
        #endregion

        #region Commands
        public ICommand OnAddToFavoritesIPButtonPressed { get; }

        public ICommand OnCheckClientButtonPressed { get; }
        #endregion

        #region Ctor
        public SettingsPageViewModel(ISettingsDataProvider settingsDataProvider,
            IFavoritesRepository favoritesRepository) : this()
        {
            if(settingsDataProvider == null)
                throw new ArgumentNullException(nameof(settingsDataProvider));

            if(favoritesRepository == null)
                throw new ArgumentNullException(nameof(favoritesRepository));

            m_dataProvider = settingsDataProvider;

            m_favoritesRepository = favoritesRepository;

            InitFieldsBasedOnConfiguration();

            InitFavoritesIPAddresses();
        }

        public SettingsPageViewModel()
        {
            #region Fields Init

            m_showPopup = false;

            m_SelectedIPAddress = string.Empty;

            InitValidArray(3);

            #endregion

            #region Init Commands

            OnAddToFavoritesIPButtonPressed = new Command(
                OnAddIPAddressTofavoritesButtonPressedExecute,
                CanOnAddIPAddressToFavoritesButtonPressedExecute
                );

            OnCheckClientButtonPressed = new Command(
                OnCheckClientButtonPressedExecute,
                CanOnCheckClientButtonPressedExecute
                );

            #endregion
        }
        #endregion

        #region Methods

        #region On Start Client Button Pressed

        private bool CanOnCheckClientButtonPressedExecute(object p) => 
            ValidateFields(1, GetLastIndexOfValidArray()) && RemoteModeEnabled;

        private void OnCheckClientButtonPressedExecute(object p)
        {
            OnCheckClientPressed?.Invoke();
        }

        #endregion

        #region On Add IPAddress To Favorites Pressed

        private bool CanOnAddIPAddressToFavoritesButtonPressedExecute(object p) => ValidateFields(1,1);

        private void OnAddIPAddressTofavoritesButtonPressedExecute(object p)
        {
            FavoriteIPs.Add(HostIPAddress);

            m_favoritesRepository.Add(new IPAddressFavorites() 
            { IPAddress = HostIPAddress });
        }

        #endregion

        private void InitFieldsBasedOnConfiguration()
        {
            var settings = m_dataProvider.GetData();
            m_fileExtensionForReplace = settings.FileExtensionForReplace;
            m_remoteModeEnabled = settings.EnableRemoteMode;
            m_hostIpAddress = settings.HostIPAddress;
            m_ListenerPortString = settings.TCPListenerPort.ToString();
        }

        private void InitFavoritesIPAddresses()
        {
            var favoriteIps = m_favoritesRepository.GetAll()
                .Where(x => x.TypeName == nameof(IPAddressFavorites))
                .Select(x => (x as IPAddressFavorites).IPAddress);

            m_FavoriteIps = new ObservableCollection<string>();

            foreach (var ip in favoriteIps)
            {
                m_FavoriteIps.Add(ip.ToString());
            }
        }

        #endregion
    }
}

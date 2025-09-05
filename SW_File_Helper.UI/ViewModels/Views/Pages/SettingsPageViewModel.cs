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
        #region Fields
        private string m_fileExtensionForReplace;

        private string m_hostIpAddress;

        private ObservableCollection<string> m_FavoriteIps;
        
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

        public string HostIPAddress 
        { get=> m_hostIpAddress; set => Set(ref m_hostIpAddress, value); }

        public ObservableCollection<string> FavoriteIPs
        { get=> m_FavoriteIps; set=> m_FavoriteIps = value; }

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

                switch (columnName)
                {
                    case nameof(FileExtensionForReplace):
                        isValid = !ValidationHelpers.IsTextEmpty(FileExtensionForReplace, out error);
                        SetValidArrayValue(0, isValid);
                        if (isValid)
                        { 
                            m_dataProvider.GetData().FileExtensionForReplace = FileExtensionForReplace;
                            m_dataProvider.SaveData();
                        }
                        break;
                    case nameof(HostIPAddress):
                        isValid = ValidationHelpers.IsIPAddressValid(HostIPAddress, out error);
                        SetValidArrayValue(1, isValid);
                        if (isValid)
                        { 
                            m_dataProvider.GetData().HostIPAddress = HostIPAddress;
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

            InitValidArray(4);

            #endregion

            #region Init Commands

            OnAddToFavoritesIPButtonPressed = new Command(
                OnAddIPAddressTofavoritesButtonPressedExecute,
                CanOnAddIPAddressToFavoritesButtonPressedExecute
                );

            #endregion
        }
        #endregion

        #region Methods

        #region On Start Client Button Pressed

        private bool CanOnStartServerButtonPressedExecute(object p) => 
            ValidateFields(1, GetLastIndexOfValidArray());

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

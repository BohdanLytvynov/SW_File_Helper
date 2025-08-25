using SW_File_Helper.BL.Helpers;
using SW_File_Helper.DAL.DataProviders.Settings;
using SW_File_Helper.ViewModels.Base.Commands;
using SW_File_Helper.ViewModels.Base.VM;
using System.Windows.Input;

namespace SW_File_Helper.ViewModels.Views.Pages
{
    public class SettingsPageViewModel : ValidatableViewModel
    {
        #region Fields
        private string m_fileExtensionForReplace;

        private ISettingsDataProvider m_dataProvider;
        #endregion

        #region Properties
        public string FileExtensionForReplace 
        { 
            get=>m_fileExtensionForReplace; 
            set=>Set(ref m_fileExtensionForReplace, value);
        }
        #endregion

        #region IDataErrorInfo
        public override string this[string columnName]
        {
            get 
            {
                string error = string.Empty;

                switch (columnName)
                {
                    case nameof(FileExtensionForReplace):
                        SetValidArrayValue(0, !ValidationHelpers.IsTextEmpty(FileExtensionForReplace, out error));
                        break;
                }

                return error;
            }
        }
        #endregion

        #region Commands
        public ICommand OnSaveButtonPressed { get; }
        #endregion

        #region Ctor
        public SettingsPageViewModel(ISettingsDataProvider settingsDataProvider) : this()
        {
            if(settingsDataProvider == null)
                throw new ArgumentNullException(nameof(settingsDataProvider));

            m_dataProvider = settingsDataProvider;

            m_fileExtensionForReplace = m_dataProvider.Settings.FileExtensionForReplace;
        }

        public SettingsPageViewModel()
        {
            #region Fields Init

            InitValidArray(1);

            #endregion

            #region Init Commands

            OnSaveButtonPressed = new Command(
                OnSaveButtonPressedExecute,
                CanOnSaveButtonPressedExecute
                );

            #endregion
        }
        #endregion

        #region Methods

        #region On Save Button Pressed

        private bool CanOnSaveButtonPressedExecute(object p)
        {
            return ValidateFields(0, GetLastIndexOfValidArray());
        }

        private void OnSaveButtonPressedExecute(object p)
        { 
            var settings = m_dataProvider.Settings;
            settings.FileExtensionForReplace = FileExtensionForReplace;

            m_dataProvider.SaveData();
        }

        #endregion

        #endregion
    }
}

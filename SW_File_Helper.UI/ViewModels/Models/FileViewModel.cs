using Microsoft.Win32;
using SW_File_Helper.BL.Helpers;
using SW_File_Helper.ViewModels.Base.Commands;
using System.Windows.Input;

namespace SW_File_Helper.ViewModels.Models
{
    public class FileViewModel : CustomListViewItem
    {
        #region Fields
        
        private string m_FilePath;

        #endregion

        #region Commands
        public ICommand OnBrowseFileButtonPressed { get; private set; }
        #endregion

        #region IDataErrorInfo
        public override string this[string columnName]
        {
            get
            { 
                string error = string.Empty;

                switch (columnName)
                {
                    case nameof(FilePath):
                        IsValid = ValidationHelpers.IsPathValid(FilePath, out error);
                        break;
                }

                return error;
            }
        }
        #endregion

        #region Properties

        public string FilePath 
        { get => m_FilePath; set=> Set(ref m_FilePath, value); }
        
        #endregion

        #region Ctor
        public FileViewModel() : base()
        {
            m_FilePath = string.Empty;
            InitCommands();
        }

        #endregion

        #region Methods
        private void InitCommands()
        {
            OnBrowseFileButtonPressed = new Command(
                OnBrowseButtonPressedExecute,
                CanOnBrowseButtonPressedExecute
                );
        }

        public override string ToString()
        {
            return $"{base.ToString()}) {FilePath} Enabled: {IsEnabled} IsValid: {IsValid}";
        }

        #region On Browse Button Pressed

        private bool CanOnBrowseButtonPressedExecute(object p) => true;

        private void OnBrowseButtonPressedExecute(object p)
        {
            if (this.GetType().Name.Equals(nameof(ListViewFileViewModel)))
            {
                OpenFileDialog dialog = new OpenFileDialog();

                if (dialog.ShowDialog() ?? false)
                {
                    this.FilePath = dialog.FileName;
                }
            }
            else 
            {
                OpenFolderDialog dialog = new OpenFolderDialog();

                if (dialog.ShowDialog() ?? false)
                {
                    this.FilePath = dialog.FolderName;
                }
            }
        }

        #endregion
        #endregion
    }
}

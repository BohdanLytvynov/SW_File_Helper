using Microsoft.Win32;
using SW_File_Helper.BL.Helpers;
using SW_File_Helper.ViewModels.Base.Commands;
using SW_File_Helper.ViewModels.Base.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SW_File_Helper.ViewModels.Models
{
    public class FileViewModel : CustomListViewItem
    {
        #region Fields        

        private string m_FilePath;

        private bool m_IsValid;

        private bool m_IsEnabled;
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
                        SetValidArrayValue(0, ValidationHelpers.IsPathValid(FilePath, out error));
                        break;                    
                }

                return error;
            }
        }
        #endregion

        #region Properties        
        public bool IsEnabled 
        { get => m_IsEnabled; set => Set(ref m_IsEnabled, value); }

        public string FilePath 
        { get => m_FilePath; set=> Set(ref m_FilePath, value); }

        public bool IsValid { get => m_IsValid; private set => m_IsValid = value; }
        #endregion

        #region Ctor
        public FileViewModel() : base()
        {
            m_FilePath = string.Empty;
            IsEnabled = false;
            IsValid = false;
            InitValidArray(1);
            InitCommands();
        }
        
        public FileViewModel(int shownumber, string filePath, bool isEnabled) : base(shownumber)
        {
            #region Fields Init            
            m_IsEnabled = isEnabled;
            IsValid = false;
            m_FilePath = filePath;
            InitValidArray(1);
            InitCommands();
            #endregion
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
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() ?? false)
            { 
                this.FilePath = dialog.FileName;
            }
        }

        #endregion
        #endregion
    }
}

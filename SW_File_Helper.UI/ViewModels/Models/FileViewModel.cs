using Microsoft.Win32;
using SW_File_Helper.BL.Helpers;
using SW_File_Helper.Interfaces;
using SW_File_Helper.ViewModels.Base.Commands;
using System.IO;
using System.Text;
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
            OpenFileDialog dialog = new OpenFileDialog();
            string pathToFile = string.Empty;
            if (dialog.ShowDialog() ?? false)
            {
                pathToFile = dialog.FileName;
            }

            if (this.GetType().Name.Equals(nameof(ListViewFileViewModel)))
            {
                this.FilePath = pathToFile;
            }
            else
            {
                this.FilePath = BuildFolderPath(pathToFile);
            }
        }

        #endregion

        private string BuildFolderPath(string pathToFile)
        {
            StringBuilder res = new StringBuilder();

            var arr = pathToFile.Split(Path.DirectorySeparatorChar).ToList();
            arr.RemoveAt(arr.Count - 1);

            for (int i = 0; i < arr.Count; i++)
            {
                res.Append(arr[i]);

                if (i < arr.Count - 1)
                {
                    res.Append(Path.DirectorySeparatorChar);
                }
            }

            return res.ToString();
        }
        #endregion
    }
}

using SW_File_Helper.ViewModels.Base.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.ViewModels.Models
{
    public class FileViewModel : ViewModelBase
    {
        #region Fields
        private int m_ShowNumber;

        private string m_FilePath;

        private bool m_IsValid;

        private bool m_IsEnabled;
        #endregion

        #region IDataErrorInfo
        public override string this[string columnName]
        {
            get
            { 
                string error = string.Empty;

                switch (columnName)
                {
                    default:
                        break;
                }

                return error;
            }
        }
        #endregion

        #region Properties
        public int ShowNumber 
        { get => m_ShowNumber; set => Set(ref m_ShowNumber, value); }

        public bool IsEnabled 
        { get => m_IsEnabled; set => Set(ref m_IsEnabled, value); }

        public string FilePath 
        { get => m_FilePath; set=> Set(ref m_FilePath, value); }

        public bool IsValid { get => m_IsValid; private set => m_IsValid = value; }
        #endregion

        #region Ctor
        public FileViewModel(int showNumber) : this(showNumber, string.Empty, false)
        {
            
        }

        public FileViewModel(int shownumber, string filePath, bool isEnabled)
        {
            #region Fields Init

            m_ShowNumber = shownumber;
            m_IsEnabled = isEnabled;                    
            m_FilePath = filePath;

            #endregion
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{m_ShowNumber}) {FilePath} Enabled: {IsEnabled} IsValid: {IsValid}";
        }
        #endregion
    }
}

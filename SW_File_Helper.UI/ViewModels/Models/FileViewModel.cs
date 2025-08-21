using SW_File_Helper.ViewModels.Base.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.ViewModels.Models
{
    internal class FileViewModel : ViewModelBase
    {
        #region Fields
        private int m_ShowNumber;

        private string m_FilePath;

        private bool m_IsValid;
        #endregion

        #region Properties
        public int ShowNumber 
        { get => m_ShowNumber; set => Set(ref m_ShowNumber, value); }

        public string FilePath 
        { get => m_FilePath; set=> Set(ref m_FilePath, value); }

        public bool IsValid { get => m_IsValid; private set => m_IsValid = value; }
        #endregion

        #region Ctor
        public FileViewModel(int shownumber)
        {
            #region Fields Init

            m_ShowNumber = shownumber;

            #endregion
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{m_ShowNumber}) ";
        }
        #endregion
    }
}

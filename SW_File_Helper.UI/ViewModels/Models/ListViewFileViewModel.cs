using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.ViewModels.Models
{
    public class ListViewFileViewModel : FileViewModel
    {
        #region Fields
        ObservableCollection<CustomListViewItem> m_destFiles;

        private string m_CurrentAssembly;

        private string m_FileViewModelTypeName;
        #endregion

        #region Properties
        public string CurrentAssembly
        { get=>m_CurrentAssembly; set=>Set(ref m_CurrentAssembly, value); }

        public string FileViewModelTypeName
        { get => m_FileViewModelTypeName; set => Set(ref m_FileViewModelTypeName, value); }
        public ObservableCollection<CustomListViewItem> DestFiles 
        {
            get=>m_destFiles; set=>m_destFiles = value; 
        }
        #endregion

        #region Ctor
        public ListViewFileViewModel() : base()
        {
            Init();
        }

        #endregion

        #region Methods
        private void Init() 
        {
            m_destFiles = new ObservableCollection<CustomListViewItem>();

            m_CurrentAssembly = Assembly.GetEntryAssembly().Location;
            m_FileViewModelTypeName = typeof(FileViewModel).FullName;
        }

        public void Validate()
        { 
            bool destFilesAreValid = true;

            foreach (var item in m_destFiles)
            {
                if (!item.IsValid)
                { 
                    destFilesAreValid = false;
                    break;
                }
            }

            IsValid = destFilesAreValid;
        }
        #endregion
    }
}

using SW_File_Helper.ViewModels.Base.VM;
using SW_File_Helper.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.ViewModels.Views
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Fields
        private string m_title;

        private string m_currentAssembly;
        
        private string m_ListViewFileViewModelTypeName;

        private ObservableCollection<CustomListViewItem> m_files;
        #endregion

        #region Properties
        public ObservableCollection<CustomListViewItem> Files
        { get=>m_files; set=>m_files = value; }
        public string Title { get => m_title; set => Set(ref m_title, value); }
        public string CurrentAssembly 
        { get=>m_currentAssembly; set=>Set(ref m_currentAssembly, value); }       
        public string ListViewFileViewModel
        { get=>m_ListViewFileViewModelTypeName; set=>Set(ref m_ListViewFileViewModelTypeName, value); }
        #endregion

        #region Commands

        #endregion

        #region Ctor
        public MainWindowViewModel()
        {
            #region Field Initialization
            m_files = new ObservableCollection<CustomListViewItem>();
            m_title = "SW File Helper v 1.0.0.0";
            m_currentAssembly = Assembly.GetEntryAssembly().Location;
            m_ListViewFileViewModelTypeName = typeof(ListViewFileViewModel).FullName;
            #endregion
        }
        #endregion

        #region Methods

        #endregion
    }
}

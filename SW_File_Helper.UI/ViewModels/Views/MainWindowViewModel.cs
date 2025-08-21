using SW_File_Helper.ViewModels.Base.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.ViewModels.Views
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Fields
        private string m_title;
        #endregion

        #region Properties
        public string Title { get => m_title; set => Set(ref m_title, value); }
        #endregion

        #region Commands

        #endregion

        #region Ctor
        public MainWindowViewModel()
        {
            #region Field Initialization

            m_title = "SW File Helper v 1.0.0.0";

            #endregion
        }
        #endregion

        #region Methods

        #endregion
    }
}

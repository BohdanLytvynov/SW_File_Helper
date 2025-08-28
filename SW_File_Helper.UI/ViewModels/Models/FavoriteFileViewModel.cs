using SW_File_Helper.ViewModels.Base.Commands;
using SW_File_Helper.ViewModels.Base.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.ViewModels.Models
{
    public class FavoriteFileViewModel : FavoriteBaseViewModel
    {
        #region Fields
        private string m_path;
        #endregion

        #region Properties
        public string Path { get=> m_path; set => Set(ref m_path, value); }
        #endregion

        #region Ctor
        public FavoriteFileViewModel() : base()
        {
            m_path = string.Empty;
        }
        #endregion
    }
}

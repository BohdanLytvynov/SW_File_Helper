using SW_File_Helper.ViewModels.Base.VM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.ViewModels.Models
{
    public sealed class FavoriteListViewFileViewModel : FavoriteFileViewModel
    {
        #region Fields
        ObservableCollection<FavoriteFileViewModel> m_destPathes;
        #endregion

        #region Properties
        public ObservableCollection<FavoriteFileViewModel> DestPathes { get => m_destPathes; set => m_destPathes = value; }
        #endregion

        #region Ctor
        public FavoriteListViewFileViewModel()
        {
            m_destPathes = new ObservableCollection<FavoriteFileViewModel>();
        }
        #endregion

        #region Methods
        public void AddPath(FavoriteFileViewModel favoriteFileViewModel)
        { 
            if(favoriteFileViewModel == null) throw new ArgumentNullException(nameof(favoriteFileViewModel));

            favoriteFileViewModel.Number = DestPathes.Count + 1;
            DestPathes.Add(favoriteFileViewModel);
        }
        #endregion
    }
}

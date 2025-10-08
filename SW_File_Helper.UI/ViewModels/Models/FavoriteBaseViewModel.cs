using SW_File_Helper.ViewModels.Base.Commands;
using SW_File_Helper.ViewModels.Base.VM;
using System.Windows.Input;

namespace SW_File_Helper.ViewModels.Models
{
    public abstract class FavoriteBaseViewModel : ViewModelBase
    {
        #region Fields
        private int m_showNumber;

        private bool m_isSelected;
        #endregion

        #region Properties
        public Guid Id { get; set; }

        public int Number { get=>m_showNumber; set=>Set(ref m_showNumber, value); }

        public bool IsSelected 
        { get => m_isSelected; set => Set(ref m_isSelected, value); }
        #endregion
    }
}

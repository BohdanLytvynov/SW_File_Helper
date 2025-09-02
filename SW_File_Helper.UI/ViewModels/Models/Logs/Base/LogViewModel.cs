using SW_File_Helper.ViewModels.Base.VM;
using System.Windows;

namespace SW_File_Helper.ViewModels.Models.Logs.Base
{
    public class LogViewModel : ViewModelBase
    {
        #region fields
        private string m_text;

        private Style m_style;
        #endregion

        #region Properties
        public string Text { get => m_text; set => Set(ref m_text, value); }

        public Style Style { get => m_style; set => Set(ref m_style, value); }
        #endregion

        #region Ctor
        public LogViewModel(string text, Style style)
        {
            m_text = text;
            m_style = style;
        }
        #endregion

    }
}

using System.Windows;

namespace SW_File_Helper.ViewModels.Models.Logs.Base
{
    public class ConsoleMessageViewModel : LogViewModel
    {
        #region Fields
        private string m_clientIp;

        private Style m_clientIpStyle;
        #endregion

        #region Properties
        public string ClientIp 
        { get=> m_clientIp; set => Set(ref m_clientIp, value); }

        public Style ClientIpStyle
        { get=> m_clientIpStyle; set => Set(ref m_clientIpStyle, value); }
        #endregion

        #region Ctor
        public ConsoleMessageViewModel(string clientIp, Style clientIpStyle,
            string msg, Style msgStyle) : base(msg, msgStyle)
        {
            m_clientIp = clientIp;
            m_clientIpStyle = clientIpStyle;
        }
        #endregion
    }
}

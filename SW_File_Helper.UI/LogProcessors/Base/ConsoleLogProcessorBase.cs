using SW_File_Helper.BL.Loggers.Enums;
using SW_File_Helper.BL.LogProcessors.Base;
using SW_File_Helper.ViewModels.Models.Logs.Base;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Interop;

namespace SW_File_Helper.LogProcessors.Base
{
    internal class ConsoleLogProcessorBase : LogProcessorBase
    {
        protected ResourceDictionary m_resourceDictionary;

        public ConsoleLogProcessorBase() : base()
        {
            m_resourceDictionary = new ResourceDictionary();

            m_resourceDictionary.Source = new Uri("/SW_File_Helper;component/Resources/LoggerStyles.xaml", 
                UriKind.RelativeOrAbsolute);
        }

        public override object Process(string msg, LogType logType)
        {
            if (logType != LogType) return null;

            var type = LogType.ToString();

            var style = m_resourceDictionary[type] as Style;

            return new LogViewModel(type + ": " + msg, style);
        }
    }
}

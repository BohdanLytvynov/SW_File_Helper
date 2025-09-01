using SW_File_Helper.BL.Loggers.Enums;
using SW_File_Helper.BL.LogProcessors.Base;
using System.Windows;
using System.Windows.Documents;

namespace SW_File_Helper.LogProcessors.Base
{
    internal class ConsoleLogProcessorBase : LogProcessorBase
    {
        protected ResourceDictionary m_resourceDictionary;

        public ConsoleLogProcessorBase() : base()
        {
            m_resourceDictionary.Source = new Uri("/SW_File_Helper;component/Resources/LoggerStyles.xaml", 
                UriKind.RelativeOrAbsolute);
        }

        public override object Process(string message, LogType type)
        {
            throw new NotImplementedException();
        }
    }
}

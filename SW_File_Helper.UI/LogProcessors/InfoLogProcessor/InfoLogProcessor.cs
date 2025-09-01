using SW_File_Helper.BL.Loggers.Enums;
using SW_File_Helper.LogProcessors.Base;
using System.Windows;
using System.Windows.Documents;

namespace SW_File_Helper.LogProcessors.InfoLogProcessor
{
    internal class InfoLogProcessor : ConsoleLogProcessorBase, IInfoLogProcessor
    {
        public InfoLogProcessor() : base()
        {
            LogType = LogType.Information;
        }

        public override object Process(string msg, LogType logType)
        {
            if (logType != LogType.Information) return null;

            List<Paragraph> paragraphs = new List<Paragraph>();

            Paragraph paragraph = new Paragraph();

            Run info = new Run("Information:");
            info.Style = m_resourceDictionary["Info"] as Style;
            Run message = new Run(" " + msg);
            message.Style = m_resourceDictionary["Info"] as Style;

            paragraph.Inlines.Add(info);
            paragraph.Inlines.Add(message);
            
            paragraphs.Add(paragraph);

            return paragraphs;
        }
    }
}

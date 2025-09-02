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
    }
}

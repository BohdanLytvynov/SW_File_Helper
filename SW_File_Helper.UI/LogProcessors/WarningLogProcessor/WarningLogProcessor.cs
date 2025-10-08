using SW_File_Helper.LogProcessors.Base;
using SW_File_Helper.BL.Loggers.Enums;

namespace SW_File_Helper.LogProcessors.WarningLogProcessor
{
    internal class WarningLogProcessor : ConsoleLogProcessorBase, IWarningLogProcessor
    {
        public WarningLogProcessor()
        {
            LogType = LogType.Warning;
        }
    }
}

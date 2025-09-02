using SW_File_Helper.BL.Loggers.Enums;
using SW_File_Helper.LogProcessors.Base;

namespace SW_File_Helper.LogProcessors.DebugLogProcessor
{
    internal class DebugLogProcessor : ConsoleLogProcessorBase, IDebugLogProcessor
    {
        public DebugLogProcessor() : base()
        {
            LogType = LogType.Debug;
        }
    }
}

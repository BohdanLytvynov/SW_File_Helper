using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.LogProcessors.DebugLogProcessor;
using SW_File_Helper.LogProcessors.ErrorLogProcessor;
using SW_File_Helper.LogProcessors.InfoLogProcessor;
using SW_File_Helper.LogProcessors.OkLogProcessor;
using SW_File_Helper.LogProcessors.WarningLogProcessor;

namespace SW_File_Helper.Loggers
{
    public class ConsoleLogger : Logger, IConsoleLogger
    {
        public ConsoleLogger() : base()
        {
            base.LogProcessors.Add(new InfoLogProcessor());
            base.LogProcessors.Add(new DebugLogProcessor());
            base.LogProcessors.Add(new WarningLogProcessor());
            base.LogProcessors.Add(new ErrorLogProcessor());
            base.LogProcessors.Add(new OkLogProcessor());
        }
    }
}

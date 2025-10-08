using SW_File_Helper.BL.Loggers.Enums;

namespace SW_File_Helper.BL.LogProcessors.Base
{
    public abstract class LogProcessorBase : ILogProcessor
    {
        public LogType LogType { get; init; }

        public LogProcessorBase()
        {
            LogType = LogType.None;
        }

        public abstract object Process(string message, LogType type);
    }
}

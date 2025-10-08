using SW_File_Helper.BL.Loggers.Enums;

namespace SW_File_Helper.BL.LogProcessors.Base
{
    public interface ILogProcessor
    {
        object Process(string message, LogType type);

        public LogType LogType { get; init; }
    }
}

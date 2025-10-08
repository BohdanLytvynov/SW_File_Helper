using SW_File_Helper.BL.Loggers.Enums;
using SW_File_Helper.BL.LogProcessors.Base;

namespace SW_File_Helper.BL.Loggers.Base
{
    public interface ILogger
    {
        event Action<object, string, LogType> OnLogProcessed;
        public List<ILogProcessor> LogProcessors { get; set; }
        void Info(string message);
        void Warn(string message);
        void Error(string message);
        void Debug(string message);
        void Ok(string message);
    }
}

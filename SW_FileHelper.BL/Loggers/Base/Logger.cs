using SW_File_Helper.BL.Loggers.Enums;
using SW_File_Helper.BL.LogProcessors.Base;

namespace SW_File_Helper.BL.Loggers.Base
{
    public abstract class Logger : ILogger
    {
        public List<ILogProcessor> LogProcessors { get; set; }

        public event Action<object, string, LogType> OnLogProcessed;

        public Logger()
        {
            LogProcessors = new List<ILogProcessor>();
        }

        public void Debug(string message)
        {
            Process(message, LogType.Debug);
        }

        public void Error(string message)
        {
            Process(message, LogType.Error);
        }

        public void Info(string message)
        {
            Process(message, LogType.Information);
        }

        public void Warn(string message)
        {
            Process(message, LogType.Warning);
        }

        public void Ok(string message)
        {
            Process(message, LogType.Ok);
        }

        protected void Process(string message, LogType logType)
        {
            object? result = null;
            foreach (var processor in LogProcessors)
            {
                result = processor.Process(message, logType);

                if (result != null)
                    OnLogProcessed?.Invoke(result, message, logType);
            }
        }
    }
}

using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.LogProcessors.InfoLogProcessor;
using System.Windows.Documents;

namespace SW_File_Helper.Loggers
{
    public class ConsoleLogger : Logger, IConsoleLogger
    {
        public ConsoleLogger() : base()
        {
            base.LogProcessors.Add(new InfoLogProcessor());
        }
    }
}

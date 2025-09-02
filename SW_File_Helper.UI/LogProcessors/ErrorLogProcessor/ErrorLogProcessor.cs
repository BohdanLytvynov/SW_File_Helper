using SW_File_Helper.LogProcessors.Base;

namespace SW_File_Helper.LogProcessors.ErrorLogProcessor
{
    internal class ErrorLogProcessor : ConsoleLogProcessorBase, IErrorLogProcessor
    {
        public ErrorLogProcessor()
        {
            LogType = BL.Loggers.Enums.LogType.Error;
        }
    }
}

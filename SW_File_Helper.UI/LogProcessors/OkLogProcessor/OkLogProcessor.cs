using SW_File_Helper.LogProcessors.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.LogProcessors.OkLogProcessor
{
    internal class OkLogProcessor : ConsoleLogProcessorBase, IOkLogProcessor
    {
        public OkLogProcessor()
        {
            LogType = BL.Loggers.Enums.LogType.Ok;
        }
    }
}

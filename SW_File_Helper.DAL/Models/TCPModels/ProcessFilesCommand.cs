using SW_File_Helper.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.DAL.Models.TCPModels
{
    public class ProcessFilesCommand : Command
    {
        public ProcessFilesCommand() : base()
        {
            Dest = new List<string>();
            Text = Constants.PROCESS_FILES_COMMAND;
            NewFileExtension = string.Empty;
        }

        public string Src { get; set; }

        public string NewFileExtension { get; set; }

        public List<string> Dest { get; set; }
    }
}

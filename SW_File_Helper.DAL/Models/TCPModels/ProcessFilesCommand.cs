using SW_File_Helper.Common;

namespace SW_File_Helper.DAL.Models.TCPModels
{
    public class ProcessFilesCommand : Command
    {
        public ProcessFilesCommand() : base()
        {
            CommandType = nameof(ProcessFilesCommand);
            Dest = new List<string>();
            Text = Constants.PROCESS_FILES_COMMAND;
            NewFileExtension = string.Empty;
            Src = string.Empty;
        }

        public string Src { get; set; }

        public string NewFileExtension { get; set; }

        public List<string> Dest { get; set; }
    }
}

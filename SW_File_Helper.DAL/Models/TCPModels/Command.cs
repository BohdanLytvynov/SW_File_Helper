using SW_File_Helper.DAL.Models.TCPModels.Enums;

namespace SW_File_Helper.DAL.Models.TCPModels
{
    public abstract class Command
    {
        public string CommandType { get; set; }
        public string Text { get; set; }

        public Command()
        {
            Text = string.Empty;
            CommandType = string.Empty;
        }
    }
}

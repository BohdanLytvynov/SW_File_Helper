using SW_File_Helper.DAL.Models.TCPModels.Enums;

namespace SW_File_Helper.DAL.Models.TCPModels
{
    public abstract class Command
    {
        public string Text { get; set; }

        public Command()
        {
            Text = string.Empty;
        }
    }
}

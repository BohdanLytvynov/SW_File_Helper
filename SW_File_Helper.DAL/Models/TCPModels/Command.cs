using SW_File_Helper.DAL.Models.TCPModels.Enums;

namespace SW_File_Helper.DAL.Models.TCPModels
{
    public abstract class Command : Message
    {
        public Command()
        {
            MessageType = MessageType.Command;
            Text = string.Empty;
        }
    }
}

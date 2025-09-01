using SW_File_Helper.DAL.Models.TCPModels.Enums;

namespace SW_File_Helper.DAL.Models.TCPModels
{
    public class Message
    {
        public string Text { get; set; }
        public MessageType MessageType { get; protected set; }
        public Message()
        {
            MessageType = MessageType.Message;
            Text = string.Empty;
        }
    }
}

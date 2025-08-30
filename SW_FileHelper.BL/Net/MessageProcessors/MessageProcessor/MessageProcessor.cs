using Newtonsoft.Json.Linq;
using SW_File_Helper.BL.Net.MessageProcessors.Base;
using SW_File_Helper.DAL.Models.TCPModels;
using SW_File_Helper.DAL.Models.TCPModels.Enums;

namespace SW_File_Helper.BL.Net.MessageProcessors.MessageProcessor
{
    public class MessageProcessor : MessageProcessorBase
    {
        public MessageProcessor() : base()
        {
            
        }

        protected override void ProcessInternal(JObject obj, string msg)
        {
            var type = GetMessageType(obj);

            switch (type)
            {
                case MessageType.Message:
                        OnProcessed?.Invoke(new Message() { Text = obj["Text"].ToString() });
                        return;
                default:
                    CallNextProcessor(msg);
                    break;
            }
        }
    }
}

using Newtonsoft.Json.Linq;
using SW_File_Helper.DAL.Models.TCPModels;
using SW_File_Helper.DAL.Models.TCPModels.Enums;

namespace SW_File_Helper.BL.Net.MessageProcessors.Base
{
    public abstract class MessageProcessorBase : IMessageProcessor
    {
        public string CommandText { get; }

        public IMessageProcessor? Next {get; set;}
        public Action<Message, string> OnProcessed { get; set; }

        protected MessageProcessorBase()
        {
            Next = null;
        }

        public void AddMessageProcessor(IMessageProcessor commandProcessor)
        {
            if(commandProcessor != null)
                Next = commandProcessor;
        }

        public void ProcessMessage(string msg, string clientIP)
        {
            JObject obj = JObject.Parse(msg);
            if (obj == null)
                throw new Exception("Unable to convert to JObject!");

            ProcessInternal(obj, msg, clientIP);
        }

        protected abstract void ProcessInternal(JObject obj,  string msg, string clientIp);

        protected void CallNextProcessor(string msg, string clientIp)
        {
            if (Next != null)
                Next.ProcessMessage(msg, clientIp);
            else
            {
                throw new Exception($"There is now message processor!");
            }
        }

        protected static MessageType GetMessageType(JObject obj)
        {
            var msgType = MessageType.None;

            msgType = Enum.Parse<MessageType>(obj["MessageType"].ToString());

            if (msgType == MessageType.None)
                throw new Exception("Unable to calculate message Type");
            return msgType;
        }
    }
}

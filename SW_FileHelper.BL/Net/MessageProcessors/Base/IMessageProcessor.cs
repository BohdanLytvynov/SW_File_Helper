using SW_File_Helper.DAL.Models.TCPModels;

namespace SW_File_Helper.BL.Net.MessageProcessors.Base
{
    public interface IMessageProcessor
    {
        public Action<Message> OnProcessed { get; set; }

        public string CommandText { get; }

        public IMessageProcessor? Next { get; set; }

        void AddMessageProcessor(IMessageProcessor commandProcessor);

        void ProcessMessage(string msg);
    }
}

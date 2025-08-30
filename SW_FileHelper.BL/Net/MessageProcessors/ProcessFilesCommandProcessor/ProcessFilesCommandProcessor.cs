using Newtonsoft.Json.Linq;
using SW_File_Helper.BL.Net.MessageProcessors.Base;
using SW_File_Helper.DAL.Models.TCPModels.Enums;
using SW_File_Helper.DAL.Models.TCPModels;
using SW_File_Helper.Common;

namespace SW_File_Helper.BL.Net.MessageProcessors.ProcessFilesCommandProcessors
{
    public class ProcessFilesCommandProcessor : MessageProcessorBase
    {
        public ProcessFilesCommandProcessor() : base()
        {
            
        }

        protected override void ProcessInternal(JObject obj, string msg)
        {
            var type = GetMessageType(obj);

            switch (type)
            {
                case MessageType.Command:
                    var command = obj["Text"].ToString();

                    if (command.Equals(Constants.PROCESS_FILES_COMMAND))
                    {
                        var dest = JArray.Parse(obj["Dest"].ToString());
                        List<string> files = new List<string>();

                        foreach (var item in dest)
                        {
                            files.Add(item.ToString());
                        }

                        OnProcessed?.Invoke(new ProcessFilesCommand()
                        {
                            Text = command,
                            Src = obj["Src"].ToString(),
                            Dest = files,
                        });
                    }
                    else
                    {
                        CallNextProcessor(msg);
                    }
                    break;
                default:
                    CallNextProcessor(msg);
                    break;
            }
        }
    }
}

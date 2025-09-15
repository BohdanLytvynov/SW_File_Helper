using Newtonsoft.Json.Linq;
using SW_File_Helper.BL.Net.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.Base;
using SW_File_Helper.DAL.Models.TCPModels;
using SW_File_Helper.DAL.Models.TCPModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.BL.Net.NetworkStreamProcessors.CommandStreamProcessors
{
    public class CommandStreamProcessor : NetworkStreamProcessorBase, ICommandStreamProcessor
    {
        public OnProcessed<JObject>? OnProcess { get; set; }

        public CommandStreamProcessor()
        {
            MessageType = MessageType.Command;
        }

        public override void Process(MessageType type, NetworkStream networkStream, string clientIp)
        {
            base.Process(type, networkStream, clientIp);

            int messageSize = GetDataSize(networkStream);
            byte[] buffer = new byte[messageSize];

            ReadBytes(networkStream, messageSize, buffer);

            OnProcess?.Invoke(JObject.Parse(Encoding.UTF8.GetString(buffer)), clientIp);
        }
    }
}

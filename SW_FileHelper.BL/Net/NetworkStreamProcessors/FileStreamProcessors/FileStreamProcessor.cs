using SW_File_Helper.BL.Net.NetworkStreamProcessors.Base;
using SW_File_Helper.DAL.Models.TCPModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.BL.Net.NetworkStreamProcessors.FileStreamProcessors
{
    public class FileStreamProcessor : NetworkStreamProcessorBase, IFileStreamProcessor
    {
        public override void Process(MessageType type, NetworkStream networkStream, string clientIp)
        {
            base.Process(type, networkStream, clientIp);


        }
    }
}

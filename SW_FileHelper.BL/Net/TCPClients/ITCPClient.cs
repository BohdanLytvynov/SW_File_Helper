using SW_File_Helper.BL.Net.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.BL.Net.TCPClients
{
    public interface ITCPClient : ITCPBase<TcpClient>
    {
        public int SendingBufferSize { get; set; }

        Task SendMessageAsync(string msg);

        Task SendFileAsync(FileStream fs);

        void SendMessage(string msg);
    }
}

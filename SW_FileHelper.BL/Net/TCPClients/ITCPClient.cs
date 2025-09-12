using SW_File_Helper.BL.Net.Base;
using System.Net.Sockets;

namespace SW_File_Helper.BL.Net.TCPClients
{
    public interface ITCPClient : ITCPBase<TcpClient>
    {
        public int SendingBufferSize { get; set; }

        Task SendMessageAsync(string msg);

        Task SendFileAsync(FileStream fs);

        void SendFile(string path);

        void SendMessage(string msg);
    }
}

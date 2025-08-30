using Newtonsoft.Json.Linq;
using SW_File_Helper.BL.Net.Base;
using System.Net.Sockets;
using System.Text;

namespace SW_File_Helper.BL.Net.TCPClients
{
    public sealed class TCPClient : TCPBase<TcpClient>, ITCPClient
    {
        public int SendingBufferSize { get; set; }

        public override void Dispose()
        {
            var instance = GetInstance();
            if (instance != null)
            {
                instance.Close();
                instance.Dispose();
            }
        }

        public override void Init()
        {
            var instance = GetInstance();

            if(instance == null)
                instance = new TcpClient(Endpoint);

            if(SendingBufferSize == 0)
                SendingBufferSize = 1024;
        }

        public async Task SendFileAsync(FileStream fs)
        {
            var tcpInstance = GetInstance();
            if (tcpInstance == null)
            {
                throw new Exception("Tcp Instance wasn't initialized!");
            }
        }

        public void SendMessage(string msg)
        {
            var tcpInstance = GetInstance();
            if (tcpInstance == null)
            {
                throw new Exception("Tcp Instance wasn't initialized!");
            }

            var networkStream = tcpInstance.GetStream();
            var bytesToSend = Encoding.UTF8.GetBytes(msg);

            networkStream.Write(bytesToSend, 0, bytesToSend.Length);
        }

        public async Task SendMessageAsync(string msg)
        {
            var tcpInstance = GetInstance();
            if (tcpInstance == null)
            {
                throw new Exception("Tcp Instance wasn't initialized!");
            }

            var networkStream = tcpInstance.GetStream();
            var bytesToSend = Encoding.UTF8.GetBytes(msg);

            await networkStream.WriteAsync(bytesToSend, 0, bytesToSend.Length);
        }
    }
}

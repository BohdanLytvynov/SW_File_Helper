using SW_File_Helper.DAL.Models.TCPModels.Enums;
using System.Net.Sockets;

namespace SW_File_Helper.BL.Net.NetworkStreamProcessors.Base
{
    public abstract class NetworkStreamProcessorBase : INetworkStreamProcessor
    {
        public INetworkStreamProcessor Next { get; set; }
        public MessageType MessageType { get; init; }
        public int RecieveBufferSize { get; set; }

        protected NetworkStreamProcessorBase()
        {
            MessageType = MessageType.None;
            RecieveBufferSize = 1024;
        }

        public virtual void Process(MessageType type, NetworkStream networkStream, string clientIp)
        {
            if(MessageType != type)
                Next?.Process(type, networkStream, clientIp);
            return;
        }

        protected virtual int GetDataSize(NetworkStream networkStream, int size = 4)
        {
            byte[] buffer = new byte[size];
            networkStream.Read(buffer, 0, size);
            return BitConverter.ToInt32(buffer);
        }

        protected virtual void ReadBytes(NetworkStream networkStream, int dataSize, byte[] buffer)
        {
            int BytesRead = 0;

            while (BytesRead < dataSize)
            {
                BytesRead = networkStream.Read(buffer, 0, dataSize - BytesRead);
            }
        }
    }
}

using SW_File_Helper.DAL.Models.TCPModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.BL.Net.NetworkStreamProcessors.Base
{
    public abstract class NetworkStreamProcessorBase : INetworkStreamProcessor
    {
        public INetworkStreamProcessor Next { get; set; }
        public MessageType MessageType { get; init; }

        public virtual void Process(MessageType type, NetworkStream networkStream, string clientIp)
        {
            if(Next!= null && MessageType != type)
                Next.Process(type, networkStream, clientIp);
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

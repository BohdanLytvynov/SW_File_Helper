using SW_File_Helper.DAL.Helpers;
using SW_File_Helper.DAL.Models.TCPModels.Enums;
using SW_File_Helper.DAL.Models.TCPModels.FileMetadata;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.BL.Extensions.NetworkStreams
{
    public static class NetworkStreamExtensions
    {
        public static void SendMessageSize(this NetworkStream networkStream, byte[] buffer)
        {
            byte[] sendBuffer = BitConverter.GetBytes(buffer.Length);
            networkStream.Write(sendBuffer, 0, sendBuffer.Length);
        }

        public static int ReadMessageSize(this NetworkStream networkStream, int dataSize = 4)
        {
            byte[] buffer = new byte[dataSize];
            networkStream.Read(buffer, 0, dataSize);
            return BitConverter.ToInt32(buffer);
        }

        public static void SendMessageType(this NetworkStream networkStream, MessageType messageType)
        {
            int typeInt = (int)messageType;
            byte[] sendBuffer = BitConverter.GetBytes(typeInt);
            networkStream.Write(sendBuffer, 0, sendBuffer.Length);
        }

        public static MessageType ReadMessageType(this NetworkStream networkStream)
        {
            byte[] buffer = new byte[4];
            networkStream.Read(buffer, 0, buffer.Length);
            return Enum.Parse<MessageType>(BitConverter.ToInt32(buffer).ToString());
        }

        public static void SendObject<T>(this NetworkStream networkStream, T obj)
        { 
            string jsonStr = JsonHelper.SerializeWithNonFormatting(obj);
            byte[] sendingBuffer = Encoding.UTF8.GetBytes(jsonStr);
            networkStream.SendMessageSize(sendingBuffer);
            networkStream.Write(sendingBuffer, 0, sendingBuffer.Length);
        }

        public static T GetObject<T>(this NetworkStream networkStream, int sizeOfObject, T obj)
        {
            byte[] buffer = new byte[sizeOfObject];
            networkStream.ReadBytes(sizeOfObject, buffer);
            string jsonString = Encoding.UTF8.GetString(buffer);
            return JsonHelper.DeSerialize(jsonString, obj);
        }

        public static void ReadBytes(this NetworkStream networkStream, int dataSize, byte[] buffer)
        {
            int BytesRead = 0;

            while (BytesRead < dataSize)
            {
                BytesRead = networkStream.Read(buffer, 0, dataSize - BytesRead);
            }
        }
    }
}

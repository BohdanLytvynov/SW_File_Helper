using Newtonsoft.Json.Linq;
using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.Base;
using SW_File_Helper.DAL.Helpers;
using SW_File_Helper.DAL.Models.TCPModels.Enums;
using SW_File_Helper.DAL.Models.TCPModels.FileMetadata;
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
        public ILogger Logger { get; set; }
        public string PathToTemp { get; init; }

        public FileStreamProcessor(ILogger logger, string pathToTemp)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            PathToTemp = pathToTemp;
            MessageType = MessageType.File;
        }

        public override void Process(MessageType type, NetworkStream networkStream, string clientIp)
        {
            base.Process(type, networkStream, clientIp);
            FileMetadata fileMetadata = null;
            try
            {
                int sizeOfMetadata = GetDataSize(networkStream);
                byte[] buffer = new byte[sizeOfMetadata];
                ReadBytes(networkStream, sizeOfMetadata, buffer);
                string jsonString = Encoding.UTF8.GetString(buffer);
                fileMetadata = JsonHelper.DeSerialize(jsonString, new FileMetadata());

                Logger?.Info($"Recieving file: {fileMetadata.FileName} using {fileMetadata.PacketCount} Packets");

                var packets = fileMetadata.PacketCount;
                int currentPacketCount = 0;
                int totalRecievedCount = 0;
                int position = 0;

                if (packets > 0)
                {
                    byte[] recieveBuffer = null;

                    using (var fs = File.Create(PathToTemp + Path.DirectorySeparatorChar + fileMetadata.FileName))
                    {
                        for (int i = 0; i < packets; i++)
                        {
                            currentPacketCount++;
                            totalRecievedCount += RecieveBufferSize;
                            recieveBuffer = new byte[RecieveBufferSize];
                            networkStream.Read(recieveBuffer, 0, recieveBuffer.Length);
                            fs.Position = position;
                            fs.Write(recieveBuffer, 0, recieveBuffer.Length);
                            position += RecieveBufferSize;
                        }
                    }

                    if (fileMetadata.PacketCount == currentPacketCount)
                    {
                        Logger.Ok($"File {fileMetadata.FileName} recieved successfuly. RecievedSize equals Estimated: {fileMetadata.FileSize == totalRecievedCount}");
                    }
                    else
                        Logger.Warn($"File {fileMetadata.FileName} recieved but Current Packet Count was: {currentPacketCount} and Estimated: {fileMetadata.PacketCount}");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Error during recieving {fileMetadata?.FileName} occured! Error: {ex}");
            }

        }
    }
}

using SW_File_Helper.BL.Extensions.NetworkStreams;
using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.Base;
using SW_File_Helper.DAL.Helpers;
using SW_File_Helper.DAL.Models.TCPModels.Enums;
using SW_File_Helper.DAL.Models.TCPModels.FileMetadata;
using System.Net.Sockets;

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
                int sizeOfMetadata = networkStream.ReadMessageSize();
                fileMetadata = networkStream.GetObject(sizeOfMetadata, new FileMetadata());

                Logger?.Info($"Recieving file: {fileMetadata.FileName} using {fileMetadata.PacketCount} Packets");

                var packets = fileMetadata.PacketCount;
                int currentPacketCount = 0;
                int totalRecievedCount = 0;
                int BytesRead = 0;

                if (packets > 0)
                {
                    byte[] recieveBuffer = null;
                    int packetSize = 0;
                    IOHelper.CreateDirectoryIfNotExists(PathToTemp);
                    using (var fs = File.Create(PathToTemp + Path.DirectorySeparatorChar + fileMetadata.FileName))
                    {
                        do
                        {
                            currentPacketCount++;
                            packetSize = networkStream.ReadMessageSize();
                            recieveBuffer = new byte[packetSize];
                            BytesRead = networkStream.Read(recieveBuffer, 0, recieveBuffer.Length);

                            if (BytesRead == 0)
                                break;

                            totalRecievedCount += BytesRead;
                            fs.Write(recieveBuffer, 0, recieveBuffer.Length);

                        } while (true);
                    }

                    if (fileMetadata.PacketCount == currentPacketCount && totalRecievedCount == fileMetadata.FileSize)
                    {
                        Logger.Ok($"File {fileMetadata.FileName} recieved successfuly. Recieved File Size equals Estimated: {fileMetadata.FileSize == totalRecievedCount}");
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

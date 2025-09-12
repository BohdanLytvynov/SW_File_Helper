using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.Base;
using SW_File_Helper.DAL.Models.TCPModels.Enums;
using System.Net.Sockets;

namespace SW_File_Helper.BL.Net.NetworkStreamProcessorWrappers.Base
{
    public class NetworkStreamProcessorWrapper : INetworkStreamProcessorWrapper
    {
        public ILogger Logger { get; set; }
        public INetworkStreamProcessor Processor { get; set; }

        public NetworkStreamProcessorWrapper(ILogger logger, INetworkStreamProcessor streamProcessor)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Processor = streamProcessor ?? throw new ArgumentNullException(nameof(streamProcessor));
        }

        public void ProcessNetworkStream(NetworkStream networkStream, string clientIp)
        {
            byte[] buffer = new byte[4];
            networkStream.Read(buffer, 0, buffer.Length);
            var type = Enum.Parse<MessageType>(BitConverter.ToInt32(buffer).ToString());

            Processor.Process(type, networkStream, clientIp);
        }
    }
}

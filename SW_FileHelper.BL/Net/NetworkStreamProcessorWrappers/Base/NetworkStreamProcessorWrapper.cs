using SW_File_Helper.BL.Extensions.NetworkStreams;
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
            var type = networkStream.ReadMessageType();
            Processor.Process(type, networkStream, clientIp);
        }
    }
}

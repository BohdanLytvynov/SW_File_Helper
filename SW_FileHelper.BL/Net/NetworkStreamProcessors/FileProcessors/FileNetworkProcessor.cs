using SW_File_Helper.BL.Net.NetworkStreamProcessors.Base;
using System.Net.Sockets;

namespace SW_File_Helper.BL.Net.NetworkStreamProcessors.FileProcessors
{
    public class FileNetworkProcessor : NetworkProcessorBase, IFileNetworkProcessor
    {
        public int BufferSize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void ProcessNetworkStream(NetworkStream networkStream, string clientIp)
        {
            throw new NotImplementedException();
        }
    }
}

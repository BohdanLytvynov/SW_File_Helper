using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.BL.Net.NetworkStreamProcessors.FileProcessors
{
    public class FileNetworkProcessor : IFileNetworkProcessor
    {
        public int BufferSize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void ProcessNetworkStream(NetworkStream networkStream)
        {
            throw new NotImplementedException();
        }
    }
}

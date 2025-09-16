using SW_File_Helper.BL.Factories.Base;
using SW_File_Helper.BL.Factories.CreationOptions;
using SW_File_Helper.BL.Loggers.Base;
using SW_File_Helper.BL.Net.TCPClients;

namespace SW_File_Helper.BL.Factories.TCPClientFactories
{
    public class TCPClientFactory : AbstractFactoryBase<ITCPClient, TCPClientCreationOptions>, ITCPClientFactory
    {
        public TCPClientFactory(ILogger logger, TCPClientCreationOptions creationOptions) 
            : base(logger, creationOptions)
        {
        }

        public override ITCPClient Create()
        {
            return new TCPClient(Logger, CreationOptions.ClientName);
        }
    }
}

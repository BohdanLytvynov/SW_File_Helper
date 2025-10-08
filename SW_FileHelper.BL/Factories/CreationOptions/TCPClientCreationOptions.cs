namespace SW_File_Helper.BL.Factories.CreationOptions
{
    public struct TCPClientCreationOptions
    {
        public string ClientName { get; init; }

        public TCPClientCreationOptions(string clientName)
        {
            ClientName = clientName;
        }
    }
}

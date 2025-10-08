namespace SW_File_Helper.DAL.Models.TCPModels.FileMetadata
{
    public class FileMetadata
    {
        public long FileSize { get; set; }
        public string FileName { get; set; }
        public int PacketCount { get; set; }

        public FileMetadata() : this(0, string.Empty, 0)
        { 
        
        }

        public FileMetadata(long fileSize, string fileName, int packetCount)
        {
            PacketCount = packetCount;
            FileSize = fileSize;
            FileName = fileName;
        }
    }
}

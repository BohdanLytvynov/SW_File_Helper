namespace SW_File_Helper.DAL.Models
{
    public class Settings
    {
        #region Properties

        public string? FileExtensionForReplace { get; set; }

        public bool EnableRemoteMode { get; set; }

        public string? HostIPAddress { get; set; }

        public int MessageListenerPort { get; set; }

        public int FileListenerPort { get; set; }

        #endregion

        #region Ctor
        public Settings()
        {
            FileExtensionForReplace = string.Empty;
            EnableRemoteMode = false;
            HostIPAddress = string.Empty;
            MessageListenerPort = 0;
            FileListenerPort = 0;
        }
        #endregion
    }
}

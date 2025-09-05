namespace SW_File_Helper.DAL.Models
{
    public class Settings
    {
        #region Properties

        public string? FileExtensionForReplace { get; set; }

        public string? HostIPAddress { get; set; }

        #endregion

        #region Ctor
        public Settings()
        {
            FileExtensionForReplace = string.Empty;
            HostIPAddress = string.Empty;
        }
        #endregion
    }
}

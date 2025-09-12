namespace SW_File_Helper.BL.Net.Base
{
    public delegate void OnProcessed<Tout>(Tout value, string clientIp);

    public interface INotifyOnProcessed<Tout>
    {
        public OnProcessed<Tout>? OnProcess { get; set; }
    }
}

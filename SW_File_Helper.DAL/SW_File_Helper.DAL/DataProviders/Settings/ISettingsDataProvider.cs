using SW_File_Helper.DAL.DataProviders.Base;

namespace SW_File_Helper.DAL.DataProviders.Settings
{
    public interface ISettingsDataProvider : IDataProvider
    {
        public SW_File_Helper.DAL.Models.Settings Settings { get; protected set; }
    }
}

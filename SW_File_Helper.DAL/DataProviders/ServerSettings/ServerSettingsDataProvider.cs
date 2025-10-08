using Newtonsoft.Json;
using SW_File_Helper.DAL.DataProviders.Base;
using SW_File_Helper.DAL.Helpers;
using SW_File_Helper.DAL.Models;

namespace SW_File_Helper.DAL.DataProviders.ServerSetting
{
    public class ServerSettingsDataProvider : DataProviderBase<ServerSettings>, IServerSettingsDataProvider
    {
        public ServerSettingsDataProvider()
        {
            WriterSettings = new JsonSerializerSettings();
            WriterSettings.Formatting = Formatting.Indented;
            ReaderSettings = WriterSettings;

            PathToFile += Path.DirectorySeparatorChar + "Settings.json";
            IOHelper.CreateFileIfNotExists(PathToFile);
        }
    }
}

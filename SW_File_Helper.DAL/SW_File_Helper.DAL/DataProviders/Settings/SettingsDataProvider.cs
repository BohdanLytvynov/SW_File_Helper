using Newtonsoft.Json;
using SW_File_Helper.DAL.DataProviders.Base;
using SW_File_Helper.DAL.Helpers;

namespace SW_File_Helper.DAL.DataProviders.Settings
{
    public sealed class SettingsDataProvider : DataProviderBase<Models.Settings>, ISettingsDataProvider
    {
        #region Ctor
        public SettingsDataProvider() : base()
        {
            WriterSettings = new JsonSerializerSettings();
            WriterSettings.Formatting = Formatting.Indented;
            ReaderSettings = WriterSettings;

            PathToFile += Path.DirectorySeparatorChar + "Settings.json";
            IOHelper.CreateFileIfNotExists(PathToFile);
        }

        #endregion
    }
}

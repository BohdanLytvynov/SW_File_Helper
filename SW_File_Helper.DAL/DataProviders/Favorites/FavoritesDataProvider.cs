using Newtonsoft.Json;
using SW_File_Helper.DAL.DataProviders.Base;
using SW_File_Helper.DAL.DataProviders.JsonConverters;
using SW_File_Helper.DAL.Helpers;
using SW_File_Helper.DAL.Models;

namespace SW_File_Helper.DAL.DataProviders.Favorites
{
    public sealed class FavoritesDataProvider : DataProviderBase<List<ModelBase>>, IFavoritesDataProvider
    {
        public FavoritesDataProvider() : base()
        {
            ReaderSettings = new JsonSerializerSettings();
            ReaderSettings.Formatting = Formatting.None;
            ReaderSettings.Converters.Add(new FavoritesJsonConverter());

            WriterSettings = new JsonSerializerSettings();
            WriterSettings.Formatting = Formatting.None;

            PathToFile += Path.DirectorySeparatorChar + "Favorites.json";
            IOHelper.CreateFileIfNotExists(PathToFile);

        }
    }
}

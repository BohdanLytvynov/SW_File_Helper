using Newtonsoft.Json;

namespace SW_File_Helper.DAL.Helpers
{
    public static class JsonHelper
    {
        public static string SerializeWithNonFormatting(object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Formatting = Formatting.None;

            return JsonConvert.SerializeObject(value, settings);
        }

        public static string Serialize(object value, JsonSerializerSettings settings = null)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (settings == null)
            {
                return JsonConvert.SerializeObject(value);
            }
            else 
            {
                return JsonConvert.SerializeObject(value, settings);
            }
        }

        public static T? DeSerialize<T>(string value, T obj, JsonSerializerSettings settings = null)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            if (settings == null)
            {
                return JsonConvert.DeserializeAnonymousType(value, obj);
            }
            else
            {
                return JsonConvert.DeserializeAnonymousType(value, obj, settings);
            }
            
        }
    }
}

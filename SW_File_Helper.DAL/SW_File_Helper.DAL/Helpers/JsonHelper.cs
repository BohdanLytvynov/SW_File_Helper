using Newtonsoft.Json;

namespace SW_File_Helper.DAL.Helpers
{
    public static class JsonHelper
    {
        public static string Serialize(object value, Formatting formatting = Formatting.Indented)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return JsonConvert.SerializeObject(value, formatting);
        }

        public static T? DeSerialize<T>(string value, T obj)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            return JsonConvert.DeserializeAnonymousType<T>(value, obj);
        }
    }
}

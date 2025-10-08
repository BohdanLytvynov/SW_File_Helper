using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SW_File_Helper.DAL.Models;

namespace SW_File_Helper.DAL.DataProviders.JsonConverters
{
    public class FavoritesJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override bool CanRead => true;

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            List<ModelBase> models = new List<ModelBase>();

            JArray jArray = JArray.Load(reader);

            foreach (var obj in jArray)
            {
                Guid id = Guid.Parse(obj["Id"].ToString());
                string typeName = obj["TypeName"].ToString();

                switch (typeName)
                {
                    case nameof(DestPathModel):
                        models.Add(new DestPathModel()
                        {
                            Id = id,
                            PathToFile = obj["PathToFile"].ToString()
                        });
                        break;
                    case nameof(FileModel):
                        JArray array = (JArray)obj["PathToDst"];
                        List<string> destinations = new List<string>();

                        foreach (var item in array)
                        {
                            destinations.Add(item.ToString());
                        }

                        models.Add(new FileModel()
                        {
                            Id = id,
                            PathToFile = obj["PathToFile"].ToString(),
                            PathToDst = destinations
                        });
                        break;
                    case nameof(IPAddressFavorites):
                        models.Add(new IPAddressFavorites() { Id = id, 
                            IPAddress = obj["IPAddress"].ToString() });

                        break;
                    default:
                        throw new NotSupportedException($"Unknown type for DeSerialization! Type name is: {objectType.Name}");
                }
            }

            return models;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

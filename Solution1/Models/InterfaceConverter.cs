using Newtonsoft.Json;

namespace PizzaStore.Models;

// this class will help json.NET to deserialize into the Ingredient abstract class
public class InterfaceConverter<T> : JsonConverter
{
    public override bool CanConvert(Type objectType) => true;

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        return serializer.Deserialize<T>(reader);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }
}
using Newtonsoft.Json;

namespace PizzaStore.Models.Security;

public static class MessageInspector
{
    private static bool IsValidJson<T>(string jsonString)
    {
        if (!(jsonString.StartsWith('{') && jsonString.EndsWith('}'))) return false;
        try {
            T? result = JsonConvert.DeserializeObject<T>(jsonString);
            return result != null;
        } catch (JsonReaderException jsE) {
            Console.WriteLine(jsE);
            return false;
        }
    }

    public static T ParseJson<T>(string jsonString)
    {
        if (typeof(T) == typeof(Customer)) {
            if (jsonString.StartsWith("{\"OrderCustomer\":")) {
                jsonString = jsonString.Substring("{\"OrderCustomer\":".Length);
                jsonString = jsonString.Remove(jsonString.IndexOf("}", StringComparison.Ordinal), 1);
            }
        }
        if (!IsValidJson<T>(jsonString)) throw new JsonReaderException("Error reading json string");
        return JsonConvert.DeserializeObject<T>(jsonString);
    }
}
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;

namespace PizzaStore.Models;

public static class EnumHelper
{
    public static HtmlString EnumToString<T>()
    {
        IEnumerable<int> values = Enum.GetValues(typeof(T)).Cast<int>();
        Dictionary<string, int> enumDictionary = values.ToDictionary(value => Enum.GetName(typeof(T), value));
        return new HtmlString(JsonConvert.SerializeObject(enumDictionary));
    }
}
using Newtonsoft.Json;

namespace PizzaStore.Models;

// Singleton pattern
public class Customer
{
    private static Customer Instance = null;
    public string Name { get; }
    public string City { get; }
    public string Street { get; }
    public string HouseNumber { get; }
    public string ZipCode { get; }

    [JsonConstructor]
    private Customer(string name, string city, string street, string houseNumber, string zipCode)
    {
        this.Name = name;
        this.City = city;
        this.Street = street;
        this.HouseNumber = houseNumber;
        this.ZipCode = zipCode;
    }
    
    public static Customer GetInstance(string name, string city, string street, string houseNumber, string zipCode)
    {
        return Instance = new Customer(name, city, street, houseNumber, zipCode);
    }

    public static Customer GetInstance()
    {
        if (Instance == null) throw new Exception("Customer has not been initialized, consider passing some arguments.");
        return Instance;
    }

    public static Customer FromJson(string json)
    {
        if (json.StartsWith("{\"OrderCustomer\":")) {
            json = json.Substring("{\"OrderCustomer\":".Length);
            json = json.Remove(json.IndexOf("}", StringComparison.Ordinal), 1);
        }
        
        return JsonConvert.DeserializeObject<Customer>(json);
    }

    public override string ToString()
    {
        return $"{this.Name}\n{this.Street} {this.HouseNumber}\n{this.ZipCode} {this.City}";
    }
}
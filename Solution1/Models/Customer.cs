using Newtonsoft.Json;
using PizzaStore.Models.Security;

namespace PizzaStore.Models;

// Singleton pattern
public class Customer
{
    public string Name { get; private set; }
    public string City { get; private set; }
    public string Street { get; private set; }
    public string HouseNumber { get; private set; }
    public string ZipCode { get; private set; }
    
    [JsonConstructor]
    public Customer(string name, string city, string street, string houseNumber, string zipCode)
    {
        this.Name = name;
        this.City = city;
        this.Street = street;
        this.HouseNumber = houseNumber;
        this.ZipCode = zipCode;
    }

    public void SetName(string name)
    {
        this.Name = name;
    }

    public void SetAddress(string city, string street, string houseNumber, string zipCode)
    {
        this.City = city;
        this.Street = street;
        this.HouseNumber = houseNumber;
        this.ZipCode = zipCode;
    }

    public override string ToString()
    {
        return $"{this.Name}\n{this.Street} {this.HouseNumber}\n{this.ZipCode} {this.City}";
    }
}
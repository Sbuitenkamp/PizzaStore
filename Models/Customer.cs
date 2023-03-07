namespace PizzaStore.Models;

public class Customer
{
    private string Name, City, Street, HouseNumber, ZipCode;

    public Customer(string name, string city, string street, string houseNumber, string zipCode)
    {
        this.Name = name;
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
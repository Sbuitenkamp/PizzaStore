namespace PizzaStore.Models;

public class Pizza
{
    private string Name;
    private List<Topping> Toppings;

    public Pizza(string name)
    {
        this.Name = name;
        // every pizza has these base ingredients
        Toppings = new List<Topping>
        {
            new Topping("Bodem", 1),
            new Topping("Tomatensaus", 1),
            new Topping("Kaas", 1)
        };
    }

    // builder design pattern
    public void AddTopping(Topping topping)
    {
        this.Toppings.Add(topping);
    }

    public override string ToString()
    {
        string result = $"Pizza: {this.Name}\n Toppings:\n";
        foreach (Topping topping in this.Toppings) result += $"  {topping.Name} x{topping.Amount}\n";
        return result;
    }
}
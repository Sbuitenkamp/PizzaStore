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
    
    public void BuildPizza(List<Topping> toppings)
    { 
        this.Toppings.AddRange(toppings);
        
    }

    // builder design pattern
    public void AddTopping(Topping topping)
    {
        Topping hasTopping = Toppings.Find(x => x.Name == topping.Name); // reference
        if (hasTopping != null) hasTopping.AddOne();
        else this.Toppings.Add(topping);
    }

    public void RemoveTopping(string toppingName)
    {
        Topping hasTopping = Toppings.Find(x => x.Name == toppingName); // reference
        if (hasTopping != null) hasTopping.MinusOne();
        if (hasTopping.Amount == 0) Toppings.Remove(Toppings.Find(x => x.Name == toppingName));
    }

    public override string ToString()
    {
        string result = $"Pizza: {this.Name}\n Toppings:\n";
        foreach (Topping topping in this.Toppings) result += $"  {topping.Name} x{topping.Amount}\n";
        return result;
    }
}
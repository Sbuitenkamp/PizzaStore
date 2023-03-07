namespace PizzaStore.Models;

public class Topping
{
    public string Name { get; }
    public int Amount { get; }

    public Topping(string name, int amount)
    {
        this.Name = name;
        this.Amount = amount;
    }
}
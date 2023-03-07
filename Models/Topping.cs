namespace PizzaStore.Models;

public class Topping
{
    public string Name { get; }
    public int Amount { get; private set; }

    public Topping(string name, int amount)
    {
        this.Name = name;
        this.Amount = amount;
    }

    public void AddOne()
    {
        this.Amount++;
    }

    public void MinusOne()
    {
        this.Amount--;
    }
}
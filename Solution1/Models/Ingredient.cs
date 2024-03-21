using Newtonsoft.Json;

namespace PizzaStore.Models;

public abstract class Ingredient
{
    public string Name { get; }
    public int Amount { get; protected set; }

    protected Ingredient(string name, int amount)
    {
        this.Name = name;
        this.Amount = amount;
    }

    public void AddAmount(int amount)
    {
        if (this is Topping) this.Amount += amount;
    }
    
    public void AddOne()
    {
        if (this is Topping) this.Amount++;
    }

    public void MinusOne()
    {
        if (this is Topping) this.Amount--;
    }
}
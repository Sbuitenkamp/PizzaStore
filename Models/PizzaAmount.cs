namespace PizzaStore.Models;

public class PizzaAmount
{
    public int Amount { get; set; }
    public Pizza PizzaType { get; set; }

    public PizzaAmount(int amount, Pizza pizza)
    {
        if (amount == 0) amount = 1;
        this.Amount = amount;
        this.PizzaType = pizza;
    }
}
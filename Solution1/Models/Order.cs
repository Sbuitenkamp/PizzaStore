using Newtonsoft.Json;

namespace PizzaStore.Models;

public class Order
{
    public List<PizzaAmount> Pizzas { get; private set; }
    public DateTime TimeOfOrder { get; private set; }
    public Customer Customer { get; set; }

    [JsonConstructor]
    public Order(List<PizzaAmount> pizzas)
    {
        this.TimeOfOrder = DateTime.Now;
        this.Pizzas = pizzas;
    }

    public Order()
    {
        this.TimeOfOrder = DateTime.Now;
        this.Pizzas = new List<PizzaAmount>(); 
    }
    public Order(Customer customer): this()
    {
        this.Customer = customer;
    }

    public void AddPizza(Pizza pizza, int amount = 1) // optional parameter
    {
        PizzaAmount amountOfPizzas = new PizzaAmount(amount, pizza);
        this.Pizzas.Add(amountOfPizzas);
    }

    public void AddPizzas(IEnumerable<PizzaAmount> pizzas)
    {
        this.Pizzas.AddRange(pizzas);
    }

    public override string ToString()
    {
        string result = "";
        foreach (PizzaAmount pizzaAmount in Pizzas) result += pizzaAmount.Amount + " " + pizzaAmount.PizzaType.ToString() + "\n";
        result += "Besteld op:" + TimeOfOrder;
        return result;
    }
}
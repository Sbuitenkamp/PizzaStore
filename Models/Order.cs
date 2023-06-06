namespace PizzaStore.Models;

public class Order
{
    public Customer OrderCustomer { get; private set; }
    public List<PizzaAmount> Pizzas { get; private set; }
    public DateTime TimeOfOrder { get; private set; }
    
    public Order() {}
    public Order(Customer customer)
    {
        this.OrderCustomer = customer;
        this.TimeOfOrder = DateTime.Now;
        this.Pizzas = new List<PizzaAmount>();
    }

    public void AddPizza(Pizza pizza, int amount = 1) // optional parameter
    {
        PizzaAmount amountOfPizzas = new PizzaAmount(amount, pizza);
        this.Pizzas.Add(amountOfPizzas);

    }

    public override string ToString()
    {
        string result = "";
        foreach (PizzaAmount pizzaAmount in Pizzas) result += pizzaAmount.Amount + " " + pizzaAmount.PizzaType.ToString() + "\n";
        result += "Besteld op:" + TimeOfOrder;
        return result;
    }
}
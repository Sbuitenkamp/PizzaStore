namespace PizzaStore.Models;

public class Order
{
    private Customer OrderCustomer;
    private List<Pizza> Pizzas;

    public Order(Customer customer)
    {
        this.OrderCustomer = customer;
        this.Pizzas = new List<Pizza>();
    }

    public void AddPizza(Pizza pizza)
    {
        this.Pizzas.Add(pizza);
    }

    public override string ToString()
    {
        string result = this.OrderCustomer.ToString() + "\n";
        foreach (Pizza pizza in Pizzas) result += pizza.ToString();
        return result;
    }
}
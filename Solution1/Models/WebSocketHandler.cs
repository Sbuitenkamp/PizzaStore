using System.Net.WebSockets;
using System.Text;
using Newtonsoft.Json;
using PizzaStore.Models.Security;

namespace PizzaStore.Models;

public class WebSocketHandler
{
    private static WebSocketHandler? _instance;

    public static WebSocketHandler Instance
    {
        get
        {
            _instance ??= new WebSocketHandler();
            return _instance;
        }
    }
    
    private WebSocketHandler() {}

    public Order ReceiveMessage(string jsonString, Order? currentOrder)
    {
        if (currentOrder == null) {
            Customer customer = MessageInspector.ParseJson<Customer>(jsonString);
            currentOrder = new Order(customer);
        } else {
            Order tempOrder = MessageInspector.ParseJson<Order>(jsonString);
            currentOrder.AddPizzas(tempOrder.Pizzas);
        }

        return currentOrder;
    }
}
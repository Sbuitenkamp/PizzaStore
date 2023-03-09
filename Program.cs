using System;
using System.Net;
using PizzaStore.Data;
using PizzaStore.Models;

namespace PizzaStore
{
    class PizzaStore
    {
        public static void Main(string[] args)
        {
            // test data
            // todo send this via HTTP
            Customer customer = Customer.GetInstance("TestCustomer", "Leeuwarden", "Rengerslaan", "10", "8917 DD");
            Pizza pizza = new Pizza(PizzaName.Tonno, 1);
            pizza.VisitLibrary();
            Order order = new Order(customer);
            order.AddPizza(pizza);
            
            // Console.WriteLine(order.ToString());
            // WebServer.Init();
            WebServer.TcpConnect();
        }
    }
    
}
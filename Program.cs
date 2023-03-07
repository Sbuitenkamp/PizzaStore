using System;
using System.Net;
using PizzaStore.Models;

namespace PizzaStore
{
    class PizzaStore
    {
        public static void Main(string[] args)
        {
            // // test data
            // Customer customer = new Customer("TestCustomer", "Leeuwarden", "Rengerslaan", "10", "8917 DD");
            // List<Topping> tonno = new List<Topping>
            // {
            //     new Topping("Tonijn", 3),
            //     new Topping("Ui", 1),
            // };
            // Pizza pizza = new Pizza("Tonno");
            // pizza.BuildPizza(tonno);
            // pizza.RemoveTopping("Tonijn");
            // Order order = new Order(customer);
            // order.AddPizza(pizza);
            //
            // Console.WriteLine(order.ToString());
            WebServer.Init();
        }
    }
}
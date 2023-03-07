using PizzaStore.Models;

// test data
Customer customer = new Customer("TestCustomer", "Leeuwarden", "Rengerslaan", "10", "8917 DD");
Pizza pizza = new Pizza("Tonno");
pizza.AddTopping(new Topping("Tonijn", 1));
Order order = new Order(customer);
order.AddPizza(pizza);

Console.WriteLine(order.ToString());

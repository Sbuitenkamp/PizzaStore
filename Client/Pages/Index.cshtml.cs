using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using PizzaStore.Data;
using PizzaStore.Models;

namespace PizzaStore.Pages;

[BindProperties]
public class IndexModel : PageModel
{
    private readonly IMemoryCache _cache;
    public Order CurrentOrder { get; set; }
    
    public bool Order { get; set; }
    
    [BindProperty]
    public string Name { get; set; }
    [BindProperty]
    public string Street { get; set; }
    [BindProperty]
    public string HouseNumber { get; set; }
    [BindProperty]
    public string ZipCode { get; set; }
    [BindProperty]
    public string Town { get; set; }
    [BindProperty]
    public int PizzaName { get; set; }
    
    public IndexModel(IMemoryCache cache)
    {
        _cache = cache;
    }
    
    public void OnGet() { }

    public void OnPostCustomer()
    {
        Customer customer = Customer.GetInstance(Name, Town, Street, HouseNumber, ZipCode);
        this.CurrentOrder = new Order(customer);
        this.Order = true;

        _cache.Set<Order>("Order", this.CurrentOrder);
    }

    public void OnPostAddPizza(int amount)
    {
        this.CurrentOrder = _cache.Get<Order>("Order");
        this.Order = true;
        Pizza pizza = new Pizza((PizzaName)PizzaName);
        pizza.VisitLibrary();
        // todo add extra toppings
        this.CurrentOrder.AddPizza(pizza, amount);
        this.CurrentOrder = _cache.Set<Order>("Order", this.CurrentOrder);
    }
}
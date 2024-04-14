using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using PizzaStore.Data;
using PizzaStore.Models;

namespace PizzaStore.Pages;

[BindProperties]
[RequireHttps]
public class IndexModel : PageModel
{
    private readonly IMemoryCache _cache;
    private string _zipCode;
    private int _pizzaName;
    
    public Order CurrentOrder { get; set; }
    public bool Order { get; set; }
    
    [BindProperty]
    public string Name { get; set; }
    [BindProperty]
    public string Street { get; set; }
    [BindProperty]
    public string HouseNumber { get; set; }
    [BindProperty]
    public string ZipCode
    {
        get => _zipCode;
        set
        {
            // 1234 AB && 1234ab == true
            const string pattern = "^[0-9]{4}( ?)([A-z]{2})$";
            if (Regex.Match(value, pattern, RegexOptions.None).Success) _zipCode = value;
        }
    }
    [BindProperty]
    public string City { get; set; }
    [BindProperty]
    public int PizzaName
    {
        get => _pizzaName;
        set
        {
            if (value is >= 0 and <= 4) _pizzaName = value;
        } 
    }
    [BindProperty]
    public List<ToppingAmount> ExtraToppings { get; set; }
    
    public IndexModel(IMemoryCache cache)
    {
        _cache = cache;
    }
    
    public void OnGet() { }
    
    public void OnPostCustomer()
    {
        Customer customer = new Customer(Name, City, Street, HouseNumber, ZipCode);
        this.CurrentOrder = new Order(customer);
        this.Order = true;

        _cache.Set<Order>("Order", this.CurrentOrder);
    }
    
    public void OnPostAddPizza(int amount)
    {
        if (amount < 1) amount = 1;
        this.CurrentOrder = _cache.Get<Order>("Order") ?? new Order();
        this.Order = true;
        PizzaBuilder builder = new PizzaBuilder((PizzaName)PizzaName);
        builder.BuildPizza();
        
        foreach (ToppingAmount toppingAmount in ExtraToppings) {
            if (!toppingAmount.Add) continue;
            builder.AddTopping(toppingAmount.GetToppingName(), toppingAmount.Amount);
        }
        
        this.CurrentOrder.AddPizza(builder.FinishPizza(), amount);
        this.CurrentOrder = _cache.Set<Order>("Order", this.CurrentOrder);
    }
}
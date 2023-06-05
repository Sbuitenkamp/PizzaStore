using System.Diagnostics;
using PizzaStore.Data;

namespace PizzaStore.Models;

public class Pizza : Ingredient
{
    private List<Ingredient> Ingredients;
    private PizzaName Type;
    public Pizza(PizzaName name, int amount = 1) : base(name.ToString(), amount)
    {
        this.Type = name;
        // every pizza has these base ingredients
        Ingredients = new List<Ingredient>
        {
            new Dough(),
            new Sauce(),
            new Topping(ToppingNames.Kaas, 1)
        };
    }
    
    public void VisitLibrary()
    { 
        // visit the ingredient library and gather ingredients based on the pizza type
        this.Ingredients.AddRange(IngredientLibrary.GatherIngredients(this.Type));
    }

    // add extra toppings 
    public void AddTopping(ToppingNames topping)
    {
        Ingredient hasIngredient = Ingredients.Find(x => x.Name == topping.ToString());
        if (hasIngredient != null) hasIngredient.AddOne();
        else this.Ingredients.Add(new Topping(topping, 1));
    }

    public void RemoveTopping(ToppingNames topping)
    {
        Ingredient hasIngredient = Ingredients.Find(x => x.Name == topping.ToString()); // reference
        if (hasIngredient != null) hasIngredient.MinusOne();
        if (hasIngredient.Amount == 0) Ingredients.Remove(Ingredients.Find(x => x.Name == topping.ToString()));
    }

    public override string ToString()
    {
        string result = $"Pizza: {this.Name}\n Toppings:\n";
        foreach (Ingredient ingredient in this.Ingredients) result += $"  {ingredient.Name} x{ingredient.Amount}\n";
        return result;
    }
}
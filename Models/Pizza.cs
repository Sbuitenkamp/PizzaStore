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
    public void AddTopping(ToppingNames topping, int amount = 1)
    {
        Ingredient hasIngredient = Ingredients.Find(x => x.Name == topping.ToString());
        if (hasIngredient != null) {
           if (amount == 1) hasIngredient.AddOne();
           else hasIngredient.AddAmount(amount);
        } else this.Ingredients.Add(new Topping(topping, amount));
    }

    public void RemoveTopping(ToppingNames topping)
    {
        Ingredient hasIngredient = Ingredients.Find(x => x.Name == topping.ToString()); // reference
        if (hasIngredient != null) hasIngredient.MinusOne();
        if (hasIngredient.Amount == 0) Ingredients.Remove(Ingredients.Find(x => x.Name == topping.ToString()));
    }

    public override string ToString()
    {
        string result = $"{this.Name}\n\nToppings:";
        foreach (Ingredient ingredient in this.Ingredients) {
            if (ingredient is Dough) continue;
            result += $"  {ingredient.Name} x{ingredient.Amount}\n";
        }
        return result;
    }
}
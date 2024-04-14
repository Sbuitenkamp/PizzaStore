using System.Diagnostics;
using Newtonsoft.Json;
using PizzaStore.Data;

namespace PizzaStore.Models;

public class Pizza : Ingredient
{
    public List<Ingredient> Ingredients { get; }
    private PizzaName Type;

    [JsonConstructor]
    public Pizza(string name, List<Topping> ingredients, int amount = 1) : base(name, amount)
    {
        this.Type = (PizzaName)Enum.Parse(typeof(PizzaName), name);
        // every pizza has these base ingredients
        List<Ingredient> baseIngredients = new List<Ingredient>
        {
            new Dough(),
            new Sauce(),
            new Topping(ToppingNames.Kaas, 1)
        };
        this.Ingredients = baseIngredients;
        this.VisitLibrary();
        this.MergeToppings(ingredients);
    }

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

        this.VisitLibrary();
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

    public void VisitLibrary()
    {
        // visit the ingredient library and gather ingredients based on the pizza type
        this.Ingredients.AddRange(IngredientLibrary.GatherIngredients(this.Type));
    }

    public void MergeToppings(List<Topping> ingredients)
    {
        foreach (Topping ingredient in ingredients) {
            this.AddTopping((ToppingNames)Enum.Parse(typeof(ToppingNames), ingredient.Name), ingredient.Amount);
        }
    }
}
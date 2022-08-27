using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class Bartender : MonoBehaviour
{
    public IntReference maxDrinks;
    public DrinkDatabase drinkDatabase;
    private Cocktail cocktail = new Cocktail();

    [Button]
    public void AddDrink(Drink _drink)
    {
        if (CanAddDrink())
        {
            DrinkEntry drinkEntry = drinkDatabase.GetDrinkEntry(_drink);
            AddDrink(drinkEntry);
        }
    }

    public void ClearCocktail()
    {
        cocktail.Empty();
    }
    
    private void AddDrink(DrinkEntry _drink)
    {
        cocktail.AddDrink(_drink);
    }

    public Cocktail GetCocktail()
    {
        return cocktail;
    }

    public List<Drink> GetDrinks()
    {
        return cocktail.GetDrinks();
    }

    public bool CanAddDrink()
    {
        return cocktail.drinkList.Count < maxDrinks;
    }
}

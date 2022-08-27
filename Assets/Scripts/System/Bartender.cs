using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Bartender : MonoBehaviour
{
    public DrinkDatabase drinkDatabase;
    private Cocktail currentCocktail = new Cocktail();

    [Button]
    public void AddDrink(Drink _drink)
    {
        DrinkEntry drinkEntry = drinkDatabase.GetDrinkEntry(_drink);
        currentCocktail.AddDrink(drinkEntry);
    }
    [Button]
    public void AddDrink(DrinkEntry _drink)
    {
        currentCocktail.AddDrink(_drink);
    }

    public Cocktail GetCocktail()
    {
        return currentCocktail;
    }
}

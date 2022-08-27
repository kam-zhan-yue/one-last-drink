using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;

[CreateAssetMenu(menuName = "Mixer")]
public class Mixer : ScriptableObject
{
    public DrinkDatabase drinkDatabase;
    public IntReference maxDrinks;
    
    public List<DrinkEntry> drinkList = new();

    public DrinkStats GetStats()
    {
        DrinkStats stats = new();
        for (int i = 0; i < drinkList.Count; ++i)
            stats.AddStats(drinkList[i].drinkStats);
        return stats;
    }

    public void Empty()
    {
        drinkList.Clear();
    }
    
    [Button]
    public void AddDrink(Drink _drink)
    {
        if (CanAddDrink())
        {
            DrinkEntry drinkEntry = drinkDatabase.GetDrinkEntry(_drink);
            AddDrink(drinkEntry);
        }
    }
    
    private void AddDrink(DrinkEntry _drink)
    {
        drinkList.Add(_drink);
    }

    public List<Drink> GetDrinks()
    {
        List<Drink> drinks = new();
        for (int i = 0; i < drinkList.Count; ++i)
            drinks.Add(drinkList[i].drink);
        return drinks;
    }

    public Cocktail GetCocktail()
    {
        return new Cocktail()
        {
            drinkList = drinkList
        };
    }
    
    public bool CanAddDrink()
    {
        return drinkList.Count < maxDrinks;
    }
}

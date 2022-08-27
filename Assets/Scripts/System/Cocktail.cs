using System;
using System.Collections.Generic;

[Serializable]
public class Cocktail
{
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
    
    public void AddDrink(DrinkEntry _drink)
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
}
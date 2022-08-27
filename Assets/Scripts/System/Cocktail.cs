using System;
using System.Collections.Generic;

[Serializable]
public class Cocktail
{
    public List<DrinkEntry> drinkList = new();
    public int maxDrinks = 0;

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

    public bool CanAddDrink()
    {
        return drinkList.Count >= maxDrinks;
    }

    public void AddDrink(DrinkEntry _drink)
    {
        if(CanAddDrink())
            drinkList.Add(_drink);
    }
}
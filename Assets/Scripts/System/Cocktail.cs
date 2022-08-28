using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

[Serializable]
public class Cocktail
{
    public List<DrinkComponent> componentList = new();

    [Button]
    public DrinkStats GetStats()
    {
        DrinkStats stats = new();
        for (int i = 0; i < componentList.Count; ++i)
        {
            DrinkStats modifiedStats = componentList[i].drinkEntry.drinkStats.GetModifiedStats(componentList[i].percentage);
            stats.AddStats(modifiedStats);
        }
        return stats;
    }
}
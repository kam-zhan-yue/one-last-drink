using System;
using System.Collections.Generic;
using System.Text;
using Sirenix.OdinInspector;

[Serializable]
public class Request
{
    private DrinkStats requestStats;
    private string requestString;

    public Request(List<Prompt> promptList)
    {
        requestStats = DrinkStats.RandomDrinkStats();

        float maxCompare = 0;
        foreach (Prompt prompt in promptList)
        {
            float compare = requestStats.Compare(prompt.stats);
            if (compare > maxCompare)
            {
                maxCompare = compare;
                requestString = prompt.promptString;
            }
        }
    }

    [Button]
    public DrinkStats GetStats()
    {
        return requestStats;
    }

    [Button]
    public override string ToString()
    {
        return requestString;
    }
}
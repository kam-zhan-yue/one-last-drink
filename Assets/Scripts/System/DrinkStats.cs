using System;
using UnityEngine;

[Serializable]
public struct DrinkStats
{
    [Range(0f, 10f)]
    public float alcohol;
    [Range(0f, 10f)]
    public float nutrition;
    [Range(0f, 10f)]
    public float sweetness;
    [Range(0f, 10f)]
    public float freshness;

    public void AddStats(DrinkStats _stats)
    {
        alcohol += _stats.alcohol;
        nutrition += _stats.nutrition;
        sweetness += _stats.sweetness;
        freshness += _stats.freshness;
    }

    public float Compare(DrinkStats _stats)
    {
        //TO WRITE!!!!
        return 0f;
    }
}
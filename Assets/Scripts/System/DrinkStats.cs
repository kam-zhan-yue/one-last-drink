using System;
using UnityEngine;
using Random = System.Random;

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

    public DrinkStats GetModifiedStats(float _multiplier)
    {
        DrinkStats stats = new();
        stats.alcohol = alcohol * _multiplier;
        stats.nutrition = nutrition * _multiplier;
        stats.sweetness = sweetness * _multiplier;
        stats.freshness = freshness * _multiplier;
        return stats;
    }

    // returns a value between 0 and 1 representing how accurate the made drink is
    public float Compare(DrinkStats _stats)
    {
        // all these values should be between 0 and 1 also
        float alcoholDivisor=  alcohol == 0 ? 1f : alcohol;
        float nutritionDivisor =  nutrition == 0 ? 1f : nutrition;
        float sweetnessDivisor =  sweetness == 0 ? 1f : sweetness;
        float freshnessDivisor =  freshness == 0 ? 1f : freshness;
        float alcoholError = Math.Abs((alcohol - _stats.alcohol) / alcoholDivisor);
        float nutritionError = Math.Abs((nutrition - _stats.nutrition) / nutritionDivisor);
        float sweetnessError = Math.Abs((sweetness - _stats.sweetness) / sweetnessDivisor);
        float freshnessError = Math.Abs((freshness - _stats.freshness) / freshnessDivisor);
        
        float result = 1f - (0.25f * alcoholError)
            - (0.25f * sweetnessError)
            - (0.25f * nutritionError)
            - (0.25f * freshnessError);

        return (result < 0) ? 0 : result;
    }

    public static DrinkStats RandomDrinkStats()
    {
        Random random = new();
        DrinkStats randomStats = new();

        randomStats.alcohol = (float) random.NextDouble() * 10;
        randomStats.nutrition = (float) random.NextDouble() * 10;
        randomStats.sweetness = (float) random.NextDouble() * 10;
        randomStats.freshness = (float) random.NextDouble() * 10;

        return randomStats;
    }
}
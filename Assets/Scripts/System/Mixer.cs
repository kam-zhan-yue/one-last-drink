using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;

[CreateAssetMenu(menuName = "Mixer")]
[InlineEditor()]
public class Mixer : ScriptableObject
{
    public DrinkDatabase drinkDatabase;
    public IntReference maxDrinks;
    
    [ShowInInspector, NonSerialized, ReadOnly]
    private List<DrinkComponent> componentList = new();
    private const float MAX = 1f;

    public float GetTotalCapacity()
    {
        float total = 0f;
        for (int i = 0; i < componentList.Count; ++i)
            total += componentList[i].percentage;
        return total;
    }

    [Button]
    public bool IncrementDrink(Drink _drink, float _increment)
    {
        float totalCapacity = GetTotalCapacity();
        if (totalCapacity >= MAX)
            return false;
        int targetIndex = -1;
        for (int i = 0; i < componentList.Count; ++i)
        {
            if (componentList[i].drinkEntry.drink == _drink)
            {
                targetIndex = i;
                break;
            }
        }

        //Don't let it go above MAX!!!
        if (targetIndex >= 0)
        {
            if (totalCapacity + _increment >= MAX)
                componentList[targetIndex].percentage += MAX - totalCapacity;
            else
                componentList[targetIndex].percentage += _increment;
        }
        else
        {
            DrinkComponent component = new();
            component.drinkEntry = drinkDatabase.GetDrinkEntry(_drink);
            if (totalCapacity + _increment >= MAX)
                component.percentage = MAX - totalCapacity;
            else
                component.percentage = _increment;
            componentList.Add(component);
        }

        return true;
    }

    [Button]
    public void Empty()
    {
        componentList.Clear();
    }

    [Button]
    public Cocktail GetCocktail()
    {
        return new Cocktail()
        {
            componentList = componentList
        };
    }

    [Button]
    public Gradient CreateGradient()
    {
        float totalCapacity = GetTotalCapacity();
        if (totalCapacity <= 0)
            return new();
        int components = componentList.Count;
        Gradient g = new();
        GradientColorKey[] gck = new GradientColorKey[components];
        GradientAlphaKey[] gak = new GradientAlphaKey[components];
        float cumulativePercentage = 0f;
        for (int i = 0; i < componentList.Count; ++i)
        {
            float time = componentList[i].percentage / totalCapacity;
            gck[i].color = componentList[i].drinkEntry.drink.colour;
            gck[i].time = cumulativePercentage + time;
            gak[i].time = cumulativePercentage + time;
            gak[i].alpha = 1.0f;
            cumulativePercentage += time;
        }
        g.SetKeys(gck, gak);
        return g;
    }

    public bool CanAddDrink()
    {
        return GetTotalCapacity() < MAX;
    }
}

using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Drink Database")]
public class DrinkDatabase : ScriptableObject
{
    [TableList] 
    public List<DrinkEntry> drinkList = new List<DrinkEntry>();

    public DrinkEntry GetDrinkEntry(Drink _drink)
    {
        for (int i = 0; i < drinkList.Count; ++i)
            if (_drink == drinkList[i].drink)
                return drinkList[i];
        return new();
    }
}

using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Drink Database")]
public class DrinkDatabase : ScriptableObject
{
    [TableList] 
    public List<DrinkEntry> drinkList = new List<DrinkEntry>();
}

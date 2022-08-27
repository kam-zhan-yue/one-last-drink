using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Prompt
{
    [TextArea] 
    public string promptString = string.Empty;
    public DrinkStats stats = new DrinkStats();
    public List<Drink> specialCombination = new();
}

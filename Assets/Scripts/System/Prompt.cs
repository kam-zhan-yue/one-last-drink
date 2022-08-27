using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[Serializable]
public class Prompt
{
    [TextArea] 
    public string promptString = string.Empty;
    public DrinkStats stats;

    public static Prompt GetRandomPrompt(List<Prompt> promptList)
    {
        Random random = new();
        int index = random.Next(promptList.Count);
        return promptList[index];
    }
}

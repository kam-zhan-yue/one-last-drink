using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

[Serializable]
public class Request
{
    private DrinkStats requestStats;
    private string requestString;

    public Request(List<Prompt> promptList)
    {
        Prompt prompt = Prompt.GetRandomPrompt(promptList);
        requestStats = prompt.stats;
        requestString = prompt.promptString;
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
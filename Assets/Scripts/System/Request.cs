using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

[Serializable]
public class Request
{
    private DrinkStats requestStats = new DrinkStats();
    private string requestString = string.Empty;

    public Request()
    {
    }

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
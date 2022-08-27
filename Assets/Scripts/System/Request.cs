using System;
using System.Collections.Generic;
using System.Text;
using Sirenix.OdinInspector;

[Serializable]
public class Request
{
    public List<Prompt> promptList = new();

    [Button]
    public DrinkStats GetStats()
    {
        DrinkStats stats = new();
        for (int i = 0; i < promptList.Count; ++i)
            stats.AddStats(promptList[i].stats);
        return stats;
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        for (int i = 0; i < promptList.Count; ++i)
        {
            sb.Append(promptList[i].promptString);
            sb.Append(" ");
        }
        return sb.ToString();
    }
}
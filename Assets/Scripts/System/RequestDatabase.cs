using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(menuName = "Request Database")]
[InlineEditor()]
public class RequestDatabase : ScriptableObject
{
    [TableList]
    public List<Prompt> promptList = new();

    [Button]
    public Request GenerateRequest()
    {
        return new Request(promptList);
    }
}

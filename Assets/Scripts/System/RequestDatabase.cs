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
    public List<Prompt> promptList = new List<Prompt>();

    [Button]
    public Request GenerateRequest(Random _random, int _maxPrompts)
    {
        Request request = new();
        request.promptList = _random.RandomSubList(promptList, _maxPrompts);
        return request;
    }
}

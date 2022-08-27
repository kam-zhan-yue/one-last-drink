using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Character")]
[InlineEditor()]
public class Character : ScriptableObject
{
    public RequestDatabase requestDatabase;
    public Sprite sprite;

    public string loveResponse = string.Empty;
    public string satisfiedResponse = string.Empty;
    public string neutralResponse = string.Empty;
    public string unsatisfiedResponse = string.Empty;
    public string hateResponse = string.Empty;
}

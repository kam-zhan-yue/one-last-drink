using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character")]
public class Character : ScriptableObject
{
    public RequestDatabase requestDatabase;

    public string loveResponse = string.Empty;
    public string satisfiedResponse = string.Empty;
    public string neutralResponse = string.Empty;
    public string unsatisfiedResponse = string.Empty;
    public string hateResponse = string.Empty;
}

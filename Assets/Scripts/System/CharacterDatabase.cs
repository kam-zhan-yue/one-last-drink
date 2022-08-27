using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Database")]
public class CharacterDatabase : ScriptableObject
{
    [TableList]
    public List<CharacterEntry> characterList = new List<CharacterEntry>();
}

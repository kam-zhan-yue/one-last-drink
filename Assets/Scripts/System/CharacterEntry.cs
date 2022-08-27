using System;
using System.Collections.Generic;

[Serializable]
public class CharacterEntry
{
    public Character character;
    public List<Drink> preferenceList = new();
}
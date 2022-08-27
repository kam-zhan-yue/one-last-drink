using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Drink")]
public class Drink : ScriptableObject
{
    public Sprite sprite;
    public Color colour;
    [TextArea]
    public string description = string.Empty;
}

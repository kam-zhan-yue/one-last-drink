using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakerPopupItem : MonoBehaviour
{
    public Image image;
    private Drink drink;

    public void Init(Drink _drink)
    {
        image.color = _drink.colour;
    }
}

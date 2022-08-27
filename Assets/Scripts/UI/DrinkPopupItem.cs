using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkPopupItem : MonoBehaviour
{
    public BarPopup barPopup;
    public SpriteRenderer spriteRenderer;
    private Drink drink;
    
    public void Init(Drink _drink)
    {
        drink = _drink;
        spriteRenderer.sprite = drink.sprite;
    }

    public void OnPointerEnter()
    {
        barPopup.PointerEnterDrink(drink);
    }

    public void OnPointerExit()
    {
        barPopup.PointerExitDrink(drink);
    }

    public void OnPointerClick()
    {
        barPopup.PointerClickDrink(drink);
    }
}

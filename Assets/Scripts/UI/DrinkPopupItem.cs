using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkPopupItem : MonoBehaviour
{
    public BarPopup barPopup;
    public SpriteRenderer spriteRenderer;
    public Image image;
    private Drink drink;

    public void Init(Drink _drink)
    {
        drink = _drink;
        if (drink.sprite != null)
            spriteRenderer.sprite = drink.sprite;
        else if(image != null)
            image.color = drink.colour;
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

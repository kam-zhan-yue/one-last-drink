using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkPopupItem : MonoBehaviour
{
    public BarPopup barPopup;
    public Image image;
    private Drink drink;

    public void Init(Drink _drink)
    {
        drink = _drink;
        ResetImage();
    }

    private void ResetImage()
    {
        if (drink.sprite != null)
            image.sprite = drink.sprite;
        else if (image != null)
            image.color = drink.colour;
    }

    public void OnPointerEnter()
    {
        if (drink.openedSprite != null)
            image.sprite = drink.openedSprite;
        barPopup.PointerEnterDrink(drink);
    }

    public void OnPointerExit()
    {
        ResetImage();
        barPopup.PointerExitDrink(drink);
    }

    public void OnPointerClick()
    {
        barPopup.PointerClickDrink(drink);
    }
}

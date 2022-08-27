using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPopup : Popup
{
    public GameManager gameManager;
    public BarPopup barPopup;
    
    public override void InitPopup()
    {
    }

    public override void ShowPopup()
    {
        barPopup.HidePopup();
        gameObject.SetActiveFast(true);
    }

    public override void HidePopup()
    {
        gameObject.SetActiveFast(false);
    }

    public void StartGame()
    {
        HidePopup();
        barPopup.ShowPopup();
        gameManager.StartGame();
        gameManager.StartCharacter();
    }
}

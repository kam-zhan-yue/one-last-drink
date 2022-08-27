using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class GameEndPopup : Popup
{
    public MenuPopup menuPopup;
    public FloatReference tipCounter;
    public TMP_Text tipText;
    
    public override void InitPopup()
    {
        gameObject.SetActiveFast(false);
    }

    public override void ShowPopup()
    {
        gameObject.SetActiveFast(true);
        tipText.SetText("Total Tips: $"+tipCounter.Value);
    }

    public void TryAgain()
    {
        gameObject.SetActiveFast(false);
        menuPopup.StartGame();
    }

    public void ToMenu()
    {
        gameObject.SetActiveFast(false);
        menuPopup.ShowPopup();
    }

    public override void HidePopup()
    {
        gameObject.SetActiveFast(false);
    }
}

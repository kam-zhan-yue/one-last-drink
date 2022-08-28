using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class GameEndPopup : Popup
{
    public MenuPopup menuPopup;
    public BarPopup barPopup;
    public CharacterPopup characterPopup;
    public DialoguePopup dialoguePopup;
    public TipPopup tipPopup;
    public FloatReference tipCounter;
    public TMP_Text tipText;
    
    public override void InitPopup()
    {
        gameObject.SetActiveFast(false);
    }

    public override void ShowPopup()
    {
        gameObject.SetActiveFast(true);
        barPopup.HidePopup();
        characterPopup.HidePopup();
        dialoguePopup.HidePopup();
        tipPopup.HidePopup();
        float roundedValue = Mathf.Round(tipCounter.Value * 100f) / 100f;
        tipText.SetText("Total Tips: $"+roundedValue);
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

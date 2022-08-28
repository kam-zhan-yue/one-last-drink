using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPopup : Popup
{
    public GameManager gameManager;
    public BarPopup barPopup;
    public DialoguePopup dialoguePopup;
    public CharacterPopup characterPopup;
    public TipPopup tipPopup;

    public override void InitPopup()
    {
    }

    public override void ShowPopup()
    {
        barPopup.HidePopup();
        dialoguePopup.HidePopup();
        characterPopup.HidePopup();
        tipPopup.HidePopup();
        gameObject.SetActiveFast(true);
    }

    public override void HidePopup()
    {
        gameObject.SetActiveFast(false);
    }

    public void StartGame()
    {
        AudioManager.instance.Play(AudioManager.BUTTON);
        HidePopup();
        gameManager.StartGame();
        gameManager.StartCharacter();
        barPopup.ShowPopup();
        characterPopup.ShowPopup();
        tipPopup.ShowPopup();
    }
}

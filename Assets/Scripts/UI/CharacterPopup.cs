using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPopup : Popup
{
    [FoldoutGroup("UI Objects")] public DialoguePopup dialoguePopup;
    [FoldoutGroup("UI Objects")] public Image characterSprite;

    public float showDuration = 1f;

    private Customer currentCustomer;
    
    public override void InitPopup()
    {
        gameObject.SetActiveFast(false);
    }

    [Button]
    public void Init(Customer _customer)
    {
        currentCustomer = _customer;
        ShowPopup();
    }

    public override void ShowPopup()
    {
        gameObject.SetActiveFast(true);
        Color originalColour = characterSprite.color;
        Color invisibleColour = originalColour;
        invisibleColour.a = 0f;
        characterSprite.color = invisibleColour;
        characterSprite.sprite = currentCustomer.character.sprite;
        characterSprite.DOColor(originalColour, showDuration).OnComplete(() =>
        {
            dialoguePopup.ShowPopup();
            dialoguePopup.DisplayDialogue(currentCustomer.character.name, currentCustomer.request.ToString());
        });
    }

    public override void HidePopup()
    {
    }
}

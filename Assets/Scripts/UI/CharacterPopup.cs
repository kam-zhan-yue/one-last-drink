using System;
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
    [FoldoutGroup("UI Objects")] public Image drinkSprite;

    public float showDuration = 1f;
    [FoldoutGroup("UI Objects")] public Animation characterAnimation;

    public bool canChangeDrink = false;
    public bool canChangeCharacter = false;
    
    public GameEvent characterComplete;
    
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
        drinkSprite.gameObject.SetActiveFast(false);
        Color originalColour = characterSprite.color;
        Color invisibleColour = originalColour;
        invisibleColour.a = 0f;
        characterSprite.color = invisibleColour;
        characterSprite.sprite = currentCustomer.character.sprite;

        if (currentCustomer.character.neutralAnimation != null)
            characterAnimation.Play(currentCustomer.character.neutralAnimation.name);
        
        characterSprite.DOColor(originalColour, showDuration).OnComplete(() =>
        {
            dialoguePopup.EnqueueDialogue(currentCustomer.character.name, currentCustomer.GetCurrentRequest().ToString());
            dialoguePopup.ShowPopup();
        });
    }

    public void ShowResponse(Response _response, Action _onComplete = null)
    {
        Sequence serveSequence = PlayServeSequence();
        serveSequence.OnComplete(() =>
        {
            characterSprite.sprite = currentCustomer.character.GetResponseSprite(_response.reaction);
            dialoguePopup.EnqueueDialogue(currentCustomer.character.name, _response.dialogue);
            dialoguePopup.EnqueueDialogue(currentCustomer.character.name, currentCustomer.character.GetRandomConnector());
            dialoguePopup.ShowPopup();
            canChangeDrink = true;
            _onComplete?.Invoke();
        });
    }
    
    private Sequence PlayServeSequence()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(dialoguePopup.HideSequence());
        sequence.Append(ShowDrinkSequence());
        sequence.Play();
        return sequence;
    }

    public void DialoguePanelClicked()
    {
        dialoguePopup.DialoguePanelClicked();
        if (canChangeCharacter)
        {
            HidePopup();
        }
        else if (canChangeDrink)
        {
            canChangeDrink = false;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(dialoguePopup.HideSequence());
            sequence.Append(HideDrinkSequence());
            sequence.Play();
            sequence.OnComplete(() =>
            {

            });
        }
    }
    
    public override void HidePopup()
    {
        HideDrinkSequence().Play();
    }

    private Sequence ShowDrinkSequence()
    {
        Sequence sequence = DOTween.Sequence();
        drinkSprite.gameObject.SetActiveFast(true);
        Color originalColour = drinkSprite.color;
        Color invisibleColour = originalColour;
        invisibleColour.a = 0f;
        drinkSprite.color = invisibleColour;
        Tween colourTween = drinkSprite.DOColor(originalColour, showDuration);
        sequence.Append(colourTween);
        return sequence;
    }

    private Sequence HideDrinkSequence()
    {
        Color originalColour = drinkSprite.color;
        Color invisibleColour = originalColour;
        invisibleColour.a = 0f;
        Sequence sequence = DOTween.Sequence();
        Tween colourTween = drinkSprite.DOColor(invisibleColour, showDuration);
        sequence.Append(colourTween);
        return sequence;
    }
    
    private Sequence HideSequence()
    {
        Color originalColour = characterSprite.color;
        Color invisibleColour = originalColour;
        invisibleColour.a = 0f;
        Sequence sequence = DOTween.Sequence();
        Tween colourTween = characterSprite.DOColor(invisibleColour, showDuration).OnComplete(() =>
        {
            characterComplete.Raise();
        });
        sequence.Append(colourTween);
        return sequence;
    }
}

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
    [FoldoutGroup("UI Objects")] public Animator characterAnimator;

    public float showDuration = 1f;
    private bool responseOver = false;
    
    public GameEvent requestCompleted;
    public GameEvent customerCompleted;
    
    private Customer currentCustomer;
    
    public override void InitPopup()
    {
        gameObject.SetActiveFast(false);
    }

    [Button]
    public void Init(Customer _customer)
    {
        currentCustomer = _customer;
        // ShowPopup();
    }

    [Button]
    public void Test(string _name)
    {
        characterAnimator.Play(_name);
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

        AnimationClip clip = currentCustomer.character.neutralAnimation;
        if (clip != null)
        {
            characterAnimator.enabled = true;
            characterAnimator.Play(clip.name);
        }
        else
        {
            characterAnimator.enabled = false;
        }
        
        characterSprite.DOColor(originalColour, showDuration).OnComplete(() =>
        {
            dialoguePopup.EnqueueDialogue(currentCustomer.character.name, currentCustomer.GetCurrentRequest().ToString());
            dialoguePopup.ShowPopup();
        });
    }

    //Used to show for a new request
    public void UpdatePanel(Customer _customer)
    {
        currentCustomer = _customer;
        AnimationClip clip = currentCustomer.character.neutralAnimation;
        if (clip != null)
        {
            characterAnimator.enabled = true;
            characterAnimator.Play(clip.name);
        }
        else
        {
            characterAnimator.enabled = false;
        }
        dialoguePopup.EnqueueDialogue(currentCustomer.character.name, currentCustomer.GetCurrentRequest().ToString());
        dialoguePopup.ShowPopup();
    }

    public void ShowResponse(Response _response, Action _onComplete = null)
    {
        Sequence serveSequence = PlayServeSequence();
        serveSequence.OnComplete(() =>
        {
            AnimationClip clip = currentCustomer.character.GetResponseAnimation(_response.reaction);
            if (clip != null)
            {
                characterAnimator.enabled = true;
                characterAnimator.Play(clip.name);
            }
            else
                characterAnimator.enabled = false;
            dialoguePopup.EnqueueDialogue(currentCustomer.character.name, _response.dialogue);
            //If there is still a request, then queue the connector
            if(currentCustomer.HasRequest())
                dialoguePopup.EnqueueDialogue(currentCustomer.character.name, currentCustomer.character.GetRandomConnector());
            dialoguePopup.ShowPopup();
            responseOver = true;
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
        AudioManager.instance.Play(AudioManager.BUTTON);
        if (responseOver && dialoguePopup.CanClose())
        {
            responseOver = false;
            if (currentCustomer.HasRequest())
            {
                Sequence sequence = DOTween.Sequence();
                sequence.Append(dialoguePopup.HideSequence());
                sequence.Append(HideDrinkSequence());
                sequence.Play();
                sequence.OnComplete(() =>
                {
                    requestCompleted.Raise();
                });
            }
            else
            {
                Sequence sequence = DOTween.Sequence();
                sequence.Append(dialoguePopup.HideSequence());
                sequence.Append(HideDrinkSequence());
                sequence.Append(HideSequence());
                sequence.Play();
                sequence.OnComplete(() =>
                {
                    customerCompleted.Raise();
                });
            }
        }
        dialoguePopup.DialoguePanelClicked();
    }
    
    public override void HidePopup()
    {
        gameObject.SetActiveFast(false);
        // Debug.Log("Hide!");
        // HideSequence().Play().OnComplete(() =>
        // {
        // });
    }

    private Sequence ShowDrinkSequence()
    {
        Debug.Log("Show Drink Sequence");
        Sequence sequence = DOTween.Sequence();
        drinkSprite.gameObject.SetActiveFast(true);
        Color invisibleColour = drinkSprite.color;
        Color visibleColour = drinkSprite.color;
        invisibleColour.a = 0f;
        visibleColour.a = 1f;
        //Set to invisible, tween to visible
        drinkSprite.color = invisibleColour;
        Tween colourTween = drinkSprite.DOColor(visibleColour, showDuration);
        sequence.Append(colourTween);
        sequence.AppendCallback(() =>
        {
            AudioManager.instance.Play(AudioManager.PLACE);
        });
        return sequence;
    }

    private Sequence HideDrinkSequence()
    {
        Debug.Log("Hide Drink Sequence");
        Color invisibleColour = drinkSprite.color;
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
        Tween colourTween = characterSprite.DOColor(invisibleColour, showDuration);
        sequence.Append(colourTween);
        return sequence;
    }
}

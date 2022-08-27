using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class DialoguePopup : Popup
{
    public TMP_Text titleText;
    public TMP_Text descriptionText;

    public float typeSpeed = 15f;
    public Ease ease = Ease.InOutCubic;
    public float showDuration = 0.4f;
    private Tween typeWriterTween;
    private Vector3 originalScale = Vector3.zero;
    private bool textScrolling = false;

    private string title;
    private Queue<Dialogue> dialogueQueue = new();
    
    public override void InitPopup()
    {
        originalScale = transform.localScale;
        gameObject.SetActiveFast(false);
    }

    [Button]
    public override void ShowPopup()
    {
        gameObject.SetActiveFast(true);
        Transform cacheTransform = transform;
        cacheTransform.localScale = Vector3.zero;
        cacheTransform.DOScale(originalScale, showDuration).SetEase(ease);
        CheckQueue();
    }

    private void CheckQueue()
    {
        if (dialogueQueue.Count > 0)
        {
            Dialogue dialogue = dialogueQueue.Dequeue();
            DisplayDialogue(dialogue);
        }
        // else
        // {
        //     if(canHide) 
        //         HidePopup();
        // }
    }

    public void EnqueueDialogue(string _title, string _body)
    {
        Dialogue dialogue = new();
        dialogue.title = _title;
        dialogue.body = _body;
        dialogueQueue.Enqueue(dialogue);
    }

    public void DisplayDialogue(Dialogue _dialogue)
    {
        DisplayDialogue(_dialogue.title, _dialogue.body);
    }

    [Button]
    private void DisplayDialogue(string _title, string _dialogue)
    {
        titleText.SetText(_title);
        string text = string.Empty;
        float speed = typeSpeed;
        if (speed == 0f)
            speed = 1f;
        typeWriterTween = DOTween.To(() => text, _x => text = _x, _dialogue, _dialogue.Length / speed)
            .OnUpdate(() =>
            {
                textScrolling = true; 
                descriptionText.SetText(text);
            })
            .OnComplete(() =>
            {
                textScrolling = false;
            }).OnKill(() =>
            {
                textScrolling = false;
                descriptionText.SetText(_dialogue);
            });
    }

    public void DialoguePanelClicked()
    {
        if (textScrolling)
        {
            Debug.Log("Killed!");
            typeWriterTween.Kill();
            textScrolling = false;
        }
        else
        {
            Debug.Log("Clicked!");
            CheckQueue();
        }
    }
    
    [Button]
    public override void HidePopup()
    {
        HideSequence().Play();
    }

    public Sequence HideSequence()
    {
        Tween scaleTween = transform.DOScale(Vector3.zero, showDuration).SetEase(ease).OnComplete(() =>
        {
            gameObject.SetActiveFast(false);
        });
        Sequence sequence = DOTween.Sequence();
        sequence.Append(scaleTween);
        return sequence;
    }
}

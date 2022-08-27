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
    }

    [Button]
    public void DisplayDialogue(string _title, string _dialogue)
    {
        titleText.SetText(_title);
        string text = string.Empty;
        float speed = typeSpeed;
        if (speed == 0f)
            speed = 1f;
        typeWriterTween = DOTween.To(() => text, _x => text = _x, _dialogue, _dialogue.Length / speed)
            .OnUpdate(() =>
        {
            descriptionText.SetText(text);
        });
    }
    
    [Button]
    public override void HidePopup()
    {
        transform.DOScale(Vector3.zero, showDuration).SetEase(ease).OnComplete(() =>
        {
            gameObject.SetActiveFast(false);
        });
    }
}

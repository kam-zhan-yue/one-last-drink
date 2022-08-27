using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager instance;

    public TMP_Text titleText;
    public TMP_Text descriptionText;
    private Camera mainCam;

    private void Awake()
    {
        if (instance && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        mainCam = Camera.main;
        Cursor.visible = true;
        gameObject.SetActiveFast(false);
    }

    private void Update()
    {
        transform.position = Mouse.current.position.ReadValue();
    }

    public void ShowTooltip(string _title, string _description)
    {
        gameObject.SetActiveFast(true);
        titleText.text = _title;
        descriptionText.text = _description;
    }

    public void HideTooltip()
    {
        gameObject.SetActiveFast(false);
        titleText.text = string.Empty;
        descriptionText.text = string.Empty;
    }
}

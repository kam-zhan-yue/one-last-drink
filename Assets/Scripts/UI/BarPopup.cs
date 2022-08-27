using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class BarPopup : Popup
{
    [FoldoutGroup("System Objects")] public FloatReference tipCounter;
    [FoldoutGroup("System Objects")] public DrinkDatabase drinkDatabase;

    [FoldoutGroup("UI Objects")] public Transform drinkLayoutGroup;
    [FoldoutGroup("UI Objects")] public DrinkPopupItem sampleDrinkPopupItem;
    [FoldoutGroup("UI Objects")] public Transform drinkTextHolder;
    [FoldoutGroup("UI Objects")] public TMP_Text drinkTitleText;
    [FoldoutGroup("UI Objects")] public TMP_Text drinkDescriptionText;

    [NonSerialized, ShowInInspector, ReadOnly]
    private List<DrinkPopupItem> currentDrinkList = new();

    public override void InitPopup()
    {
        int numToSpawn = drinkDatabase.drinkList.Count - currentDrinkList.Count;
        if (numToSpawn > 0)
        {
            sampleDrinkPopupItem.gameObject.SetActiveFast(true);
            for (int i = 0; i < numToSpawn; ++i)
            {
                DrinkPopupItem popupItem = Instantiate(sampleDrinkPopupItem, drinkLayoutGroup);
                currentDrinkList.Add(popupItem);
            }
        }

        for (int i = 0; i < currentDrinkList.Count; ++i)
        {
            if (i < drinkDatabase.drinkList.Count)
            {
                currentDrinkList[i].gameObject.SetActiveFast(true);
                currentDrinkList[i].Init(drinkDatabase.drinkList[i].drink);
            }
            else
            {
                currentDrinkList[i].gameObject.SetActiveFast(false);
            }
        }
        
        sampleDrinkPopupItem.gameObject.SetActiveFast(false);
    }

    public override void ShowPopup()
    {
    }

    public void PointerEnterDrink(Drink _drink)
    {
        TooltipManager.instance.ShowTooltip(_drink.name, _drink.description);
    }

    public void PointerExitDrink(Drink _drink)
    {
        TooltipManager.instance.HideTooltip();
    }

    public void PointerClickDrink(Drink _drink)
    {
        
    }

    public override void HidePopup()
    {
    }
}

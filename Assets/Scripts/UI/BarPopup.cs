using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class BarPopup : Popup
{
    [FoldoutGroup("System Objects")] public Bartender bartender;
    [FoldoutGroup("System Objects")] public FloatReference tipCounter;
    [FoldoutGroup("System Objects")] public IntReference maxDrinks;
    [FoldoutGroup("System Objects")] public DrinkDatabase drinkDatabase;

    [FoldoutGroup("UI Objects")] public Transform drinkLayoutGroup;
    [FoldoutGroup("UI Objects")] public DrinkPopupItem sampleDrinkPopupItem;
    [FoldoutGroup("UI Objects")] public Transform shakerLayoutGroup;
    [FoldoutGroup("UI Objects")] public ShakerPopupItem sampleShakerPopupItem;

    [NonSerialized, ShowInInspector, ReadOnly]
    private List<DrinkPopupItem> currentDrinkList = new();

    private List<ShakerPopupItem> currentShakerList = new();
    private List<ShakerPopupItem> currentEmptyList = new();

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

        int emptyToSpawn = maxDrinks - currentEmptyList.Count;
        if (emptyToSpawn > 0)
        {
            sampleShakerPopupItem.gameObject.SetActiveFast(true);
            for (int i = 0; i < emptyToSpawn; ++i)
            {
                ShakerPopupItem popupItem = Instantiate(sampleShakerPopupItem, shakerLayoutGroup);
                currentEmptyList.Add(popupItem);
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
        sampleShakerPopupItem.gameObject.SetActiveFast(false);
    }

    public override void ShowPopup()
    {
    }

    private void AddDrink(Drink _drink)
    {
        if (bartender.CanAddDrink())
        {
            bartender.AddDrink(_drink);
            UpdatePanel();
        }
    }

    public void ClearCocktail()
    {
        bartender.ClearCocktail();
        UpdatePanel();
    }

    public void SubmitCocktail()
    {
        
    }

    private void UpdatePanel()
    {
        List<Drink> drinks = bartender.GetDrinks();
        int numToSpawn = drinks.Count - currentShakerList.Count;
        if (numToSpawn > 0)
        {
            sampleShakerPopupItem.gameObject.SetActiveFast(true);
            for (int i = 0; i < numToSpawn; ++i)
            {
                ShakerPopupItem popupItem = Instantiate(sampleShakerPopupItem, shakerLayoutGroup);
                currentShakerList.Add(popupItem);
            }
        }

        int drinksActive = 0;
        for (int i = 0; i < currentShakerList.Count; ++i)
        {
            if (i < drinks.Count)
            {
                currentShakerList[i].gameObject.SetActiveFast(true);
                currentShakerList[i].transform.SetAsLastSibling();
                currentShakerList[i].Init(drinks[i]);
                drinksActive++;
            }
            else
            {
                currentShakerList[i].gameObject.SetActiveFast(false);
            }
        }

        int emptyToActivate = maxDrinks - drinksActive;
        for (int i = 0; i < currentEmptyList.Count; ++i)
        {
            if (i < emptyToActivate)
            {
                currentEmptyList[i].gameObject.SetActiveFast(true);
                currentEmptyList[i].transform.SetAsLastSibling();
            }
            else
            {
                currentEmptyList[i].gameObject.SetActiveFast(false);
            }
        }
            
        sampleShakerPopupItem.gameObject.SetActiveFast(false);
    }

    public void PointerEnterDrink(Drink _drink)
    {
        TooltipManager.instance.ShowTooltip(_drink.name, _drink.description);
    }

    public void PointerExitDrink(Drink _drink)
    {
        if(TooltipManager.instance != null)
            TooltipManager.instance.HideTooltip();
    }

    public void PointerClickDrink(Drink _drink)
    {
        AddDrink(_drink);
    }

    public override void HidePopup()
    {
    }
}

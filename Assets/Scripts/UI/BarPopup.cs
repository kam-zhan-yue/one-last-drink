using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class BarPopup : Popup
{
    [FoldoutGroup("System Objects")] public GameManager gameManager;
    [FoldoutGroup("System Objects")] public Mixer mixer;
    [FoldoutGroup("System Objects")] public FloatReference tipCounter;
    [FoldoutGroup("System Objects")] public IntReference maxDrinks;
    [FoldoutGroup("System Objects")] public DrinkDatabase drinkDatabase;

    [FoldoutGroup("UI Objects")] public Transform drinkLayoutGroup;
    [FoldoutGroup("UI Objects")] public DrinkPopupItem sampleDrinkPopupItem;
    [FoldoutGroup("UI Objects")] public Image shakerFillItem;
    [FoldoutGroup("UI Objects")] public Button submitButton;

    public float incrementValue = 0.5f;
    public float pourTimeStep = 0.1f;
    public float timeMultiplier = 0.01f;

    [NonSerialized, ShowInInspector, ReadOnly]
    private List<DrinkPopupItem> currentDrinkList = new();

    private CoroutineHandle pourRoutine;
    private CoroutineHandle updateRoutine;
    private Gradient mixerGradient = new();

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
        submitButton.gameObject.SetActiveFast(false);
        gameObject.SetActiveFast(false);
    }

    public override void ShowPopup()
    {
        gameObject.SetActiveFast(true);
        Timing.KillCoroutines(updateRoutine);
        updateRoutine = Timing.RunCoroutine(UpdateRoutine());
    }

    public void StartPouring(Drink _drink)
    {
        AudioManager.instance.Play(AudioManager.POUR);
        Timing.KillCoroutines(pourRoutine);
        pourRoutine = Timing.RunCoroutine(PourRoutine(_drink));
    }

    public void EndPouring()
    {
        AudioManager.instance.Stop(AudioManager.POUR);
        Timing.KillCoroutines(pourRoutine);
    }

    private IEnumerator<float> UpdateRoutine()
    {
        float time = 0;
        float timeStep = 0.15f;
        while (true)
        {
            time += timeStep;
            float evaluation = Mathf.PingPong(time * timeMultiplier, 1f);
            shakerFillItem.color = mixerGradient.Evaluate(evaluation);
            yield return Timing.WaitForSeconds(0.15f);
        }
    }
    
    private IEnumerator<float> PourRoutine(Drink _drink)
    {
        while (true)
        {
            if (!mixer.IncrementDrink(_drink, incrementValue))
                EndPouring();
            UpdatePanel();
            yield return Timing.WaitForSeconds(pourTimeStep);
        }
    }
    
    public void ClearCocktail()
    {
        AudioManager.instance.Play(AudioManager.BUTTON);
        mixer.Empty();
        UpdatePanel();
    }

    private void UpdatePanel()
    {
        //Set the Capacity Text
        float capacity = mixer.GetTotalCapacity();
        if(capacity > 0)
            submitButton.gameObject.SetActiveFast(true);
        
        //Scale up the fill
        Transform shakerTransform = shakerFillItem.transform;
        Vector3 shakerScale = shakerTransform.localScale;
        shakerScale.x = capacity;
        shakerTransform.localScale = shakerScale;
            
        mixerGradient = mixer.CreateGradient();
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
    
    public void NewRequestCreated()
    {
        submitButton.gameObject.SetActiveFast(true);
    }

    public void SubmitCocktail()
    {
        AudioManager.instance.Play(AudioManager.SERVE);
        gameManager.ServeCocktail(mixer);
        ClearCocktail();
        submitButton.gameObject.SetActiveFast(false);
    }

    public override void HidePopup()
    {
        gameObject.SetActiveFast(false);
        Timing.KillCoroutines(updateRoutine);
        Timing.KillCoroutines(pourRoutine);
    }
}

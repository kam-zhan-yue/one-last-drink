using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CustomerController customerController;
    public FloatReference perfectTip;
    public FloatReference tips;
    public GameEndPopup gameEndPopup;
    public CharacterPopup characterPopup;
    public List<Customer> customerList = new();
    public Customer customer;
    public GameEvent newRequest;

    private int currentIndex = 0;

    private void Start()
    {
        AudioManager.instance.Play(AudioManager.BGM);
    }

    [Button]
    public void StartGame()
    {
        currentIndex = 0;
        tips.Value = 0f;
        customerList = customerController.GenerateCustomerList();
    }

    [Button]
    public void StartCharacter()
    {
        GetNewCharacter();
        if (customer.HasRequest())
        {
            characterPopup.Init(customer);
            newRequest.Raise();
        }
    }

    private void GetNewCharacter()
    {
        //I'm in a rush, i can't change this stupid code rn
        customer = customerList[currentIndex];
    }
    
    public void ServeCocktail(Mixer _mixer)
    {
        Cocktail cocktail = _mixer.GetCocktail();
        //After judging the cocktail, the index for the next request is incremented
        float score = customer.JudgeCocktail(cocktail);
        ShowResponse(customer.character, score);
    }

    private void ShowResponse(Character _character, float _score)
    {
        Response response = _character.GetResponse(_score);
        characterPopup.ShowResponse(response, () =>
        {
            UpdateTips(_score);
        });
    }

    private void UpdateTips(float _score)
    {
        tips.Value += _score * perfectTip;
    }

    public void OnRequestCompleted()
    {
        //When the player finishes reading the response to the request and a connector, check to see if
        //there are any more requests. If there are no more requests, then move onto the next character.
        characterPopup.UpdatePanel(customer);
        newRequest.Raise();
    }

    public void OnCustomerCompleted()
    {
        IncrementCustomer();
        if(Completed())
            EndGame();
        else
            StartCharacter();
    }

    public Customer GetCurrentCustomer()
    {
        if (currentIndex < 0 || currentIndex >= customerList.Count)
            return null;
        return customerList[currentIndex];
    }

    private bool LastCustomer()
    {
        return currentIndex == customerList.Count - 1;
    }

    private bool Completed()
    {
        return currentIndex >= customerList.Count;
    }

    private void IncrementCustomer()
    {
        currentIndex++;
    }
    
    private void EndGame()
    {
        gameEndPopup.ShowPopup();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CustomerController customerController;
    public FloatReference tips;
    public CharacterPopup characterPopup;
    public List<Customer> customerList = new();
    public Customer customer;

    private int currentIndex = 0;

    [Button]
    public void StartGame()
    {
        customerList = customerController.GenerateCustomerList();
    }

    [Button]
    public void StartCharacter()
    {
        GetNewCharacter();
        characterPopup.Init(customer);
    }

    private void GetNewCharacter()
    {
        customer = customerList[currentIndex];
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
    
    public void ServeCocktail(Mixer _mixer)
    {
        Cocktail cocktail = _mixer.GetCocktail();
        float score = customer.JudgeCocktail(cocktail);
        ShowResponse(customer.character, score);
    }

    public void ShowResponse(Character _character, float _score)
    {
        if (LastCustomer())
        {
            characterPopup.canChangeCharacter = true;
        }
        Response response = _character.GetResponse(_score);
        characterPopup.ShowResponse(response, () =>
        {
            tips.Value += _score * 50;
        });
    }

    private void UpdateTips()
    {
        
    }

    public void ChangeCustomer()
    {
        IncrementCustomer();
        if(Completed())
            EndGame();
        else
            StartCharacter();
    }

    private void EndGame()
    {
        
    }
}

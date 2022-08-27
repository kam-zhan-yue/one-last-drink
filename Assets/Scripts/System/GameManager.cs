using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CustomerController customerController;
    public FloatReference tips;
    public CharacterPopup characterPopup;
    public Customer customer;

    private void Awake()
    {
        if(instance && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }
    
    [Button]
    public void StartGame()
    {
        customerController.GenerateCustomerList();
    }

    [Button]
    public void StartCharacter()
    {
        GetNewCharacter();
        characterPopup.Init(customer);
    }

    private void GetNewCharacter()
    {
        customer = customerController.GetCurrentCustomer();
    }

    public void ServeCocktail(Mixer _mixer)
    {
        Cocktail cocktail = _mixer.GetCocktail();
        float score = customer.JudgeCocktail(cocktail);
        ShowResponse(customer.character, score);
    }

    public void ShowResponse(Character _character, float _score)
    {
        if (customerController.LastCustomer())
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
        customerController.IncrementCustomer();
        if(customerController.Completed())
            EndGame();
        else
            StartCharacter();
    }

    private void EndGame()
    {
        
    }

    private void OnDestroy()
    {
        instance = null;
    }
}

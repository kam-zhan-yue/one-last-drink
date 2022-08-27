using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CustomerController customerController;
    public CharacterPopup characterPopup;

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
        characterPopup.Init(customerController.GetCurrentCustomer());
    }

    public void ServeCocktail(Mixer _mixer)
    {
        Customer customer = customerController.GetCurrentCustomer();
        Cocktail cocktail = _mixer.GetCocktail();
        float score = customer.JudgeCocktail(cocktail);
        ShowResponse(customer.character, score);
    }

    public void ShowResponse(Character _character, float _score)
    {
        Response response = _character.GetResponse(_score);
        characterPopup.UpdatePanel(response);
    }

    public void ChangeCustomer()
    {
        customerController.IncrementCustomer();
        if(customerController.Completed())
            EndGame();
        else
            characterPopup.Init(customerController.GetCurrentCustomer());
    }

    private void EndGame()
    {
        
    }

    private void OnDestroy()
    {
        instance = null;
    }
}

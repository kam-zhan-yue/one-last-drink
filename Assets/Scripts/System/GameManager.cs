using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Mixer mixer;
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

    public void ServeCocktail()
    {
        Customer customer = customerController.GetCurrentCustomer();
        Cocktail cocktail = mixer.GetCocktail();
        float score = customer.JudgeCocktail(cocktail);
        customerController.IncrementCustomer();
        if(customerController.Completed())
            EndGame();
    }

    private void EndGame()
    {
        
    }

    private void OnDestroy()
    {
        instance = null;
    }
}

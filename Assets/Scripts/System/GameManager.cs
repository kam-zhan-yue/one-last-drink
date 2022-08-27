using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Bartender bartender;
    public CustomerController customerController;
    public CharacterPopup characterPopup;

    [FoldoutGroup("Testing")]
    [NonSerialized, ShowInInspector, ReadOnly]
    public Customer testCustomer;

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
        Cocktail cocktail = bartender.GetCocktail();
        float score = customer.JudgeCocktail(cocktail);
        customerController.IncrementCustomer();
        if(customerController.Completed())
            EndGame();
    }

    [FoldoutGroup("Testing")]
    [Button]
    public void GenerateCustomer(Character _character)
    {
        testCustomer = customerController.GenerateCustomer(_character);
    }
    
    [FoldoutGroup("Testing")]
    [Button]
    public float TestServeCocktail(List<Drink> _drinks)
    {
        Customer customer = customerController.GetCurrentCustomer();
        Cocktail testCocktail = new();
        for (int i = 0; i < _drinks.Count; ++i)
        {
            testCocktail.AddDrink(bartender.drinkDatabase.GetDrinkEntry(_drinks[i]));
        }
        return customer.JudgeCocktail(testCocktail);
    }

    private void EndGame()
    {
        
    }

    private void OnDestroy()
    {
        instance = null;
    }
}

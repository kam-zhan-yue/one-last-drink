using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Debugger")]
public class Debugger : ScriptableObject
{
    public DrinkDatabase drinkDatabase;
    public CustomerController customerController;

    public Customer testCustomer;
    public List<Drink> testDrinks = new();
    
    [Button]
    public void GenerateCustomer(Character _character)
    {
        testCustomer = customerController.GenerateCustomer(_character);
    }
    
    [Button]
    public float TestCocktail()
    {
        Customer customer = customerController.GetCurrentCustomer();
        Cocktail testCocktail = new();
        for (int i = 0; i < testDrinks.Count; ++i)
        {
            testCocktail.AddDrink(drinkDatabase.GetDrinkEntry(testDrinks[i]));
        }
        return customer.JudgeCocktail(testCocktail);
    }
}

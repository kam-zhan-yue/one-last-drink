using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Debugger")]
public class Debugger : ScriptableObject
{
    public CustomerController customerController;

    public Customer testCustomer;
    public Mixer testMixer;
    
    [Button]
    public void GenerateCustomer(Character _character)
    {
        testCustomer = customerController.GenerateCustomer(_character);
    }

    [Button]
    public void GenerateCustomerPrompt(Character _character, int _promptIndex)
    {
        testCustomer = customerController.GenerateTestCustomer(_character, _promptIndex);
    }
    
    [Button]
    public float TestCocktail()
    {
        Customer customer = testCustomer;
        return customer.JudgeCocktail(testMixer.GetCocktail());
    }
}

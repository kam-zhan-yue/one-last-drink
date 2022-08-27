using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(menuName = "Customer Controller")]
[InlineEditor()]
public class CustomerController : ScriptableObject
{
    public IntReference maxCharacters;
    public IntReference maxRequests;
    public CharacterDatabase characterDatabase;
    
    [NonSerialized, ShowInInspector, ReadOnly]
    public List<Customer> customerList = new();
    private readonly Random random = new();

    [Button]
    public List<Customer> GenerateCustomerList()
    {
        List<CharacterEntry> characterList = random.RandomSubList(characterDatabase.characterList, maxCharacters);
        customerList.Clear();
        for (int i = 0; i < characterList.Count; ++i)
        {
            Customer newCustomer = GenerateCustomer(characterList[i].character);
            customerList.Add(newCustomer);
        }

        return customerList;
    }

    public Customer GenerateCustomer(Character _character)
    {
        Customer customer = new(_character, maxRequests);
        return customer;
    }
}

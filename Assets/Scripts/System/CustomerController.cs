using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(menuName = "Customer Controller")]
[InlineEditor()]
public class CustomerController : ScriptableObject
{
    public int maxCustomers = 0;
    public int maxOrders = 0;
    public CharacterDatabase characterDatabase;
    
    [NonSerialized, ShowInInspector, ReadOnly]
    public List<Customer> customerList = new();
    private readonly Random random = new();

    [Button]
    public List<Customer> GenerateCustomerList()
    {
        List<CharacterEntry> characterList = random.RandomSubList(characterDatabase.characterList, maxCustomers);
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
        Customer customer = new(_character, maxOrders);
        return customer;
    }
}

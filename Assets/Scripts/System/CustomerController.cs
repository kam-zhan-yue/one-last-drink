using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(menuName = "Customer Controller")]
public class CustomerController : ScriptableObject
{
    public int maxCustomers = 0;
    public int maxPrompts = 0;
    public CharacterDatabase characterDatabase;
    
    private List<Customer> customerList = new();
    private int currentIndex = 0;
    private readonly Random random = new();

    [Button]
    public void GenerateCustomerList()
    {
        List<CharacterEntry> characterList = random.RandomSubList(characterDatabase.characterList, maxCustomers);
        customerList.Clear();
        for (int i = 0; i < characterList.Count; ++i)
        {
            Customer newCustomer = new();
            newCustomer.character = characterList[i].character;
            newCustomer.request = characterList[i].character.requestDatabase.GenerateRequest(random, maxPrompts);
            customerList.Add(newCustomer);
        }
    }

    public Customer GetCurrentCustomer()
    {
        if (currentIndex < 0 || currentIndex >= customerList.Count)
            return new();
        return customerList[currentIndex];
    }

    public bool Completed()
    {
        return currentIndex >= customerList.Count;
    }

    public void IncrementCustomer()
    {
        currentIndex++;
    }
}

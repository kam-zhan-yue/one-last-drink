using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(menuName = "Customer Controller")]
public class CustomerController : ScriptableObject
{
    public int maxCustomers = 0;
    public int maxPrompts = 0;
    public CharacterDatabase characterDatabase;
    public List<Customer> customerList = new();
    
    private Random random = new();

    public List<Customer> GenerateCustomerList()
    {
        List<CharacterEntry> characterList = random.RandomSubList(characterDatabase.characterList, maxCustomers);
        List<Customer> randomList = new();
        for (int i = 0; i < characterList.Count; ++i)
        {
            Customer newCustomer = new();
            newCustomer.character = characterList[i].character;
            newCustomer.request = characterList[i].character.requestDatabase.GenerateRequest(random, maxPrompts);
            randomList.Add(newCustomer);
        }
        return randomList;
    }
}

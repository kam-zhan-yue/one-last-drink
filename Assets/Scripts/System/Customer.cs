using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Customer
{
    public Character character;
    public List<Request> requestList = new();
    private int requestListIndex = 0;

    public Customer(Character character, int maxOrders)
    {
        this.character = character;

        for (int i = 0; i < maxOrders; ++i)
        {
            Request request = character.requestDatabase.GenerateRequest();
            if (requestList.Contains(request))
            {
                --i;
                continue;
            }
            
            requestList.Add(request);
        }
    }

    public float JudgeCocktail(Cocktail _cocktail)
    {
        DrinkStats requestStats = requestList[requestListIndex++].GetStats();
        DrinkStats cocktailStats = _cocktail.GetStats();
        return requestStats.Compare(cocktailStats);
    }

    public Response GetResponse(float _score)
    {
        return character.GetResponse(_score);
    }
}
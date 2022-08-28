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

    public Customer(Character _character, int _promptIndex, bool _debug)
    {
        character = _character;
        Prompt prompt = character.requestDatabase.promptList[_promptIndex];
        List<Prompt> promptList = new();
        promptList.Add(prompt);
        requestList.Add(new Request(promptList));
    }

    public float JudgeCocktail(Cocktail _cocktail)
    {
        if (requestList.Count <= 0f)
            return 0f;
        Request request = requestListIndex >= requestList.Count ? requestList[^1] : requestList[requestListIndex];
        DrinkStats requestStats = request.GetStats();
        DrinkStats cocktailStats = _cocktail.GetStats();
        return requestStats.Compare(cocktailStats);
    }

    public void IncrementRequest()
    {
        requestListIndex++;
    }

    public Response GetResponse(float _score)
    {
        return character.GetResponse(_score);
    }

    public bool HasRequest()
    {
        return requestListIndex < requestList.Count;
    }
    

    public Request GetCurrentRequest()
    {
        if (requestListIndex < 0 || requestListIndex >= requestList.Count)
            return new Request();
        return requestList[requestListIndex];
    }
}
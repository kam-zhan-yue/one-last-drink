using System;

[Serializable]
public class Customer
{
    public Character character;
    public Request request;

    public Customer(Character character)
    {
        this.character = character;
        request = character.requestDatabase.GenerateRequest();
    }

    public float JudgeCocktail(Cocktail _cocktail)
    {
        DrinkStats requestStats = request.GetStats();
        DrinkStats cocktailStats = _cocktail.GetStats();
        return requestStats.Compare(cocktailStats);
    }

    public Response GetResponse(float _score)
    {
        return character.GetResponse(_score);
    }
}
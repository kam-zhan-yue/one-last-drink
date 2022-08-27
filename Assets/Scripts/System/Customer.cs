using System;

[Serializable]
public class Customer
{
    public Character character;
    public Request request = new Request();

    public float JudgeCocktail(Cocktail _cocktail)
    {
        DrinkStats requestStats = request.GetStats();
        DrinkStats cocktailStats = _cocktail.GetStats();
        return requestStats.Compare(cocktailStats);
    }
}
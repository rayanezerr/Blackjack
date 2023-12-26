namespace Blackjack;

public class Card
{
    public string Type { get; }
    public string Rank { get; }

    public Card(string type, string rank)
    {
        Type = type;
        Rank = rank;
    }

    public int GetCardValue()
    {
        int cardValue;

        if (int.TryParse(Rank, out cardValue))
        {
            return cardValue;
        }
        else if (Rank == "Jack" || Rank == "Queen" || Rank == "King")
        {
            return 10;
        }
        return 0;
    }
    
    //For testing
    public override string ToString()
    {
        return $"{Rank} of {Type}";
    }
}
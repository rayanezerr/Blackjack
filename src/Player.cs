namespace Blackjack;

public class Player
{
    private List<Card> hand;

    public Player()
    {
        hand = new List<Card>();
    }

    public List<Card> Hand
    {
        get { return hand; }
    }

    public void AddCard(Card card)
    {
        hand.Add(card);
    }

    public int HandValue()
    {
        int value = 0;
        
        foreach (Card card in hand)
        {
            value += card.GetCardValue();
        }
        
        return value;
    }

    public int DealersVisibleHandValue()
    {
        int value = 0;
        
        for (int i = 1; i < hand.Count; i++)
        {
            value += hand[i].GetCardValue();
        }

        return value;
    }
}
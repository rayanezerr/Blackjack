using System.Windows.Documents;

namespace Blackjack;

public class CardDeck
{
    private List<Card> cardsList;

    public CardDeck()
    {
        cardsList = new List<Card>();

        string[] ranks = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
        string[] types = { "Hearts", "Diamonds", "Spades", "Clubs" };

        foreach (string type in types)
        {
            foreach (string rank in ranks)
            {
                cardsList.Add(new Card(type, rank));
            }
        }
    }

    public int Length()
    {
        return cardsList.Count;
    }

    public Card this[int index]
    {
        get
        {
            if (index >= 0 && index < cardsList.Count)
            {
                return cardsList[index];
            }
            else
            {
                throw new IndexOutOfRangeException("Max 51");
            }
        }
    }

    public void Shuffle()
    {
        Random rand = new Random();
        for (int i = cardsList.Count-1; i > 0; i--)
        {
            int k = rand.Next(i + 1);
            (cardsList[k], cardsList[i]) = (cardsList[i], cardsList[k]);
        }
    }

    public Card DrawCard()
    {
        Card drawnCard = cardsList[0];
        cardsList.RemoveAt(0);
        return drawnCard;
    }

    public bool IsDeckEmpty()
    {
        if (cardsList.Count == 0)
        {
            return true;
        }
        return false;
    }
}
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


    public void Shuffle()
    {
        Random rand = new Random();
        for (int i = cardsList.Count; i > 1; i--)
        {
            int k = rand.Next(i + 1);
            Card value = cardsList[k];
            cardsList[k] = cardsList[i];
            cardsList[i] = value;
        }
    }
}
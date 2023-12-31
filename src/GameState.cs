namespace Blackjack;

public class GameState
{
    public CardDeck deck;
    public Player player;
    public Player dealer;
    public Player CurrentPlayer;
    // public bool GameOver { get; private set; }
    //
    // public Player winner;
    // public event Action PlayerTurnStarted;
    public event Action Tie;
    public event Action<bool> PlayerLose; //Boolean value true if busted 
    public event Action<bool> DealerLose; //Boolean value true if busted
    public event Action<Player> GameEnded;  //Winner

    public GameState()
    {
        deck = new CardDeck();
        player = new Player();
        dealer = new Player();
        CurrentPlayer = player;
        deck.Shuffle();
        player.AddCard(deck.DrawCard());
        player.AddCard(deck.DrawCard());
        dealer.AddCard(deck.DrawCard());
        dealer.AddCard(deck.DrawCard());
    }

    public bool IsBlackJack(Player p)
    {
        if (p.HandValue() == 21)
        {
            return true;
        }
        return false;
    }

    public bool IsDealerAbove()
    {
        if (dealer.HandValue() >= 17)
        {
            return true;
        }

        return false;
    }

    public bool IsBusted(Player p)
    {
        if (p.HandValue() > 21)
        {
            return true;
        }
        return false;
    }

    public void Hit()
    {
        player.AddCard(deck.DrawCard());

        if (IsBusted(player))
        {
            PlayerLose?.Invoke(true);
            GameEnded?.Invoke(dealer);
        }

        if (IsBlackJack(player))
        {
            DealerLose?.Invoke(false);
            GameEnded?.Invoke(player);
        }
    }

    public void Stand()
    {
        StartDealerTurn();
        if (IsBlackJack(dealer))
        {
            PlayerLose?.Invoke(false);
            GameEnded?.Invoke(dealer);
        }
    }

    public void StartDealerTurn()
    {
        while (!IsDealerAbove() && !IsBusted(dealer))
        {
            dealer.AddCard(deck.DrawCard());
        }
        Console.WriteLine(dealer.HandValue());
        if (IsBusted(dealer))
        {
            DealerLose?.Invoke(true);
            GameEnded?.Invoke(player);
        }
        else
        {
            if (dealer.HandValue() > player.HandValue())
            {
                PlayerLose?.Invoke(false);
                GameEnded?.Invoke(dealer);
            }
            else if (dealer.HandValue() < player.HandValue())
            {
                DealerLose?.Invoke(false);
                GameEnded?.Invoke(player);
            }
            else
            {
                Tie?.Invoke();
                GameEnded?.Invoke(null);
            }
        }
    }
}
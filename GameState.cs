namespace Blackjack;

public class GameState
{
    public CardDeck deck;
    public Player player;
    public Player dealer;
    public Player CurrentPlayer;
    public bool GameOver { get; private set; }
    
    public Player winner;
    public event Action PlayerTurnStarted;
    public event Action Tie;
    public event Action<bool> PlayerLose; //Boolean value true if busted 
    public event Action<bool> DealerLose; //Boolean value true if busted
    public event Action GameEnded; 

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

    public bool IsFirstBlackJack(Player p)
    {
        if (p.Hand.Count == 2 && p.HandValue() == 21)
        {
            return true;
        }

        return false;
    }

    public Player SwitchPlayer()
    {
        if (CurrentPlayer == player)
        {
            CurrentPlayer = dealer;
            return CurrentPlayer;
        }
        else
        {
            CurrentPlayer = player;
            return CurrentPlayer;
        }
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

    // public void StartPlayerTurn()
    // {
    //     PlayerTurnStarted?.Invoke();
    // }

    public void Hit()
    {
        CurrentPlayer.AddCard(deck.DrawCard());

        if (IsBusted(CurrentPlayer))
        {
            PlayerLose?.Invoke(true);
            GameEnded?.Invoke();
        }
    }

    public void Stand()
    {
        StartDealerTurn();
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
        }
        else
        {
            if (dealer.HandValue() > player.HandValue())
            {
                PlayerLose?.Invoke(false);
                GameEnded?.Invoke();
            }
            else if (dealer.HandValue() < player.HandValue())
            {
                DealerLose?.Invoke(false);
                GameEnded?.Invoke();
            }
            else
            {
                Tie?.Invoke();
            }
        }
    }
}
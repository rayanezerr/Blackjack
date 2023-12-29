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
    public event Action PlayerHit;
    public event Action PlayerTurnEnded; 
    public event Action DealerTurnStarted; 
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
            GameOver = true;
            winner = SwitchPlayer();
            return true;
        }

        return false;
    }

    public void StartPlayerTurn()
    {
        PlayerTurnStarted?.Invoke();
    }

    public void Hit()
    {
        CurrentPlayer.AddCard(deck.DrawCard());

        if (IsBusted(CurrentPlayer))
        {
            PlayerTurnEnded?.Invoke();
        }

        else
        {
            PlayerHit?.Invoke();
        }
    }

    public void Stand()
    {
        if (CurrentPlayer == player)
        {
            PlayerTurnEnded?.Invoke();
            StartDealerTurn();
        }
    }

    public void StartDealerTurn()
    {
        DealerTurnStarted?.Invoke();

        while (!IsDealerAbove() && !IsBusted(dealer))
        {
            dealer.AddCard(deck.DrawCard());
        }
        
        GameEnded?.Invoke();
    }
}
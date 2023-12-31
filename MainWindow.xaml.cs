using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Blackjack;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private int totalChips = 1000;
    private int z = 2;
    private List<Image> playerUiImage = new List<Image>();
    private List<Image> dealerUiImage = new List<Image>();
    
    private int betMoney;
    private GameState game;
    public MainWindow()
    {
        game = new GameState();
        InitializeComponent();
        SetUpUI();
        UpdateBetUI();
        AttachEvent();
    }

    private void SetUpUI()
    {
        playerUiImage.Clear();
        dealerUiImage.Clear();
        for (int i = 1; i <= 8; i++)
        {
            playerUiImage.Add((Image)FindName($"PlayerCard{i}"));
        }

        for (int i = 1; i <= 7; i++)
        {
            dealerUiImage.Add((Image)FindName($"DealerCard{i}"));
        }
    }

    private void AttachEvent()
    {
        game.PlayerLose += OnPlayerLose;
        game.DealerLose += OnDealerLose;
        game.Tie += OnTie;
        game.GameEnded += OnGameEnded;
    }
    private string GetCardPath(Card card)
    {
        return $"{card.Rank.ToLower()}_of_{card.Type.ToLower()}.png";
    }

    private ImageSource UpdateImageSource(string path)
    {
        ImageSource source = new BitmapImage(new Uri($"pack://application:,,,/assets/{path}"));
        return source;
    }
    
    private void UpdatePlayerHandUI()
    {
        for (int i = 0; i < z; i++)
        {
            playerUiImage[i].Source = UpdateImageSource(GetCardPath(game.player.Hand[i]));
        }

        PlayerValueText.Text = $"Hand value : {game.player.HandValue()}";
    }

    private async void UpdateDealerHandUI(bool allCards = false)
    {
        int i = 0;
        if (allCards)
        {
            i = 0;
        }
        else
        {
            i = 1;
            dealerUiImage[0].Source = new BitmapImage(new Uri($"pack://application:,,,/assets/back.png"));
            DealerValueText.Text = $"Dealer's hand value : {game.dealer.DealersVisibleHandValue()}";
        }
        while (i < game.dealer.Hand.Count)
        { 
            await Task.Delay(500);
            dealerUiImage[i].Source = UpdateImageSource(GetCardPath(game.dealer.Hand[i]));
            i++;
        }
    }

    private void UpdateBetUI()
    {
        CurrentChips.Text = $"Chips : {totalChips}";
    }
    
    private void Bet_Click(object sender, RoutedEventArgs e)
    {
        if (BetBox.Text != "")
        {
            betMoney = int.Parse(BetBox.Text);
            UpdatePlayerHandUI();
            UpdateDealerHandUI();
            if (betMoney > totalChips)
            {
                betMoney = totalChips;
            }
            totalChips -= betMoney;
            UpdateBetUI();
            BetGrid.Visibility = Visibility.Hidden;
            GameGrid.Visibility = Visibility.Visible;
        }
    }
    private void Hit_Click(object sender, RoutedEventArgs e)
    {
        game.Hit();
        z += 1;
        UpdatePlayerHandUI();
    }

    private async void Stand_Click(object sender, RoutedEventArgs e)
    {
        game.Stand();
        HitButton.Visibility = Visibility.Hidden;
        UpdateDealerHandUI(true);
        await Task.Delay(game.dealer.Hand.Count *500);
        DealerValueText.Text = $"Dealer's hand value : {game.dealer.HandValue()}";
    }

    private async void OnPlayerLose(bool busted)
    {
        HideButtons();
        await Task.Delay(1000 + game.dealer.Hand.Count*500);
        if (busted)
        {
            EndText.Text = "Player busted, dealer wins";
        }
        else
        {
            EndText.Text = "Player loses";
        }
        EndGrid.Visibility = Visibility.Visible;
        
    }

    private async void OnDealerLose(bool busted)
    {
        HideButtons();
        await Task.Delay(1000 + game.dealer.Hand.Count*500);
        if (busted)
        {
            EndText.Text = "Dealer busted, player wins";
        }
        else
        {
            EndText.Text = "Player wins";
        }
        // GameGrid.Visibility = Visibility.Hidden;
        EndGrid.Visibility = Visibility.Visible;
       
    }

    private async void OnTie()
    {
        await Task.Delay(1000 + game.dealer.Hand.Count*500);
        EndText.Text = "Push";
        EndGrid.Visibility = Visibility.Visible;
        
        
    }

    private void HideButtons()
    {
        StandButton.Visibility = Visibility.Hidden;
        HitButton.Visibility = Visibility.Hidden;
    }

    private void ShowButtons()
    {
        StandButton.Visibility = Visibility.Visible;
        HitButton.Visibility = Visibility.Visible;
    }
    

    private async void OnGameEnded(Player winner = null)
    {
        await Task.Delay(2500 + (game.dealer.Hand.Count)*500);
        GameGrid.Visibility = Visibility.Hidden;
        EndGrid.Visibility = Visibility.Hidden;
        InfoGrid.Visibility = Visibility.Visible;
       

        if (winner == game.player)
        {
            InfoText.Text = $"You won {betMoney*2} chips";
            totalChips += betMoney * 2;
        }

        else if (winner == game.dealer)
        {
            InfoText.Text = $"You lost {betMoney} chips";
        }
        else
        {
            InfoText.Text = $"You got your chips back";
            totalChips += betMoney;
        }
        UpdateBetUI();
        await Task.Delay(1500);
        RestartGame();
    }

    private void RestartGame()
    {
        game = new GameState();
        AttachEvent();
        BetGrid.Visibility = Visibility.Visible;
        GameGrid.Visibility = Visibility.Hidden;
        EndGrid.Visibility = Visibility.Hidden;
        InfoGrid.Visibility = Visibility.Hidden;
        ShowButtons();

        foreach (Image image in dealerUiImage)
        {
            image.Source = null;
        }
        foreach (Image image in playerUiImage)
        {
            image.Source = null;
        }
        
        z = 2;
        SetUpUI();
        UpdateBetUI();
    }
    
}
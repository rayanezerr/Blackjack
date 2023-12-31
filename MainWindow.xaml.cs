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
    private int y = 2;
    private List<Image> playerUiImage = new List<Image>();
    private List<Image> dealerUiImage = new List<Image>();
    
    private int betMoney;
    private readonly GameState game = new GameState();
    public MainWindow()
    {
        InitializeComponent();
        SetUpUI();
        UpdateBetUI();
        game.PlayerLose += OnPlayerLose;
        game.DealerLose += OnDealerLose;
        game.Tie += OnTie;
    }

    private void SetUpUI()
    {
        playerUiImage.Add(PlayerCard1);
        playerUiImage.Add(PlayerCard2);
        playerUiImage.Add(PlayerCard3);
        playerUiImage.Add(PlayerCard4);
        playerUiImage.Add(PlayerCard5);
        playerUiImage.Add(PlayerCard6);
        playerUiImage.Add(PlayerCard7);
        playerUiImage.Add(PlayerCard8);
        dealerUiImage.Add(DealerCard1);
        dealerUiImage.Add(DealerCard2);
        dealerUiImage.Add(DealerCard3);
        dealerUiImage.Add(DealerCard4);
        dealerUiImage.Add(DealerCard5);
        dealerUiImage.Add(DealerCard6);
        dealerUiImage.Add(DealerCard7);
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
    private void Hit_Click(object sender, RoutedEventArgs e)
    {
        game.Hit();
        z += 1;
        UpdatePlayerHandUI();
        
        Console.WriteLine(GetCardPath(game.player.Hand[game.player.Hand.Count-1]));   
    }
    
    private void Stand_Click(object sender, RoutedEventArgs e)
    {
        game.Stand();
        HitButton.Visibility = Visibility.Hidden;
        UpdateDealerHandUI(true);
    }

    private async void OnPlayerLose(bool busted)
    {
        HideButtons();
        await Task.Delay(1000);
        if (busted)
        {
            EndText.Text = "Player busted, dealer win";
        }
        else
        {
            EndText.Text = "Player lose";
        }
        // GameGrid.Visibility = Visibility.Hidden;
        EndGrid.Visibility = Visibility.Visible;
        SummaryScreen(game.dealer);
    }

    private async void OnDealerLose(bool busted)
    {
        HideButtons();
        await Task.Delay(1000);
        if (busted)
        {
            EndText.Text = "Dealer busted, player win";
        }
        else
        {
            EndText.Text = "Player win";
        }
        // GameGrid.Visibility = Visibility.Hidden;
        EndGrid.Visibility = Visibility.Visible;
        SummaryScreen(game.player);
    }

    private async void OnTie()
    {
        await Task.Delay(1000);
        EndText.Text = "Push";
        EndGrid.Visibility = Visibility.Visible;
        SummaryScreen();
        
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
    

    private async void SummaryScreen(Player winner = null)
    {
        await Task.Delay(2500);
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
        
    }
}
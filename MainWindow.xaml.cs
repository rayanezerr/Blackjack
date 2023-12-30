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
    private int z = 2;
    private List<Image> imageList = new List<Image>();
    
    private int betMoney;
    private readonly GameState game = new GameState();
    public MainWindow()
    {
        InitializeComponent();
        imageList.Add(PlayerCard1);
        imageList.Add(PlayerCard2);
        imageList.Add(PlayerCard3);
        imageList.Add(PlayerCard4);
        imageList.Add(PlayerCard5);
    }

    private string GetCardPath(Card card)
    {
        return $"{card.Rank.ToLower()}_of_{card.Type.ToLower()}.png";
    }

    private ImageSource UpdateImageSource(string path)
    {
        ImageSource source = new BitmapImage(new Uri($"pack://application:,,,/assets2/{path}"));
        return source;
    }
    
    private void UpdatePlayerHandUI()
    {
        
        for (int i = 0; i < z; i++)
        {
            imageList[i].Source = UpdateImageSource(GetCardPath(game.player.Hand[i]));
        }
    }

    private void UpdateDealerHandUI(bool allCards = false)
    {
        
    }

    private void UpdateBetUI()
    {
        
    }
    
    private void Bet_Click(object sender, RoutedEventArgs e)
    {
        betMoney = int.Parse(BetBox.Text);
        UpdatePlayerHandUI();
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
    }
    
    
    
    
    
    
    
    
    
    
    
    
}
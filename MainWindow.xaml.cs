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

namespace Blackjack;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        // test();
        test2();
    }

    
    //Testing functions
    public void test()
    {
        CardDeck oeoe = new CardDeck();
        oeoe.Shuffle();
        for (int i = 0; i < oeoe.Length(); i++)
        {
            Console.WriteLine(oeoe[i]);
        }
        Console.WriteLine("=====================");
        Console.WriteLine(oeoe.Length());
        for (int i = 0; i < 52; i++)
        {
            oeoe.DrawCard();
        }
        Console.WriteLine(oeoe.Length());
        Console.WriteLine("=====================");
        Console.WriteLine(oeoe.IsDeckEmpty());
        for (int i = 0; i < oeoe.Length(); i++)
        {
            Console.WriteLine(oeoe[i]);
        }
    }


    public void test2()
    {
        Player player = new Player();
        Player dealer = new Player();
        CardDeck deck = new CardDeck();
        
        deck.Shuffle();
        
        player.AddCard(deck.DrawCard());
        player.AddCard(deck.DrawCard());
        dealer.AddCard(deck.DrawCard());
        dealer.AddCard(deck.DrawCard());
        dealer.AddCard(deck.DrawCard());
        dealer.AddCard(deck.DrawCard());
        player.AddCard(deck.DrawCard());

        Console.WriteLine(player.HandValue());
        Console.WriteLine(dealer.DealersVisibleHandValue());
        Console.WriteLine(dealer.HandValue());

        for (int i = 0; i < player.Hand.Count; i++)
        {
            Console.WriteLine(player.Hand[i]);
        }
        
        for (int i = 0; i < dealer.Hand.Count; i++)
        {
            Console.WriteLine(dealer.Hand[i]);
        }
        
        



    }
}
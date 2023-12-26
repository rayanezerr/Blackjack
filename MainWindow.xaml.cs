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
        test();
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
}
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

using System.Windows.Controls;

namespace Blackjack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        protected static Random rnd = new Random();
        private static MainWindow window = null;
        public static String actieveSpeler;

        public static GameState gameState;

        public MainWindow()
        {
            InitializeComponent();
            window = this;

            Utils.UpdateTimer();
            new Player(PlayerType.Speler, "Speler", 250);
            new Player(PlayerType.Huis, "Huis");

            Utils.Shuffle();
            AantalKaarten.Text = Cards.CardList.Count.ToString();
            Kapitaal.Text = Player.GetPlayer(PlayerType.Speler).GetMoney().ToString();

        }

        private void Deel_Click(object sender, RoutedEventArgs e)
        {
           
            int SetBet = (int)Math.Round(Bet.Value, 0);
            if (Utils.ValidateMoney(SetBet, Player.GetPlayer(PlayerType.Speler)))
            {
                KnoppenUtils.DrawCards();
            }

           
        }

        private void Hit_Click(object sender, RoutedEventArgs e)
        {
            if (gameState.Equals(GameState.Running))
            {
                Cards Kaart = Utils.RandomCard();
                Player.GetPlayer(PlayerType.Speler).AddCard(Kaart);
                Utils.HandleCards(Kaart, KaartenSpeler);
                txtSpelerTotaal.Text = Player.GetPlayer(PlayerType.Speler).TotalCardNumber().ToString();
                ImageHandler.revealImage();
              
                Double.IsEnabled = false;
                KnoppenUtils.CheckState();
            }
            else
            {
                MessageBox.Show("Je moet eerst delen!");
            }
        }



        private void Sta_Click(object sender, RoutedEventArgs e)
        {
            KnoppenUtils.Stand();
        }

        private void Historiek_Click(object sender, RoutedEventArgs e)
        {
            History history = new History();
            history.Show();
        }


        public static MainWindow GetClass()
        {
            return window;
        }

        private void Bet_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BetBlock.Text = Math.Round(Bet.Value, 0).ToString();

        }

        private void Double_Click(object sender, RoutedEventArgs e)
        {
            int currBet = Player.GetPlayer(PlayerType.Speler).GetBet();
            if (Utils.ValidateMoney(currBet * 2, Player.GetPlayer(PlayerType.Speler)))
            {
                Double.IsEnabled = false;
                Player.GetPlayer(PlayerType.Speler).SetBet(currBet * 2);
                MessageBox.Show("Doubled!");

                ImageHandler.AssignDoubleDownImage();

                window.Inzet.Text = Player.GetPlayer(PlayerType.Speler).GetBet().ToString();
                Sta_Click(sender, e);
            }
        }

        private void Debug_Click(object sender, RoutedEventArgs e)
        {
            if(HuisGrid.Visibility == Visibility.Visible)
            {
                HuisGrid.Visibility = Visibility.Hidden;
            } 
            else
            {
                HuisGrid.Visibility = Visibility.Visible;
            }


            if (SpelerGrid.Visibility == Visibility.Visible)
            {
                SpelerGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                SpelerGrid.Visibility = Visibility.Visible;
            }
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            Utils.RestartGame();
        }

    }


}
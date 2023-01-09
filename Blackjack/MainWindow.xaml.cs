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

            new Speler(PlayerType.Speler, "Speler", 250);
            new Speler(PlayerType.Huis, "Huis");

            Utils.Shuffle();
            AantalKaarten.Text = Kaarten.KaartenLijst.Count.ToString();
            Kapitaal.Text = Speler.GetSpeler(PlayerType.Speler).GetGeld().ToString();

        }

        private void Deel_Click(object sender, RoutedEventArgs e)
        {
           
            int SetBet = (int)Math.Round(Bet.Value, 0);
            if (Utils.ValidateMoney(SetBet, Speler.GetSpeler(PlayerType.Speler)))
            {
                KnoppenUtils.Deel();
            }

           
        }

        private void Hit_Click(object sender, RoutedEventArgs e)
        {
            if (gameState.Equals(GameState.Running))
            {
                Kaarten Kaart = Utils.randomKaart();
                Speler.GetSpeler(PlayerType.Speler).VoegKaartToe(Kaart);
                Utils.handleCards(Kaart, KaartenSpeler);
                txtSpelerTotaal.Text = Speler.GetSpeler(PlayerType.Speler).TotaalAantal().ToString();
                ImageHandler.revealImage();
              
                Double.IsEnabled = false;
                KnoppenUtils.checkState();
            }
            else
            {
                MessageBox.Show("Je moet eerst delen!");
            }
        }



        private void Sta_Click(object sender, RoutedEventArgs e)
        {
            KnoppenUtils.Sta();
        }

        private void Historiek_Click(object sender, RoutedEventArgs e)
        {
            
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
            int currBet = Speler.GetSpeler(PlayerType.Speler).GetBet();
            if (Utils.ValidateMoney(currBet * 2, Speler.GetSpeler(PlayerType.Speler)))
            {
                Double.IsEnabled = false;
                Speler.GetSpeler(PlayerType.Speler).SetBet(currBet * 2);
                MessageBox.Show("Doubled!");

                ImageHandler.AssignDoubleDownImage();

                window.Inzet.Text = Speler.GetSpeler(PlayerType.Speler).GetBet().ToString();
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
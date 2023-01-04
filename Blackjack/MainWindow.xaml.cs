using System;
using System.Collections.Generic;
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

                ImageHandler.setImage(Kaart.getBitmap());
                Double.IsEnabled = false;
                KnoppenUtils.Reset();
            }
            else
            {
                MessageBox.Show("Je moet eerst delen!");
            }
        }



        private void Sta_Click(object sender, RoutedEventArgs e)
        {
            Double.IsEnabled = false;
            while (Speler.GetSpeler(PlayerType.Huis).TotaalAantal() < 16)
            {
                Kaarten kaart = Utils.randomKaart();
                Speler.GetSpeler(PlayerType.Huis).VoegKaartToe(kaart);
                Utils.handleCards(kaart, KaartenHuis);
                txtHuisTotaal.Text = Speler.GetSpeler(PlayerType.Huis).TotaalAantal().ToString();
            }
            KnoppenUtils.Staan();

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
                window.Inzet.Text = Speler.GetSpeler(PlayerType.Speler).GetBet().ToString();
            }
        }

    }


}
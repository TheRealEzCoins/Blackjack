using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Blackjack
{
    internal class Utils
    {
        private static Random rnd = new Random();
        private static bool isRevealed = false;

        public static bool GetRandomBool()
        {

            int cf = rnd.Next(1, 2);

            if (cf == 1)
                return true;
            else
                return false;
        }

        public static Kaarten randomKaart()
        {
            int kaart = rnd.Next(Kaarten.KaartenLijst.Count);
            return Kaarten.KaartenLijst[kaart];
        }


        public static void handleCards(Kaarten kaart, TextBlock block)
        {
            MainWindow window = MainWindow.GetClass();
            if (block == window.KaartenSpeler)
                block.Text = Speler.GetSpeler(PlayerType.Speler).VertaalKaart();
            if (block == window.KaartenHuis)
            {
                block.Text = Speler.GetSpeler(PlayerType.Huis).VertaalKaart();
            }
            Kaarten.KaartenLijst.Remove(kaart);
        }

        public static void handleHiddenCard(Kaarten kaart, TextBlock block)
        {
            MainWindow window = MainWindow.GetClass();
            if (block == window.KaartenSpeler)
                block.Text = Speler.GetSpeler(PlayerType.Speler).VertaalKaart();
            if (block == window.KaartenHuis)
            {
                if (isRevealed)
                {
                    int nr = kaart.getNummer();
                    string naam = kaart.getNaam();
                    block.Text = block.Text + nr + " " + naam + "\n";
                }
                else
                {
                    block.Text = block.Text + "????" + "\n";
                    isRevealed = true;
                }
            }
            Kaarten.KaartenLijst.Remove(kaart);
        }

        public static string readList(List<Kaarten> list)
        {
            string s = "";

            foreach (Kaarten kaart in list)
            {
                string KaartNaam = kaart.getNaam();
                string KaartNr = kaart.getNummer().ToString();
                s = s + "\n" + KaartNaam;
            }

            return s;
        }

        public static void Shuffle()
        {
            String[] KaartenNamenIrregulair = { "Schoppen", "Harten", "Klaveren", "Ruiten" };
            String[] KaartenNamenRegulair = { "boer", "vrouw", "heer" };
            MainWindow.gameState = GameState.Stopped;

            for (int i = 2; i < 11; i++)
            {
                foreach (string s in KaartenNamenIrregulair)
                {
                    if (!Kaarten.KaartenLijst.Contains(new Kaarten(s, i)))
                    {
                        new Kaarten(s, i);
                    }
                }
            }

            foreach (string s in KaartenNamenRegulair)
            {
                if (!Kaarten.KaartenLijst.Contains(new Kaarten(s, 10)))
                {
                    new Kaarten(s, 10);
                }
            }

            for (int i = 1; i < 5; i++)
            {
                BitmapImage image = new BitmapImage(new Uri("aas.jpg", UriKind.Relative));
                new Kaarten("aas", 1, image);
            }
        }

        public static void restartGame()
        {
            MainWindow mainWindow = MainWindow.GetClass();
            foreach (Speler speler in Speler.Spelers)
            {
                speler.getKaarten().Clear();
            }
            mainWindow.Hit.IsEnabled = false;
            mainWindow.Sta.IsEnabled = false;
            mainWindow.Deel.IsEnabled = true;
            mainWindow.KaartenHuis.Text = "";
            mainWindow.KaartenSpeler.Text = "";
            mainWindow.txtHuisTotaal.Text = "";
            mainWindow.txtSpelerTotaal.Text = "";
            Shuffle();
        }

        public static bool ValidateMoney(int input, Speler speler)
        {
            int spelerGeld = speler.GetGeld();
            int minBet = (spelerGeld / 100) * 10;
            if (input < spelerGeld && input > minBet)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Invalid Input!");
                return false;
            }
        }

        public static void updateKapitaal()
        {
            MainWindow.GetClass().Kapitaal.Text = Speler.GetSpeler(PlayerType.Speler).GetGeld().ToString();
        }

        public static void lose()
        {
            int bet = Speler.GetSpeler(PlayerType.Speler).GetBet();
            int geld = Speler.GetSpeler(PlayerType.Speler).GetGeld();
            int newGeld = geld - bet;
            MessageBox.Show("You Lose!");
            Speler.GetSpeler(PlayerType.Speler).SetGeld(newGeld);
            Speler.GetSpeler(PlayerType.Speler).SetBet(0);
            updateKapitaal();
            Utils.restartGame();
            return;
        }

        public static void win()
        {
            int bet = Speler.GetSpeler(PlayerType.Speler).GetBet();
            int geld = Speler.GetSpeler(PlayerType.Speler).GetGeld();
            MessageBox.Show("You win!");
            Speler.GetSpeler(PlayerType.Speler).SetGeld(geld + (bet * 2));
            Speler.GetSpeler(PlayerType.Speler).SetBet(0);
            updateKapitaal();
            Utils.restartGame();
            return;
        }

        public static void draw()
        {
            MessageBox.Show("Draw!"); 
            Speler.GetSpeler(PlayerType.Speler).SetBet(0);
            updateKapitaal();
            Utils.restartGame();
            return;
        }
    }

  


}

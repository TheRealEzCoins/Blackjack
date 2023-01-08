using System;
using System.Collections.Generic;
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
            if (Kaarten.KaartenLijst.Count == 0)
            {
                Utils.Shuffle();
            }
            MainWindow window = MainWindow.GetClass();
            if (block == window.KaartenSpeler)
            {
                block.Text = Speler.GetSpeler(PlayerType.Speler).VertaalKaart();
                int i = Speler.GetSpeler(PlayerType.Speler).getKaarten().Count - 1;
                ImageHandler.setImage(kaart, Speler.GetSpeler(PlayerType.Speler));
            }
            if (block == window.KaartenHuis)
            {
                block.Text = Speler.GetSpeler(PlayerType.Huis).VertaalKaart();
                ImageHandler.setImage(kaart, Speler.GetSpeler(PlayerType.Huis));
            }
            Kaarten.KaartenLijst.Remove(kaart);
            window.AantalKaarten.Text = Kaarten.KaartenLijst.Count.ToString();
        }

        public static void handleHiddenCard(Kaarten kaart, TextBlock block)
        {
            MainWindow window = MainWindow.GetClass();
            if (block == window.KaartenSpeler)
                block.Text = Speler.GetSpeler(PlayerType.Speler).VertaalKaart();
            if (block == window.KaartenHuis)
            {
                if (!isRevealed)
                {
                    block.Text = block.Text + "????" + "\n";
                    isRevealed = true;
                    ImageHandler.setHiddenImage(Speler.GetSpeler(PlayerType.Huis));
                }
                else
                {
                    int nr = kaart.getNummer();
                    string naam = kaart.getNaam();
                    block.Text = block.Text + nr + " " + naam + "\n";
                    ImageHandler.setImage(kaart, Speler.GetSpeler(PlayerType.Huis));
                }
            }
            Kaarten.KaartenLijst.Remove(kaart);
            window.AantalKaarten.Text = Kaarten.KaartenLijst.Count.ToString();
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
            String[] KaartenNamenRegulair = { "Boer", "Dame", "Koning" };
            MainWindow.gameState = GameState.Stopped;
           

            for (int i = 2; i < 11; i++)
            {
                foreach (string s in KaartenNamenIrregulair)
                {
                    if (!Kaarten.KaartenLijst.Contains(new Kaarten(s, i, GetBitmapImage(s, i))))
                    {
                        new Kaarten(s, i, GetBitmapImage(s, i));
                    }
                }
            }


            foreach(string irr in KaartenNamenIrregulair)
            {
                foreach(string s in KaartenNamenRegulair)
                {
                  new Kaarten(irr + " " + s, 10, GetBitmapImage(irr, s));
                }
            }


            foreach(String s in KaartenNamenIrregulair)
            {
                new Kaarten(s, 0, GetBitmapImage(s, 0), true);
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
            mainWindow.Inzet.Text = "0";
            isRevealed = false;
            ImageHandler.IsHidden = true;
            mainWindow.SpelerCanvas.Children.Clear();
            mainWindow.HuisCanvas.Children.Clear();
            MainWindow.gameState = GameState.Stopped;
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

        public static BitmapImage GetBitmapImage(String naam, int nummer)
        {
            if(nummer == 0)
                return new BitmapImage(new Uri("img/" + naam + "-aas" + ".png", UriKind.Relative));
            else
                return new BitmapImage(new Uri("img/" + naam + "-" + nummer + ".png", UriKind.Relative));

          
        }

        public static BitmapImage GetBitmapImage(String vorm, String naam)
        {        
                return new BitmapImage(new Uri("img/" + vorm + "-" + naam + ".png", UriKind.Relative));
        }

        public static Image TranslateBitMapImage(BitmapImage bitmapImage)
        {
            BitmapImage image = bitmapImage;
            Image img = new Image();
            img.Source = image;
            return img;
        }
    }
        
  


}

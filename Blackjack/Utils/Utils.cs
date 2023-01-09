using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Blackjack
{
    internal class Utils
    {
        private static Random rnd = new Random();
        private static bool isRevealed = false;
        private static DispatcherTimer dispatcher;
        private static int Round = 1;
        public static String History = "";

        public static bool GetRandomBool()
        {

            int cf = rnd.Next(1, 2);

            if (cf == 1)
                return true;
            else
                return false;
        }

        public static Cards RandomCard()
        {
            int kaart = rnd.Next(Cards.CardList.Count);
            return Cards.CardList[kaart];
        }

        // Handles each image's card interaction 
        public static void HandleCards(Cards kaart, TextBlock block)
        {
            if (Cards.CardList.Count == 0)
            {
                Utils.Shuffle();
            }
            MainWindow window = MainWindow.GetClass();
            if (block == window.KaartenSpeler)
            {
                block.Text = Player.GetPlayer(PlayerType.Speler).TranslateCard();
                int i = Player.GetPlayer(PlayerType.Speler).GetCards().Count - 1;
                ImageHandler.setImage(kaart, Player.GetPlayer(PlayerType.Speler));
            }
            if (block == window.KaartenHuis)
            {
                block.Text = Player.GetPlayer(PlayerType.Huis).TranslateCard();
                ImageHandler.setImage(kaart, Player.GetPlayer(PlayerType.Huis));
            }
        }

        // Handles the hidden cards interaction
        public static void HandleHiddenCard(Cards kaart, TextBlock block)
        {
            MainWindow window = MainWindow.GetClass();
            if (block == window.KaartenSpeler)
                block.Text = Player.GetPlayer(PlayerType.Speler).TranslateCard();
            if (block == window.KaartenHuis)
            {
                if (!isRevealed)
                {
                    block.Text = block.Text + "????" + "\n";
                    isRevealed = true;
                    ImageHandler.setHiddenImage(Player.GetPlayer(PlayerType.Huis));
                }
                else
                {
                    int nr = kaart.GetNumber();
                    string naam = kaart.GetName();
                    block.Text = block.Text + nr + " " + naam + "\n";
                    ImageHandler.setImage(kaart, Player.GetPlayer(PlayerType.Huis));
                }
            }

        }

        // Schuffles the deck
        public static void Shuffle()
        {
            String[] KaartenNamenIrregulair = { "Schoppen", "Harten", "Klaveren", "Ruiten" };
            String[] KaartenNamenRegulair = { "Boer", "Dame", "Koning" };
            MainWindow.gameState = GameState.Stopped;
           

            for (int i = 2; i < 11; i++)
            {
                foreach (string s in KaartenNamenIrregulair)
                {
                    if (!Cards.CardList.Contains(new Cards(s, i, GetBitmapImage(s, i))))
                    {
                        new Cards(s, i, GetBitmapImage(s, i));
                    }
                }
            }


            foreach(string irr in KaartenNamenIrregulair)
            {
                foreach(string s in KaartenNamenRegulair)
                {
                  new Cards(irr + " " + s, 10, GetBitmapImage(irr, s));
                }
            }


            foreach(String s in KaartenNamenIrregulair)
            {
                new Cards(s, 0, GetBitmapImage(s, 0), true);
            }

        }

        // Resets the current round
        public static void ResetGame()
        {
            MainWindow mainWindow = MainWindow.GetClass();
            foreach (Player speler in Player.PlayerList)
            {
                speler.GetCards().Clear();
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
            mainWindow.DoubleKaart.Source = null;
            MainWindow.gameState = GameState.Stopped;
        }

        // Restarts the entire game
        public static void RestartGame()
        {
            MainWindow mainWindow = MainWindow.GetClass();
            ResetGame();
            Player.PlayerList.Clear();
            new Player(PlayerType.Speler, "Speler", 250);
            new Player(PlayerType.Huis, "Huis");

            Cards.CardList.Clear();
            Utils.Shuffle();
            mainWindow.AantalKaarten.Text = Cards.CardList.Count.ToString();
            mainWindow.Kapitaal.Text = Player.GetPlayer(PlayerType.Speler).GetMoney().ToString();
        }

        // Check if the money the player has is correct to an input
        public static bool ValidateMoney(int input, Player speler)
        {
            int spelerGeld = speler.GetMoney();
            int minBet = (spelerGeld / 100) * 10;
            if (input <= spelerGeld && input >= minBet)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Invalid Input!");
                return false;
            }
        }

        // Updates the money display TextBox in the XAML
        public static void UpdateMoneyDisplay()
        {
            MainWindow.GetClass().Kapitaal.Text = Player.GetPlayer(PlayerType.Speler).GetMoney().ToString();
        }

        // Method ran when the player loses
        public static void Lose()
        {
            int bet = Player.GetPlayer(PlayerType.Speler).GetBet();
            int geld = Player.GetPlayer(PlayerType.Speler).GetMoney();
            int newGeld = geld - bet;
            MessageBox.Show("You Lose!");
            Player.GetPlayer(PlayerType.Speler).SetMoney(newGeld);
            Player.GetPlayer(PlayerType.Speler).SetBet(0);
            UpdateMoneyDisplay();
            History = History +
                "\n Round " +
                Round +
                " - -" +
                bet +
                " " + Player.GetPlayer(PlayerType.Speler).TotalCardNumber() +
                " / " + Player.GetPlayer(PlayerType.Huis).TotalCardNumber();
            Utils.ResetGame();
            Round++;
            return;
        }

        // Method ran when the player wins
        public static void Win()
        {
            int bet = Player.GetPlayer(PlayerType.Speler).GetBet();
            int geld = Player.GetPlayer(PlayerType.Speler).GetMoney();
            MessageBox.Show("You Win!");
            Player.GetPlayer(PlayerType.Speler).SetMoney(geld + (bet * 2));
            Player.GetPlayer(PlayerType.Speler).SetBet(0);
            UpdateMoneyDisplay();
            History = History +
                "\n Round " +
                Round +
                " - +" +
                bet +
                " " + Player.GetPlayer(PlayerType.Speler).TotalCardNumber() +
                " / " + Player.GetPlayer(PlayerType.Huis).TotalCardNumber();
            Utils.ResetGame();
            Round++;
            return;
        }

        // Method ran when the player draws
        public static void Draw()
        {
            MessageBox.Show("Draw!"); 
            Player.GetPlayer(PlayerType.Speler).SetBet(0);
            UpdateMoneyDisplay();
            History = History +
                "\n Round " +
                Round +
                " - DRAW " 
                + Player.GetPlayer(PlayerType.Speler).TotalCardNumber() +
                " / " + Player.GetPlayer(PlayerType.Huis).TotalCardNumber();
            Utils.ResetGame();
            Round++;
            return;
        }

        // Get the bitmap image related to a card
        public static BitmapImage GetBitmapImage(String naam, int nummer)
        {
            if(nummer == 0)
                return new BitmapImage(new Uri("img/" + naam + "-aas" + ".png", UriKind.Relative));
            else
                return new BitmapImage(new Uri("img/" + naam + "-" + nummer + ".png", UriKind.Relative));

          
        }

        // Get the bitmap image related to a card (Aas only)
        public static BitmapImage GetBitmapImage(String vorm, String naam)
        {        
                return new BitmapImage(new Uri("img/" + vorm + "-" + naam + ".png", UriKind.Relative));
        }

        // Used to translate a BitmapImage to an Image
        public static Image TranslateBitMapImage(BitmapImage bitmapImage)
        {
            BitmapImage image = bitmapImage;
            Image img = new Image();
            img.Source = image;
            return img;
        }

        // Method for updating the current time displayed in the XAML
        public static void UpdateTimer()
        {
            dispatcher = new DispatcherTimer();
            dispatcher.Interval = new TimeSpan(0, 0, 1);
            dispatcher.Start();
            dispatcher.Tick += new EventHandler(DateTimeDispatcher);
        }

        // Dispatcher for the method above
        private static void DateTimeDispatcher(Object sender, EventArgs e)
        {
            String now = DateTime.Now.ToString("HH:mm:ss");
            MainWindow window = MainWindow.GetClass();
            window.Time.Text = now;
        }
    }
        
  


}

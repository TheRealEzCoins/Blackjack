using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Blackjack
{
    internal class KnoppenUtils
    {

        private static DispatcherTimer dispatcher;
        private static DispatcherTimer staDispatcher;
        // Check the Total of both player and dealer if one of them reached a criteria run one of the 3 events.
        public static void CheckEndState()
        {

            int SpelerTotaal = Player.GetPlayer(PlayerType.Speler).TotalCardNumber();
            int HuisTotaal = Player.GetPlayer(PlayerType.Huis).TotalCardNumber();
            if (SpelerTotaal > 21)
            {
                Utils.Lose();
                return;
            }

            if (HuisTotaal > 21)
            {
                Utils.Win();
                return;
            }
           
            if (SpelerTotaal > HuisTotaal)
            {
                Utils.Win();
                return;
            }

            if (SpelerTotaal == HuisTotaal)
            {
                Utils.Draw();
                return;
            }

            if (SpelerTotaal < HuisTotaal)
            {
                Utils.Lose();
                return;
            }

        }

        // Smaller version of the version above
        public static void CheckState()
        {
            int SpelerTotaal = Player.GetPlayer(PlayerType.Speler).TotalCardNumber();
            int HuisTotaal = Player.GetPlayer(PlayerType.Huis).TotalCardNumber();
            if (SpelerTotaal > 21)
            {
                Utils.Lose();
                return;
            }

            if (HuisTotaal > 21)
            {
                Utils.Win();
                return;
            }

        }

        // Draw 2 cards for each person and hide 1 card in the dealer's hand
        public static void DrawCards()
        {
            dispatcher = new DispatcherTimer();
            dispatcher.Interval = new TimeSpan(0, 0, 1);
            dispatcher.Start();

            MainWindow window = MainWindow.GetClass();
            if (MainWindow.gameState.Equals(GameState.Stopped))
            {
                MainWindow.gameState = GameState.Running;
                window.Hit.IsEnabled = true;
                window.Sta.IsEnabled = true;
                window.Deel.IsEnabled = false;
                window.Double.IsEnabled = true;
                
                int bet = int.Parse(MainWindow.GetClass().BetBlock.Text);
                Player.GetPlayer(PlayerType.Speler).SetBet(bet);
                window.Inzet.Text = bet.ToString();
 
                dispatcher.Tick += new EventHandler(DrawDispatcher);
                KnoppenUtils.CheckState();
            }
            else
            {
                MessageBox.Show("Je hebt al gedeeld!");
            }
        }

        // Dispatcher to make the cards appear slower
        private static void DrawDispatcher(object sender, EventArgs e)
        {
            if (Player.GetPlayer(PlayerType.Speler).GetCards().Count < 2)
            {
                if (Cards.CardList.Count == 0)
                {
                    Utils.Shuffle();
                    MessageBox.Show("Shuffling!");
                }
                MainWindow window = MainWindow.GetClass();
                Cards kaart = Utils.RandomCard();
                Player.GetPlayer(PlayerType.Speler).AddCard(kaart);
                Utils.HandleCards(kaart, window.KaartenSpeler);
                window.txtSpelerTotaal.Text = Player.GetPlayer(PlayerType.Speler).TotalCardNumber().ToString();
            } 
            else
            {
                dispatcher.Stop();
            }

            if (Player.GetPlayer(PlayerType.Huis).GetCards().Count < 2)
            {
                if (Cards.CardList.Count == 0)
                {
                    Utils.Shuffle();
                    MessageBox.Show("Shuffling!");
                }
                MainWindow window = MainWindow.GetClass();
                Cards kaart = Utils.RandomCard();
                Player.GetPlayer(PlayerType.Huis).AddCard(kaart);
                Utils.HandleHiddenCard(kaart, window.KaartenHuis);
                window.txtHuisTotaal.Text = Player.GetPlayer(PlayerType.Huis).TotalCardNumber().ToString();
            }
            else
            {
                dispatcher.Stop();
            }
        }


        // Stand logic
        public static void Stand() 
        {
            staDispatcher = new DispatcherTimer();
            staDispatcher.Interval = new TimeSpan(0, 0, 1);
            staDispatcher.Start();
            MainWindow window = MainWindow.GetClass();
            window.Double.IsEnabled = false;
            ImageHandler.revealImage();

            staDispatcher.Tick += new EventHandler(StandDispatcher);

        }


        // Dispatcher which will keep giving cards to the dealer untill they reach a < 16 score
        private static void StandDispatcher(Object sender, EventArgs e)
        {
            MainWindow window = MainWindow.GetClass();
            if(Player.GetPlayer(PlayerType.Huis).TotalCardNumber() < 16)
            {
                Cards kaart = Utils.RandomCard();
                Player.GetPlayer(PlayerType.Huis).AddCard(kaart);
                Utils.HandleCards(kaart, window.KaartenHuis);
                window.txtHuisTotaal.Text = Player.GetPlayer(PlayerType.Huis).TotalCardNumber().ToString();
            }
            else
            {
                if (window.DoubleKaart.Source != null)
                {
                    KnoppenUtils.DoubleDownImageReveal();
                }
                staDispatcher.Stop();
                KnoppenUtils.CheckEndState();
            }
        }

        // Reveals the double down card
        public static void DoubleDownImageReveal()
        {
            MainWindow window = MainWindow.GetClass();
            Cards extraKaart = Utils.RandomCard();
            Player.GetPlayer(PlayerType.Speler).AddCard(extraKaart);
            ImageHandler.RevealDoubleDownImage(extraKaart);
            window.txtSpelerTotaal.Text = Player.GetPlayer(PlayerType.Speler).TotalCardNumber().ToString();
        }
    }
}

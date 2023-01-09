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
        public static void Staan()
        {

            int SpelerTotaal = Speler.GetSpeler(PlayerType.Speler).TotaalAantal();
            int HuisTotaal = Speler.GetSpeler(PlayerType.Huis).TotaalAantal();
            if (SpelerTotaal > 21)
            {
                Utils.lose();
                return;
            }

            if (HuisTotaal > 21)
            {
                Utils.win();
                return;
            }
           
            if (SpelerTotaal > HuisTotaal)
            {
                Utils.win();
                return;
            }

            if (SpelerTotaal == HuisTotaal)
            {
                Utils.draw();
                return;
            }

            if (SpelerTotaal < HuisTotaal)
            {
                Utils.lose();
                return;
            }
        }

        public static void checkState()
        {
            int SpelerTotaal = Speler.GetSpeler(PlayerType.Speler).TotaalAantal();
            int HuisTotaal = Speler.GetSpeler(PlayerType.Huis).TotaalAantal();
            if (SpelerTotaal > 21)
            {
                Utils.lose();
                return;
            }

            if (HuisTotaal > 21)
            {
                Utils.win();
                return;
            }

        }

        public static void Deel()
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
                Speler.GetSpeler(PlayerType.Speler).SetBet(bet);
                window.Inzet.Text = bet.ToString();
 
                dispatcher.Tick += new EventHandler(DeelDispatcher);
                KnoppenUtils.checkState();
            }
            else
            {
                MessageBox.Show("Je hebt al gedeeld!");
            }
        }

        private static void DeelDispatcher(object sender, EventArgs e)
        {
            if (Speler.GetSpeler(PlayerType.Speler).getKaarten().Count < 2)
            {
                if (Kaarten.KaartenLijst.Count == 0)
                {
                    Utils.Shuffle();
                }
                MainWindow window = MainWindow.GetClass();
                Kaarten kaart = Utils.randomKaart();
                Speler.GetSpeler(PlayerType.Speler).VoegKaartToe(kaart);
                Utils.handleCards(kaart, window.KaartenSpeler);
                window.txtSpelerTotaal.Text = Speler.GetSpeler(PlayerType.Speler).TotaalAantal().ToString();
            } 
            else
            {
                dispatcher.Stop();
            }

            if (Speler.GetSpeler(PlayerType.Huis).getKaarten().Count < 2)
            {
                if (Kaarten.KaartenLijst.Count == 0)
                {
                    Utils.Shuffle();
                }
                MainWindow window = MainWindow.GetClass();
                Kaarten kaart = Utils.randomKaart();
                Speler.GetSpeler(PlayerType.Huis).VoegKaartToe(kaart);
                Utils.handleHiddenCard(kaart, window.KaartenHuis);
                window.txtHuisTotaal.Text = Speler.GetSpeler(PlayerType.Huis).TotaalAantal().ToString();
            }
            else
            {
                dispatcher.Stop();
            }
        }


        public static void Sta() 
        {
            staDispatcher = new DispatcherTimer();
            staDispatcher.Interval = new TimeSpan(0, 0, 1);
            staDispatcher.Start();
            MainWindow window = MainWindow.GetClass();
            window.Double.IsEnabled = false;
            ImageHandler.revealImage();

            staDispatcher.Tick += new EventHandler(StaDispatcher);

        }



        private static void StaDispatcher(Object sender, EventArgs e)
        {
            MainWindow window = MainWindow.GetClass();
            if(Speler.GetSpeler(PlayerType.Huis).TotaalAantal() < 16)
            {
                Kaarten kaart = Utils.randomKaart();
                Speler.GetSpeler(PlayerType.Huis).VoegKaartToe(kaart);
                Utils.handleCards(kaart, window.KaartenHuis);
                window.txtHuisTotaal.Text = Speler.GetSpeler(PlayerType.Huis).TotaalAantal().ToString();
            }
            else
            {
                if (window.DoubleKaart.Source != null)
                {
                    KnoppenUtils.DoubleDownImageReveal();
                }
                staDispatcher.Stop();
                KnoppenUtils.Staan();
            }
        }

        public static void DoubleDownImageReveal()
        {
            MainWindow window = MainWindow.GetClass();
            Kaarten extraKaart = Utils.randomKaart();
            Speler.GetSpeler(PlayerType.Speler).VoegKaartToe(extraKaart);
            ImageHandler.RevealDoubleDownImage(extraKaart);
            window.txtSpelerTotaal.Text = Speler.GetSpeler(PlayerType.Speler).TotaalAantal().ToString();
        }
    }
}

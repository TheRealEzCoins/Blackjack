using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Blackjack
{
    internal class KnoppenUtils
    {
       

        public static void Staan()
        {

            int SpelerTotaal = Speler.GetSpeler(PlayerType.Speler).TotaalAantal();
            int HuisTotaal = Speler.GetSpeler(PlayerType.Huis).TotaalAantal();
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

                // Speler trekt kaarten
                for (int i = 0; i < 2; i++)
                {
                    if(Kaarten.KaartenLijst.Count == 0)
                    {
                        Utils.Shuffle();
                    }
                    Kaarten kaart = Utils.randomKaart();
                    Speler.GetSpeler(PlayerType.Speler).VoegKaartToe(kaart);
                    Utils.handleCards(kaart, window.KaartenSpeler);
                }

                window.txtSpelerTotaal.Text = Speler.GetSpeler(PlayerType.Speler).TotaalAantal().ToString();




                // Huis trekt kaarten
                for (int i = 0; i < 2; i++)
                {
                    if (Kaarten.KaartenLijst.Count == 0)
                    {
                        Utils.Shuffle();
                    }
                    Kaarten kaart = Utils.randomKaart();
                    Speler.GetSpeler(PlayerType.Huis).VoegKaartToe(kaart);
                    Utils.handleHiddenCard(kaart, window.KaartenHuis);
                }


                window.txtHuisTotaal.Text = Speler.GetSpeler(PlayerType.Huis).TotaalAantal().ToString();


                KnoppenUtils.checkState();
            }
            else
            {
                MessageBox.Show("Je hebt al gedeeld!");
            }
        }
    }
}

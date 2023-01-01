using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
=======
using System.Windows.Controls;
using System.Windows.Media.Imaging;
<<<<<<<<< Temporary merge branch 1
=========
>>>>>>> 2eee5f2ce0f532acaf3200c32671bf80028f0337
>>>>>>>>> Temporary merge branch 2
=======
using System.Windows.Controls;
using System.Windows.Media.Imaging;
<<<<<<<<< Temporary merge branch 1
=========
>>>>>>> 2eee5f2ce0f532acaf3200c32671bf80028f0337
>>>>>>>>> Temporary merge branch 2
=======
using System.Windows.Controls;
using System.Windows.Media.Imaging;
<<<<<<<<< Temporary merge branch 1
=========
>>>>>>> 2eee5f2ce0f532acaf3200c32671bf80028f0337
>>>>>>>>> Temporary merge branch 2
=======
using System.Windows.Controls;
using System.Windows.Media.Imaging;
<<<<<<<<< Temporary merge branch 1
=========
>>>>>>> 2eee5f2ce0f532acaf3200c32671bf80028f0337
>>>>>>>>> Temporary merge branch 2

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

<<<<<<<<< Temporary merge branch 1
=========
<<<<<<< HEAD
            int SetBet = (int) Math.Round(Bet.Value, 0);
            if (Utils.ValidateMoney(SetBet ,Speler.GetSpeler(PlayerType.Speler)))
            {
                KnoppenUtils.Deel();
=======
>>>>>>>>> Temporary merge branch 2
            if (gameState.Equals(GameState.Stopped))
            { 
                gameState = GameState.Running;

                Hit.IsEnabled = true;
                Sta.IsEnabled = true;
                Deel.IsEnabled = false;

                // Speler trekt kaarten
                for(int i = 0; i < 2; i++)
                {
                    Kaarten kaart = Utils.randomKaart();
                    SpeelerHand.Add(kaart);
                    int Toevoeging = kaart.getNummer();
                    SpelerTotaal += Toevoeging;
                    Utils.handleCards(kaart, KaartSpeler, KaartenSpeler);
                }

                txtSpelerTotaal.Text = SpelerTotaal.ToString();




                // Huis trekt kaarten
                for (int i = 0; i < 2; i++)
                {
                        Kaarten kaart = Utils.randomKaart();
                        DealerHand.Add(kaart);
                        HuisTotaal += kaart.getNummer();
                        Utils.handleCards(kaart, KaartHuis, KaartenHuis);
                }
               

                txtHuisTotaal.Text = HuisTotaal.ToString();


                KnoppenUtils.Reset();
<<<<<<<<< Temporary merge branch 1
=========
>>>>>>> 2eee5f2ce0f532acaf3200c32671bf80028f0337
>>>>>>>>> Temporary merge branch 2
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


       

       
<<<<<<<<< Temporary merge branch 1
=========
>>>>>>> 2eee5f2ce0f532acaf3200c32671bf80028f0337
>>>>>>>>> Temporary merge branch 2
    }


}
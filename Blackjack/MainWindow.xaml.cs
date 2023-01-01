using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
<<<<<<< HEAD
=======
using System.Windows.Controls;
using System.Windows.Media.Imaging;
>>>>>>> 2eee5f2ce0f532acaf3200c32671bf80028f0337

namespace Blackjack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

<<<<<<< HEAD
        protected static Random rnd = new Random();
        private static MainWindow window = null;
        public static String actieveSpeler;
=======
        private static List<Kaarten> SpeelerHand = new List<Kaarten>();
        private static List<Kaarten> DealerHand = new List<Kaarten>();
        public static int SpelerTotaal = 0;
        public static int HuisTotaal = 0;
        protected static String KaartSpeler = "";
        protected static String KaartHuis = "";
        protected static Random rnd = new Random();
        private static MainWindow window = null;

       
       
        public static GameState gameState;



>>>>>>> 2eee5f2ce0f532acaf3200c32671bf80028f0337

        public static GameState gameState;

        public MainWindow()
        {
            InitializeComponent();
            window = this;
<<<<<<< HEAD

            new Speler(PlayerType.Speler, "Speler", 250);
            new Speler(PlayerType.Huis, "Huis");

            Utils.Shuffle();
            window.Inzet.Text = "0";
            Kapitaal.Text = Speler.GetSpeler(PlayerType.Speler).GetGeld().ToString();

=======
            Utils.Shuffle();
        
>>>>>>> 2eee5f2ce0f532acaf3200c32671bf80028f0337
        }

        private void Deel_Click(object sender, RoutedEventArgs e)
        {

<<<<<<< HEAD
            int SetBet = (int) Math.Round(Bet.Value, 0);
            if (Utils.ValidateMoney(SetBet ,Speler.GetSpeler(PlayerType.Speler)))
            {
                KnoppenUtils.Deel();
=======
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
>>>>>>> 2eee5f2ce0f532acaf3200c32671bf80028f0337
            } 
        }

        private void Hit_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
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
=======
            if (gameState.Equals(GameState.Running)) {
            Kaarten Kaart = Utils.randomKaart();
            Utils.handleCards(Kaart, KaartSpeler, KaartenSpeler);
                    
            SpelerTotaal += Kaart.getNummer();
            txtSpelerTotaal.Text = SpelerTotaal.ToString();

            KnoppenUtils.Reset();
            } else
>>>>>>> 2eee5f2ce0f532acaf3200c32671bf80028f0337
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
<<<<<<< HEAD
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

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BetBlock.Text = Math.Round(Bet.Value, 0).ToString();
            
        }

        private void Double_Click(object sender, RoutedEventArgs e)
        {
            int currBet = Speler.GetSpeler(PlayerType.Speler).GetBet();
            if(Utils.ValidateMoney(currBet * 2, Speler.GetSpeler(PlayerType.Speler)))
            {
                Double.IsEnabled = false;
                Speler.GetSpeler(PlayerType.Speler).SetBet(currBet * 2);
                MessageBox.Show("Doubled!");
                window.Inzet.Text = Speler.GetSpeler(PlayerType.Speler).GetBet().ToString();
            }
        }
=======

                HuisTotaal += kaart.getNummer();
                Utils.handleCards(kaart, KaartHuis, KaartenHuis);
                txtHuisTotaal.Text = HuisTotaal.ToString();
            }
            KnoppenUtils.Reset();
            KnoppenUtils.Staan();
            
        }

      



      



        public static MainWindow GetClass()
        {
            return window;
        }

        public static List<Kaarten> GetSpelerHand()
        {
            return SpeelerHand;
        }


       

       
>>>>>>> 2eee5f2ce0f532acaf3200c32671bf80028f0337
    }


}

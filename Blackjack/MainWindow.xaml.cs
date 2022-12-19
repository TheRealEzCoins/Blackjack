using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Blackjack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static List<Kaarten> SpeelerHand = new List<Kaarten>();
        private static List<Kaarten> DealerHand = new List<Kaarten>();
        public static int SpelerTotaal = 0;
        public static int HuisTotaal = 0;
        protected static String KaartSpeler = "";
        protected static String KaartHuis = "";
        protected static Random rnd = new Random();
        private static MainWindow window = null;

       
       
        public static GameState gameState;





        public MainWindow()
        {
            InitializeComponent();
            window = this;
            Utils.Shuffle();
        
        }

        private void Deel_Click(object sender, RoutedEventArgs e)
        {

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
            } 
            else 
            {
                MessageBox.Show("Je hebt al gedeeld!");
            }



        }

        private void Hit_Click(object sender, RoutedEventArgs e)
        {
            if (gameState.Equals(GameState.Running)) {
            Kaarten Kaart = Utils.randomKaart();
            Utils.handleCards(Kaart, KaartSpeler, KaartenSpeler);
                    
            SpelerTotaal += Kaart.getNummer();
            txtSpelerTotaal.Text = SpelerTotaal.ToString();

            KnoppenUtils.Reset();
            } else
            {
                MessageBox.Show("Je moet eerst delen!");
            }
        }



        private void Sta_Click(object sender, RoutedEventArgs e)
        {
           
            while (HuisTotaal < 16)
            {
                Kaarten kaart = Utils.randomKaart();

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


       

       
    }


}

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

        protected static int SpelerTotaal = 0;
        protected static int HuisTotaal = 0;
        protected static String KaartSpeler = "";
        protected static String KaartHuis = "";
        protected static Random rnd = new Random();
        protected static bool deel = false;
        protected static MainWindow window = null;
        protected static String[] KaartenNamenIrregulair = { "Schoppen", "Harten", "Klaveren", "Ruiten" };
        protected static String[] KaartenNamenRegulair = { "boer", "vrouw", "heer" };
        private static List<Kaarten> KaartenLijst = new List<Kaarten>();





        public MainWindow()
        {
            InitializeComponent();
            window = this;

            fillList();
        
        }

        private void Deel_Click(object sender, RoutedEventArgs e)
        {
           
            if (!deel)
            {
                deel= true;

                Hit.IsEnabled = true;
                Sta.IsEnabled = true;
                Deel.IsEnabled = false;
                // Speler vraagt nieuwe kaarten aan.
                Random rnd = new Random();
                int kaart1 = rnd.Next(KaartenLijst.Count);
                int kaart2 = rnd.Next(KaartenLijst.Count);

                int toevoeging1 = KaartenLijst[kaart1].getNummer();
                int toevoeging2 = KaartenLijst[kaart2].getNummer();
                SpelerTotaal += toevoeging1;
                SpelerTotaal += toevoeging2;

                Utils.handleCards(kaart1, KaartSpeler, KaartenLijst, KaartenSpeler);
                Utils.handleCards(kaart2, KaartSpeler, KaartenLijst, KaartenSpeler);

                txtSpelerTotaal.Text = SpelerTotaal.ToString();



                // Huis trekt nieuwe 
                int huiskaart1 = rnd.Next(KaartenLijst.Count);
                int toevoeginghuis1 = KaartenLijst[huiskaart1].getNummer();
                HuisTotaal += toevoeginghuis1;

                Utils.handleCards(huiskaart1, KaartHuis, KaartenLijst, KaartenHuis);

                txtHuisTotaal.Text = HuisTotaal.ToString();


                Reset(window);
            } 
            else 
            {
                MessageBox.Show("Je hebt al gedeeld!");
            }



        }

        private void Hit_Click(object sender, RoutedEventArgs e)
        {
            if (deel) { 
            int Kaart1 = rnd.Next(KaartenLijst.Count);
            Utils.handleCards(Kaart1, KaartSpeler, KaartenLijst, KaartenSpeler);
                int toevoeging = KaartenLijst[Kaart1].getNummer();
                    
            SpelerTotaal += toevoeging;
            txtSpelerTotaal.Text = SpelerTotaal.ToString();

            Reset(window);
            } else
            {
                MessageBox.Show("Je moet eerst delen!");
            }
        }



        private void Sta_Click(object sender, RoutedEventArgs e)
        {
           
            while (HuisTotaal < 16)
            {
                int kaart = rnd.Next(KaartenLijst.Count);
                int toevoeging = KaartenLijst[kaart].getNummer();

                HuisTotaal += toevoeging;
                Utils.handleCards(kaart, KaartHuis, KaartenLijst, KaartenHuis);
                txtHuisTotaal.Text = HuisTotaal.ToString();
            }
            Reset(window);
                Staan(window);
            
        }

        public static void Default(MainWindow mainWindow)
        {
            mainWindow.Hit.IsEnabled = false;
            mainWindow.Sta.IsEnabled = false;
            mainWindow.Deel.IsEnabled = true;
            mainWindow.KaartenHuis.Text = "";
            mainWindow.KaartenSpeler.Text = "";
            mainWindow.txtHuisTotaal.Text = "";
            mainWindow.txtSpelerTotaal.Text = "";
            SpelerTotaal = 0;
            HuisTotaal = 0;
            deel = false;
            fillList();
        }



        public static void Reset(MainWindow mainWindow)
        {
            if (SpelerTotaal > 21)
            {
                MessageBox.Show("You lose!");
                Default(mainWindow);
                return;
            }

            if (HuisTotaal > 21)
            {
                MessageBox.Show("You Win!");
                Default(mainWindow);
                return;
            }
        }


        public static void Staan(MainWindow mainWindow)
        {
            if (SpelerTotaal > HuisTotaal)
            {
                MessageBox.Show("Gewonnen!");
                Default(mainWindow);
                return;
            }

            if (SpelerTotaal == HuisTotaal)
            {
                MessageBox.Show("Push!");
                Default(mainWindow);
                return;
            }

            if (SpelerTotaal < HuisTotaal)
            {
                MessageBox.Show("Verloren!");
                Default(mainWindow);
                return;
            }
        }

        public static void fillList()
        {
            for (int i = 2; i < 11; i++)
            {
                foreach (string s in KaartenNamenIrregulair)
                {
                    if (!KaartenLijst.Contains(new Kaarten(s, i)))
                    {
                        KaartenLijst.Add(new Kaarten(s, i));
                    }
                }
            }

            foreach (string s in KaartenNamenRegulair)
            {
                if (!KaartenLijst.Contains(new Kaarten(s, 10)))
                {
                    KaartenLijst.Add(new Kaarten(s, 10));
                }
            }

            for(int i = 1; i < 5; i++)
            {
                KaartenLijst.Add(new Kaarten("aas", 1, Utils.GetImage("C:\\Users\\steve\\source\\repos\\Blackjack\\Blackjack\\Assets\\aas.png")));
            }
        }
    }


}

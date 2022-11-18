using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        



        public MainWindow()
        {
            InitializeComponent();
            window = this;
           
        
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
                int kaart1 = rnd.Next(1, 10);
                int kaart2 = rnd.Next(1, 10);

                SpelerTotaal += kaart1;
                SpelerTotaal += kaart2;

                Utils.handleCards(KaartSpeler, kaart1, KaartenSpeler);
                Utils.handleCards(KaartSpeler, kaart2, KaartenSpeler);

                txtSpelerTotaal.Text = SpelerTotaal.ToString();



                // Huis trekt nieuwe 
                int IHuis = rnd.Next(1, 10);
                HuisTotaal += IHuis;

                Utils.handleCards(KaartHuis, IHuis, KaartenHuis);

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
            int ISpeler = rnd.Next(1, 10);
            Utils.handleCards(KaartSpeler, ISpeler, KaartenSpeler);

            SpelerTotaal += ISpeler;
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
                int kaart = rnd.Next(1, 10);
                HuisTotaal += kaart;
                Utils.handleCards(KaartHuis, kaart, KaartenHuis);
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
                MessageBox.Show("You Win!");
                Default(mainWindow);
                return;
            }

            if (SpelerTotaal == HuisTotaal)
            {
                MessageBox.Show("Draw!");
                Default(mainWindow);
                return;
            }

            if (SpelerTotaal < HuisTotaal)
            {
                MessageBox.Show("You lose!");
                Default(mainWindow);
                return;
            }
        }
    }

}

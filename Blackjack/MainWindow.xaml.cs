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

        



        public MainWindow()
        {
            InitializeComponent();
           
        
        }

        private void Deel_Click(object sender, RoutedEventArgs e)
        {

            // Speler vraagt nieuwe kaarten aan.

            Random rnd = new Random();
            int kaart1 = rnd.Next(1, 21);
            int kaart2 = rnd.Next(1, 21);

            SpelerTotaal += kaart1;
            SpelerTotaal += kaart2;

            Utils.handleCards(KaartSpeler, kaart1, KaartenSpeler);
            Utils.handleCards(KaartSpeler, kaart2, KaartenSpeler);

            txtSpelerTotaal.Text = SpelerTotaal.ToString();

            if (SpelerTotaal > 21)
            {
                MessageBox.Show("You lost!");
                SpelerTotaal = 0;
                HuisTotaal = 0;
                KaartenSpeler.Text = "";
                KaartenHuis.Text = "";
                txtSpelerTotaal.Text = "";
                return;
            }


            // Huis trekt nieuwe 
            int IHuis = rnd.Next(1, 21);
            HuisTotaal += IHuis;

            Utils.handleCards(KaartHuis, IHuis, KaartenHuis);


            if (HuisTotaal > 21)
            {
                MessageBox.Show("You Win!");
                SpelerTotaal = 0;
                HuisTotaal = 0;
                KaartenHuis.Text = "";
                KaartenSpeler.Text = "";
                return;
            }



        }

        private void Hit_Click(object sender, RoutedEventArgs e)
        {
            int ISpeler = rnd.Next(1, 21);
            Utils.handleCards(KaartSpeler, ISpeler, KaartenSpeler);

            SpelerTotaal += ISpeler;
            txtSpelerTotaal.Text = SpelerTotaal.ToString();

            if (SpelerTotaal > 21)
            {
                MessageBox.Show("You lose!");
                SpelerTotaal = 0;
                HuisTotaal = 0;
                KaartenHuis.Text = "";
                KaartenSpeler.Text = "";
                txtSpelerTotaal.Text = "";
                return;
            }
        }
    }
}

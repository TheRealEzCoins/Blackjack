using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Blackjack
{
    internal class Utils
    {
        private static Random rnd = new Random();
        private static List<Kaarten> KaartenLijst = new List<Kaarten>();
       
        public static bool GetRandomBool()
        {

            int cf = rnd.Next(1, 2);

            if (cf == 1)
                return true;
            else
                return false;
        }

        public static Kaarten randomKaart()
        {
            int kaart = rnd.Next(KaartenLijst.Count);
            return KaartenLijst[kaart];
        }


        public static void handleCards(Kaarten kaart, String s, TextBlock block)
        {


            int i = kaart.getNummer();
            String addedString = kaart.getNaam();
            MainWindow mainWindow = MainWindow.getWindow();
            if (block == mainWindow.KaartenHuis)
            {
                    
            }

            switch (i)
            {
                case 1:
                    s = block.Text + "\n" + i + " " + addedString;
                    break;

                case 10:
                    if (Utils.GetRandomBool())
                        s = block.Text + "\n" + i + " " + addedString;
                    break;

                default:
                    s = block.Text + "\n" + i + " " + addedString;
                    break;
            }
            block.Text = s;
            KaartenLijst.Remove(kaart);
        }

        public static string readList(List<Kaarten> list)
        {
            string s = "";

            foreach (Kaarten kaart in list)
            {
                string KaartNaam = kaart.getNaam();
                string KaartNr = kaart.getNummer().ToString();
                s = s + "\n" + KaartNaam;
            }

            return s;
        }

        public static void Shuffle()
        {
            String[] KaartenNamenIrregulair = { "Schoppen", "Harten", "Klaveren", "Ruiten" };
            String[] KaartenNamenRegulair = { "boer", "vrouw", "heer" };
            MainWindow.gameState = GameState.Stopped;

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

            for (int i = 1; i < 5; i++)
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri("https://media.istockphoto.com/id/997750252/vector/pixel-art-playing-cards-standart-deck-vector-set.jpg?s=1024x1024&w=is&k=20&c=eA6OWXtul6HT8n-MU9zbuuYPsaIVcLsg2VsMRSaWjt8=");
                bitmapImage.EndInit();
                KaartenLijst.Add(new Kaarten("aas", 1, bitmapImage));
            }
        }

        public static void restartGame()
        {
            MainWindow mainWindow = MainWindow.getWindow();
            mainWindow.Hit.IsEnabled = false;
            mainWindow.Sta.IsEnabled = false;
            mainWindow.Deel.IsEnabled = true;
            mainWindow.KaartenHuis.Text = "";
            mainWindow.KaartenSpeler.Text = "";
            mainWindow.txtHuisTotaal.Text = "";
            mainWindow.txtSpelerTotaal.Text = "";
            MainWindow.SpelerTotaal = 0;
            MainWindow.HuisTotaal = 0;
            Shuffle();
        }
    }

   
}

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
        public static bool GetRandomBool()
        {
       
            int cf = rnd.Next(1, 2);

            if (cf == 1)
                return true;
            else
                return false;
        } 


        public static void handleCards(int i, String s, List<Kaarten> kaarten, TextBlock block) 
        {

           
            Kaarten kaart = kaarten[i];
            int addedInt = kaart.getNummer();
            String addedString = kaart.getNaam();
         

            switch(i)
            {
                case 1:
                    if (Utils.GetRandomBool())
                        s = block.Text + "\n" + addedInt + " " + addedString;               
                    break;

                case 10:
                    if (Utils.GetRandomBool())
                        s = block.Text + "\n" + addedInt + " " + addedString;
                    break;

                default:
                    s = block.Text + "\n" + addedInt + " " + addedString;
                    break;
            }
            block.Text = s;
            kaarten.Remove(kaart);
        }


        public static Image GetImage(String source)
        {
            Image myImage = new Image();
            BitmapImage bitmap = new BitmapImage();

            bitmap.BeginInit();
            bitmap.UriSource = new Uri(source);

            bitmap.EndInit();
            myImage.Source = bitmap;
            return myImage;
        }

        public static void MoveTo(Image target, double newX, double newY)
        {
            var top = Canvas.GetTop(target);
            var left = Canvas.GetLeft(target);
            TranslateTransform trans = new TranslateTransform();
            target.RenderTransform = trans;
            DoubleAnimation anim1 = new DoubleAnimation(top, newY - top, TimeSpan.FromSeconds(10));
            DoubleAnimation anim2 = new DoubleAnimation(left, newX - left, TimeSpan.FromSeconds(10));
            trans.BeginAnimation(TranslateTransform.XProperty, anim1);
            trans.BeginAnimation(TranslateTransform.YProperty, anim2);
        }

    }


}

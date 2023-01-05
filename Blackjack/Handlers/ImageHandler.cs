using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Blackjack
{
    internal class ImageHandler
    {

        public static void setImage(BitmapImage image, Speler speler)
        {
           speler.GetImage().Source = image;
        }
    }

}

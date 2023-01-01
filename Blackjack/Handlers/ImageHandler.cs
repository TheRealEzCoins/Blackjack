using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Blackjack
{
    internal class ImageHandler
    {
        public static void sourceImage()
        {
            
        }


        public static Image createImage(Kaarten kaart)
        {
            BitmapImage image = kaart.getBitmap();
            Image img = new Image();
            img.Source = image;
            return img;
        }
    }
}

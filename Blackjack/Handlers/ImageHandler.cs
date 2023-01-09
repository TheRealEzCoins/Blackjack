using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Blackjack
{
    internal class ImageHandler
    {

        public static Thickness hiddenImage;
        public static Boolean IsHidden = true;
       
        public static void setImage(Kaarten kaart, Speler speler)
        {
           MainWindow window = MainWindow.GetClass();
           if(speler.GetPlayerType() == PlayerType.Speler)
            {
                Canvas canvas = window.SpelerCanvas;

                Image image = kaart.GetImage();
                image.Width = 30;
                image.Height = canvas.Height;
                image.Margin = new Thickness(30 * speler.getKaarten().Count ,0 ,0 ,0);


                canvas.Children.Add(image);
           }

           if(speler.GetPlayerType() == PlayerType.Huis)
            {
                Canvas canvas = window.HuisCanvas;
                Image image = kaart.GetImage();
                image.Width = 30;
                image.Height = canvas.Height;
                image.Margin = new Thickness(30 * speler.getKaarten().Count, 0, 0, 0);


                canvas.Children.Add(image);
           }
        }

        public static void setHiddenImage(Speler speler)
        {
            MainWindow window = MainWindow.GetClass();
            if (speler.GetPlayerType() == PlayerType.Speler)
            {
                Canvas canvas = window.SpelerCanvas;

                Image image = Utils.TranslateBitMapImage(new BitmapImage(new Uri(@"/img/Achterkant.png", UriKind.Relative)));
                image.Width = 30;
                image.Height = canvas.Height;
                image.Margin = new Thickness(30 * speler.getKaarten().Count, 0, 0, 0);


                canvas.Children.Add(image);
            }

            if (speler.GetPlayerType() == PlayerType.Huis)
            {
                Canvas canvas = window.HuisCanvas;
                Image image = Utils.TranslateBitMapImage(new BitmapImage(new Uri(@"/img/Achterkant.png", UriKind.Relative)));
                image.Width = 30;
                image.Height = canvas.Height;
                image.Margin = new Thickness(30 * speler.getKaarten().Count, 0, 0, 0);

                hiddenImage = new Thickness(30 * speler.getKaarten().Count, 0, 0, 0);

                canvas.Children.Add(image);
            }
        }

        public static void revealImage()
        {
            if (IsHidden)
            {
                MainWindow window = MainWindow.GetClass();
                Canvas canvas = window.HuisCanvas;
                canvas.Children.RemoveAt(0);
                Kaarten kaart = Speler.GetSpeler(PlayerType.Huis).getKaarten()[0];

                Image image = kaart.GetImage();
                image.Width = 30;
                image.Height = canvas.Height;
                image.Margin = hiddenImage;

                canvas.Children.Add(image);
                IsHidden = false;
            }

        }


        public static void AssignDoubleDownImage()
        {
            MainWindow window = MainWindow.GetClass();
            Image image = Utils.TranslateBitMapImage(new BitmapImage(new Uri(@"/img/Achterkant.png", UriKind.Relative)));
            RotateTransform rotateTransform = new RotateTransform(90);
            window.DoubleKaart.RenderTransform = rotateTransform;

            window.DoubleKaart.Source = image.Source;
        }

        public static void RevealDoubleDownImage(Kaarten kaarten)
        {
            MainWindow window = MainWindow.GetClass();
            Image image = kaarten.GetImage();
            RotateTransform rotateTransform = new RotateTransform(90);
            window.DoubleKaart.RenderTransform = rotateTransform;

            window.DoubleKaart.Source = image.Source;

            
        }
    }

}

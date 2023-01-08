using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Blackjack
{

    public class Kaarten
    {
        public static List<Kaarten> KaartenLijst = new List<Kaarten>();
        private string naam;
        private int nummer;
        private BitmapImage bitmap;
        private bool IsAas;

        public Kaarten(String naam, int nummer, BitmapImage bitmap)
        {
            this.naam = naam;
            this.bitmap = bitmap;
            this.nummer = nummer;
            IsAas = false;
            KaartenLijst.Add(this);
        }

        public Kaarten(String naam, int nummer, BitmapImage bitmap, bool isAas)
        {
            this.naam = naam;
            this.bitmap = bitmap;
            this.nummer = nummer;
            IsAas = isAas;
            KaartenLijst.Add(this);
        }


        public string getNaam()
        {
            return naam;
        }

        public int getNummer()
        {
            return nummer;
        }

        public void setNummer(int Nummer)
        {
            nummer = Nummer;
        }


        public BitmapImage getBitmap() 
        {
            return bitmap;
        }


        public Image GetImage()
        {
            BitmapImage image = this.getBitmap();
            Image img = new Image();
            img.Source = image;
            return img;
        }

    }
}
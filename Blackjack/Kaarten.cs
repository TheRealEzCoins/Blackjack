using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Blackjack
{

    class Kaarten
    {
        private string naam;
        private int nummer;
        private BitmapImage bitmap;

        public Kaarten(String naam, int nummer, BitmapImage bitmap) {
            this.naam = naam;
            this.bitmap = bitmap;
            this.nummer = nummer;
        }

        public Kaarten(String naam, int nummer)
        {
            this.naam = naam;
            this.bitmap = null;
            this.nummer = nummer;

        }

        public string getNaam()
        {
            return naam;
        }

        public int getNummer() 
        {
            return nummer;
        }

       
        public BitmapImage getBitmap() 
        {
            return bitmap;
        }
      
    }
}

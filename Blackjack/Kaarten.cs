using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Blackjack
{

    class Kaarten
    {
        private string naam;
        private int nummer;
        private Image image;
        public Kaarten(String naam, int nummer, Image image) {
            this.naam = naam;
            this.image = image;
            this.nummer = nummer;

        }

        public Kaarten(String naam, int nummer)
        {
            this.naam = naam;
            this.image = null;
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

       
        public Image getImage() 
        {
            return image;
        }
      
    }
}

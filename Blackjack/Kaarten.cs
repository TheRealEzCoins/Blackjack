using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{

    class Kaarten
    {
        private string naam;
        private int nummer;
        public Kaarten(String naam, int nummer) {
            this.naam = naam;
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
      
    }
}

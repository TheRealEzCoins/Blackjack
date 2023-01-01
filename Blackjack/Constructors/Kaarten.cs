using System;
using System.Collections.Generic;
<<<<<<< HEAD:Blackjack/Constructors/Kaarten.cs
=======
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
>>>>>>> 2eee5f2ce0f532acaf3200c32671bf80028f0337:Blackjack/Kaarten.cs
using System.Windows.Media.Imaging;

namespace Blackjack
{

    public class Kaarten
    {
        public static List<Kaarten> KaartenLijst = new List<Kaarten>();
        private string naam;
        private int nummer;
        private BitmapImage bitmap;

<<<<<<< HEAD:Blackjack/Constructors/Kaarten.cs
        public Kaarten(String naam, int nummer, BitmapImage bitmap)
        {
            this.naam = naam;
            this.bitmap = bitmap;
            this.nummer = nummer;
            KaartenLijst.Add(this);
=======
        public Kaarten(String naam, int nummer, BitmapImage bitmap) {
            this.naam = naam;
            this.bitmap = bitmap;
            this.nummer = nummer;
>>>>>>> 2eee5f2ce0f532acaf3200c32671bf80028f0337:Blackjack/Kaarten.cs
        }

        public Kaarten(String naam, int nummer)
        {
            this.naam = naam;
<<<<<<< HEAD:Blackjack/Constructors/Kaarten.cs
            bitmap = null;
=======
            this.bitmap = null;
>>>>>>> 2eee5f2ce0f532acaf3200c32671bf80028f0337:Blackjack/Kaarten.cs
            this.nummer = nummer;
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

<<<<<<< HEAD:Blackjack/Constructors/Kaarten.cs

        public BitmapImage getBitmap()
=======
       
        public BitmapImage getBitmap() 
>>>>>>> 2eee5f2ce0f532acaf3200c32671bf80028f0337:Blackjack/Kaarten.cs
        {
            return bitmap;
        }

    }
}

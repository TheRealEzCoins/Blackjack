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

    public class Cards
    {
        public static readonly List<Cards> CardList = new List<Cards>();
        private readonly string _name;
        private int _number;
        private readonly BitmapImage _bitmap;
        private readonly bool _isAce;

        public Cards(String name, int number, BitmapImage bitmap)
        {
            this._name = name;
            this._bitmap = bitmap;
            this._number = number;
            _isAce = false;
            CardList.Add(this);
        }

        public Cards(String name, int number, BitmapImage bitmap, bool isAce)
        {
            this._name = name;
            this._bitmap = bitmap;
            this._number = number;
            _isAce = isAce;
            CardList.Add(this);
        }


        public string GetName()
        {
            return _name;
        }

        public int GetNumber()
        {
            return _number;
        }

        public void SetNumber(int number)
        {
            _number = number;
        }


        public BitmapImage GetBitmap()
        {
            return _bitmap;
        }


        public Image GetImage()
        {
            BitmapImage image = this.GetBitmap();
            Image img = new Image();
            img.Source = image;
            return img;
        }

    }
}
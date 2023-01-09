using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Blackjack
{
    public class Player
    {
        private String _name;
        private int _money;
        private List<Cards> _cards;

        public static List<Player> PlayerList = new List<Player>();
        private PlayerType _playerType { get; set; }
        private int _bet;
        public Player(PlayerType type, String name, int money)
        {

            _name = name;
            _money = money;
            _cards = new List<Cards>();
            _playerType = type;
            _bet = 0;
            PlayerList.Add(this);

        }

        public Player(PlayerType type, String name)
        {

            _name = name;
            _money = 0;
            _cards = new List<Cards>();
            _playerType = type;
            PlayerList.Add(this);

        }

        public int GetMoney() { return _money; }
        public void SetMoney(int Geld) { this._money = Geld; }
        public int GetBet()
        {
            return _bet;
        }


        public void RemoveMoney(int value)
        {
            _money = -value;
        }

        public PlayerType GetPlayerType()
        {
            return _playerType;
        }

        public int TotalCardNumber()
        {
            int i = 0;
            foreach (Cards k in _cards)
            {
                i += k.GetNumber();
            }

            return i;
        }

        public List<Cards> GetCards()
        {
            return _cards;
        }

        public void AddCard(Cards card)
        {
            MainWindow window = MainWindow.GetClass();
            if (card.GetNumber() == 0)
            {
                if (TotalCardNumber() + 11 < 21)
                {
                    card.SetNumber(11);
                }
                else
                {
                    card.SetNumber(1);
                }
            }
            _cards.Add(card);
            Cards.CardList.Remove(card);
            window.AantalKaarten.Text = Cards.CardList.Count.ToString();
        }

        public void SetBet(int value)
        {
            _bet = value;
        }


        public String TranslateCard()
        {
            StringBuilder s = new StringBuilder();
            foreach (Cards k in _cards)
            {
                String nr = k.GetNumber().ToString();
                String naam = k.GetName();
                s.Append(nr + " " + naam + "\n");
            }

            return s.ToString();

        }

        public static Player GetPlayer(PlayerType type)
        {
            foreach (Player s in PlayerList)
            {
                if (s._playerType == type) return s;
            }
            return null;
        }
    }
}
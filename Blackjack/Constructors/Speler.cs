using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    public class Speler
    {
        private String Naam;
        private int Geld;
        private List<Kaarten> Kaarten;

        public static List<Speler> Spelers = new List<Speler>();
        private PlayerType PlayerType { get; set; }
        private int Bet;
        public Speler(PlayerType type, String naam, int geld)
        {
            Naam = naam;
            Geld = geld;
            Kaarten = new List<Kaarten>();
            PlayerType = type;
            Bet = 0;
            Spelers.Add(this);

        }

        public Speler(PlayerType type, String naam)
        {
            Naam = naam;
            Geld = 0;
            Kaarten = new List<Kaarten>();
            PlayerType = type;
            Spelers.Add(this);

        }

        public int GetGeld() { return Geld; }
        public void SetGeld(int Geld) { this.Geld = Geld; }
        public int GetBet()
        {
            return Bet;
        }

        public void RemoveGeld(int value)
        {
            Geld =- value;
        }

        public int TotaalAantal()
        {
            int i = 0;
            foreach (Kaarten k in Kaarten)
            {
                i += k.getNummer();
            }

            return i;
        }

        public List<Kaarten> getKaarten()
        {
            return Kaarten;
        }

        public void VoegKaartToe(Kaarten kaart)
        {
            Kaarten.Add(kaart);
        }
        
        public void SetBet(int value)
        {
            Bet = value;
        }


        public String VertaalKaart()
        {
            StringBuilder s = new StringBuilder();
            foreach (Kaarten k in Kaarten)
            {
                String nr = k.getNummer().ToString();
                String naam = k.getNaam();
                s.Append(nr + " " + naam + "\n");
            }

            return s.ToString();

        }

        public static Speler GetSpeler(PlayerType type)
        {
            foreach (Speler s in Spelers)
            {
                if (s.PlayerType == type) return s;
            }
            return null;
        }
    }
}

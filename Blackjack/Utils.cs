using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Blackjack
{
    internal class Utils
    {
        private static Random rnd = new Random();
        public static bool GetRandomBool()
        {
       
            Random rnd = new Random();
            int cf = rnd.Next(1, 2);

            if (cf == 1)
                return true;
            else
                return false;
        } 


        public static void clearTextBlock()
        {
            
        }

        public static void handleCards(String s, int kaartnr, TextBlock block) 
        {
            String[] KaartenNamenRegulair = { "aas", "Boer", "Vrouw", "Heer" };
            String[] KaartenNamenIrregulair = { "Schoppen ♠", "Harten ♥", "Klaveren ♣", "Ruiten ♦" };
            int cf = rnd.Next(0, 3);

            if (kaartnr == 1)
            {
                if (Utils.GetRandomBool())
                    s = block.Text + "\n" + kaartnr + " " + KaartenNamenRegulair[0];
                else
                    s = block.Text + "\n" + kaartnr + " " + KaartenNamenIrregulair[cf];
            }
            else if (kaartnr == 10)
            {
                if (Utils.GetRandomBool())
                    s = block.Text + "\n" + kaartnr + " " + KaartenNamenRegulair[rnd.Next(1, 3)];
                else
                    s = block.Text + "\n" + kaartnr + " " + KaartenNamenIrregulair[cf];
            }
            else
            {
                s = block.Text + "\n" + kaartnr + " " + KaartenNamenIrregulair[rnd.Next(0, 3)];
            }

            block.Text = s;

        

        }
    }
}

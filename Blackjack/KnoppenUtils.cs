using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Blackjack
{
    internal class KnoppenUtils
    {
        public static void Staan()
        {
           
            if (MainWindow.SpelerTotaal > MainWindow.HuisTotaal)
            {
                MessageBox.Show("Gewonnen!");
                Utils.restartGame();
                return;
            }

            if (MainWindow.SpelerTotaal == MainWindow.HuisTotaal)
            {
                MessageBox.Show("Push!");
                Utils.restartGame();
                return;
            }

            if (MainWindow.SpelerTotaal < MainWindow.HuisTotaal)
            {
                MessageBox.Show("Verloren!");
                Utils.restartGame();
                return;
            }
        }

        public static void Reset()
        {
            if (MainWindow.SpelerTotaal > 21)
            {
                MessageBox.Show("You lose!");
                Utils.restartGame();
                return;
            }

            if (MainWindow.HuisTotaal > 21)
            {
                MessageBox.Show("You Win!");
                Utils.restartGame();
                return;
            }
        }
    }
}

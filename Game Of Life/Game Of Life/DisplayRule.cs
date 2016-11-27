using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Game_Of_Life
{
    class DisplayRule
    {
        int numberOfRowsCols;
        Button[,] cellArray;
        bool[,] checkArray;
        public DisplayRule(int numberOfRowsCols, Button[,] cellArray, bool[,] checkArray)
        {
            this.numberOfRowsCols = numberOfRowsCols;
            this.cellArray = cellArray;
            this.checkArray = checkArray;
        }

        public void showRule()
        {
            for (int i = 1; i < numberOfRowsCols - 1; i++)
            {
                for (int j = 1; j < numberOfRowsCols - 1; j++)
                {
                    if (checkArray[i, j] == true)
                        cellArray[i, j].BackColor = Color.Red;
                    else cellArray[i, j].BackColor = Color.White;
                }

            }
        }
    }
}

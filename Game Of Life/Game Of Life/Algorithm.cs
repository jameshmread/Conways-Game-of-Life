using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.IO.Ports;

namespace Game_Of_Life
{
    class AlgorithmClass
    {
        public
            int numberOfGenerations;
            int numberOfRowsCols;
            Button[,] cellArray;
            bool[,] checkArray;

            SerialPort portOut;
            Exception portNotOpened = new Exception();
        
        private void openPort()
        {
            portOut = new SerialPort("COM4", 9600);
            try
            {
                portOut.DtrEnable = true;
                portOut.Open();
            }
            catch(Exception portNotOpened)
            {
                Console.WriteLine("Port not opened");
            }
        }

        public  AlgorithmClass(Button[,] AcellArray,bool[,] AcheckArray, int AnumberOfGenerations, int AnumberOfRowsCols)
        {
            this.numberOfGenerations = AnumberOfGenerations;
            this.numberOfRowsCols = AnumberOfRowsCols;
            this.cellArray = AcellArray;
            this.checkArray = AcheckArray;
            openPort();
        }
        public void algorithm()
        {
            //portOut.Write("generation increase");
                numberOfGenerations++;
                // .Text = "" + numberOfGenerations;

                //first nested loop sets the check array to hold information about cells
                //trying to prevent the changes cascading and changing once a single cell has changed
                for (int i = 1; i < numberOfRowsCols - 1; i++)
                {
                    for (int j = 1; j < numberOfRowsCols - 1; j++)
                    {
                        if (cellArray[i, j].BackColor.Equals(Color.Red))
                            checkArray[i, j] = true;
                        else checkArray[i, j] = false;
                    }

                }
                //checks button cells
                for (int i = 1; i < numberOfRowsCols - 1; i++)
                {
                    for (int j = 1; j < numberOfRowsCols - 1; j++)
                    {
                        try
                        {
                            if (i == numberOfRowsCols - 2 && j == numberOfRowsCols - 2)
                                applyCheckToButtonGrid(); //actual state change display

                            ruleSet(cellArray[i, j],
                                cellArray[i - 1, j - 1], cellArray[i, j - 1], cellArray[i + 1, j - 1],
                                cellArray[i - 1, j], cellArray[i + 1, j],
                                cellArray[i - 1, j + 1], cellArray[i, j + 1], cellArray[i + 1, j + 1], i, j);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            //do nothing
                        }
                    }
                }
            
        }
        private void ruleSet(Button centralButton, Button TL, Button TM, Button TR, Button ML, Button MR, Button BL, Button BM, Button BR, int iPos, int jPos)
        {
            int numberOfNeighbours = 0;
            if (centralButton.BackColor.Equals(Color.Red)) //i.e. cell is alive
            {
                if (TL.BackColor.Equals(Color.Red))
                    numberOfNeighbours++;
                if (TM.BackColor.Equals(Color.Red))
                    numberOfNeighbours++;
                if (TR.BackColor.Equals(Color.Red))
                    numberOfNeighbours++;
                if (ML.BackColor.Equals(Color.Red))
                    numberOfNeighbours++;
                if (MR.BackColor.Equals(Color.Red))
                    numberOfNeighbours++;
                if (BL.BackColor.Equals(Color.Red))
                    numberOfNeighbours++;
                if (BM.BackColor.Equals(Color.Red))
                    numberOfNeighbours++;
                if (BR.BackColor.Equals(Color.Red))
                    numberOfNeighbours++;

                applyRule(numberOfNeighbours, true, iPos, jPos);
                return;
            }
            else if (centralButton.BackColor.Equals(Color.White)) //i.e. cell is dead
            {
                if (TL.BackColor.Equals(Color.Red))
                    numberOfNeighbours++;
                if (TM.BackColor.Equals(Color.Red))
                    numberOfNeighbours++;
                if (TR.BackColor.Equals(Color.Red))
                    numberOfNeighbours++;
                if (ML.BackColor.Equals(Color.Red))
                    numberOfNeighbours++;
                if (MR.BackColor.Equals(Color.Red))
                    numberOfNeighbours++;
                if (BL.BackColor.Equals(Color.Red))
                    numberOfNeighbours++;
                if (BM.BackColor.Equals(Color.Red))
                    numberOfNeighbours++;
                if (BR.BackColor.Equals(Color.Red))
                    numberOfNeighbours++;

                applyRule(numberOfNeighbours, false, iPos, jPos);
                return;
            }
        }

        private void applyRule(int neighbours, bool centralCellAlive, int iPos, int jPos)
        {
            if (centralCellAlive)
            {
                if (neighbours < 2)
                    //centralCell.BackColor = Color.White;
                    checkArray[iPos, jPos] = false;
                else if (neighbours > 3)
                    //centralCell.BackColor = Color.White;
                    checkArray[iPos, jPos] = false;

                else if (neighbours == 2 || neighbours == 3)
                    //centralCell.BackColor = Color.Red;
                    checkArray[iPos, jPos] = true;
            }
            else if (!centralCellAlive)
            {
                if (neighbours == 3)
                    //centralCell.BackColor = Color.Red;
                    checkArray[iPos, jPos] = true;
                //ELSE PROBS NOT NEEDED centralCell.BackColor = Color.White;
            }
        }

        //applies state of check to button grid
        private void applyCheckToButtonGrid()
        {
            DisplayRule displayRule = new DisplayRule(numberOfRowsCols, cellArray, checkArray);

            Thread displayThread = new Thread(new ThreadStart(displayRule.showRule));
            displayThread.Start();
        }
    }
}

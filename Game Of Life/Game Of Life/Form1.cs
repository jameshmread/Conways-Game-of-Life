using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Game_Of_Life
{
    public partial class Form1 : Form
    {
        public
        static int gameWidth = 10;
        //static int gameHeight = 10;
        static int windowWidth = 500;
        static int windowHeight = 500;
        static int cellSize = 25;
        static int numberOfRowsCols = 20;
        int numberOfGenerations = 0;
        Button[,] cellArray = new Button[numberOfRowsCols,numberOfRowsCols];
        bool[,] checkArray = new bool[numberOfRowsCols, numberOfRowsCols];
        AlgorithmClass _algorithmClass;
        public Form1()
        {
            InitializeComponent();
            
            _algorithmClass = new AlgorithmClass(cellArray, checkArray, numberOfGenerations, numberOfRowsCols);
            this.Size = new Size(windowWidth, windowHeight);
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            createButtons();
        }

        //creates array of buttons
        private void createButtons()
        {
            for(int i = 0; i < numberOfRowsCols; i++)
            {
                for(int j = 0; j < numberOfRowsCols; j++)
                {
                    Button newButton = new Button();
                    newButton.Location = new Point(i * cellSize, j * cellSize);
                    newButton.Size = new Size(cellSize,cellSize);
                    newButton.BackColor = Color.White;
                    this.Controls.Add(newButton);
                    newButton.Click += newButton_Click;
                    cellArray[i, j] = newButton;
                    checkArray[i, j] = false;
                }
            }

            
        }

        void newButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            //throw new NotImplementedException();
            //Console.WriteLine(btn);
            if (btn.BackColor.Equals(Color.Red))
            {    
                btn.BackColor = Color.White;
            }
            else
            {
                btn.BackColor = Color.Red;  
            }
        }

       

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.B)
            {
                //algorithm(cellArray);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _algorithmClass = new AlgorithmClass(cellArray, checkArray, numberOfGenerations, numberOfRowsCols);
         
            Thread _thread = new Thread(new ThreadStart(_algorithmClass.algorithm));
            _thread.Start();
                //_algorithmClass.algorithm(cellArray);
            
        }
    }

    
}

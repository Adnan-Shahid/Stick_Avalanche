//Adnan Shahid
//May 27 2014
//Main Menu
//Main menu form that gies the user option for what to do in the beginning of the game
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AdnanShahid_FinalCulminating
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }
        
        //Variable Declaration
        int intMouseY;

        protected override void OnMouseMove(MouseEventArgs e)
        {
           //Makes stickman image move with the mouse 
            base.OnMouseMove(e);
            intMouseY = e.Y;
            if (intMouseY > 145 && intMouseY < 330)
            {
                pcbCursor.Top = intMouseY;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit(); //Exit the Application
        }

        private void btnHighScores_Click(object sender, EventArgs e)
        {
            High_Scores HighScores = new High_Scores(); //Open High Scores
            this.Hide();
            HighScores.Show();
        }

        private void btnInstructions_Click(object sender, EventArgs e)
        {
            Instructions Instructions = new Instructions();     //Open Instructions
            this.Hide();
            Instructions.Show();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Game Game = new Game(); //Open Game
            this.Hide();
            Game.Show();
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();     //Close all forms when clicking the x button on the top right
        }

    }
}

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
    public partial class EnterName : Form
    {
        public EnterName()
        {
            InitializeComponent();
        }
        int intFinalScore = Game.intFinalScore;
        string strName;

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblScore.Text = intFinalScore.ToString();   //Global variable show on label
        }

        private void EnterName_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); //Close all forms when clicking x button
        }

        private void btnHighScores_Click(object sender, EventArgs e)
        {
            if (this.txtName.Text == "")
            {
                this.txtName.Text = "???";  //If player doesnt enter anything for his name
            }
            strName = this.txtName.Text;    //Input from user
            //Entering and editing file
            StreamWriter Tex = new StreamWriter("Highscores.txt", true);    
            //Method to write down high score
            Tex.WriteLine(strName + ", " + intFinalScore);
            Tex.Flush();
            Tex.Close();
            High_Scores HighScores = new High_Scores(); //Open High Scores
            this.Hide();
            HighScores.Show();

        }
    }
}

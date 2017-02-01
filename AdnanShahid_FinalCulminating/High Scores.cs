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
    public partial class High_Scores : Form
    {
        public High_Scores()
        {
            InitializeComponent();
            //Variable Declaration
            int intTemp = 0;
            string strTemp = "";
            int intCounter = 0;
            string strInput;
            int intIndexValue;
            //Reading High scores file
            StreamReader re = File.OpenText("HighScores.txt");
            while ((strInput = re.ReadLine()) != null)
            {
                intCounter++;
            }
            re.Close();

            //Getting length of array
            string[] strName = new string[intCounter];
            int[] intScore = new int[intCounter];
            StreamReader re2 = File.OpenText("HighScores.txt");
            //Getting values for the name and for the score
            for (int intCounter2 = 0; intCounter2 < intCounter; intCounter2++)
            {
                strInput = re2.ReadLine();
                intIndexValue = strInput.IndexOf(",");
                strName[intCounter2] = strInput.Substring(0, intIndexValue);
                intScore[intCounter2] = Int32.Parse(strInput.Substring(intIndexValue + 1, strInput.Length - (intIndexValue + 1)));
            }
            re2.Close();
            
            //Sorting the scores and the names together
            for (int intCounter3 = 0; intCounter3 < intCounter; intCounter3++)
            {
                for (int intCounter4 = 0; intCounter4 < (intCounter - 1); intCounter4++)
                {
                    if (intScore[intCounter4] < intScore[intCounter4 + 1])
                    {
                        strTemp = strName[intCounter4];
                        intTemp = intScore[intCounter4];
                        strName[intCounter4] = strName[intCounter4 + 1];
                        intScore[intCounter4] = intScore[intCounter4 + 1];
                        strName[intCounter4 + 1] = strTemp;
                        intScore[intCounter4 + 1] = intTemp;
                        intCounter4 = 0;
                    }
                }
            }
            //Output to the user
            lblName1.Text = strName[0];
            lblName2.Text = strName[1];
            lblName3.Text = strName[2];
            lblName4.Text = strName[3];
            lblName5.Text = strName[4];

            lblScore1.Text = intScore[0].ToString();
            lblScore2.Text = intScore[1].ToString();
            lblScore3.Text = intScore[2].ToString();
            lblScore4.Text = intScore[3].ToString();
            lblScore5.Text = intScore[4].ToString();

        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Application.Restart();  //Restarting game to reset the timers
        }

        private void High_Scores_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); //Close all forms when clicking the x button on the top right
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit(); //Close the game
        }
    }
}

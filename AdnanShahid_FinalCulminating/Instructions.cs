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
    public partial class Instructions : Form
    {
        public Instructions()
        {
            InitializeComponent();
           
        }

        

        private void lblGoal_Click(object sender, EventArgs e)
        {

        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            MainMenu MainMenu = new MainMenu(); //Return to main menu
            this.Hide();
            MainMenu.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Instructions_ForeColorChanged(object sender, EventArgs e)
        {

        }

        private void Instructions_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); //Close all forms when clicking the x button on the top right
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class splashForm : Form
    {
        public splashForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // this method brings up the game board in one player mode
            mainForm frm = new mainForm(); // create the main form
            frm.Text = "TicTacToe: One Player"; // change the title text to indicate one player mode
            frm.FormClosed += new FormClosedEventHandler(mainForm_FormClosed); // not really sure what this line does, grabbed it from the help file
            frm.Show(); // show the main form
            this.Hide(); // hide this splash form

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // this method brings up the game board in two player mode
            mainForm frm = new mainForm();
            frm.Text = "TicTacToe: Two Players";
            frm.FormClosed += new FormClosedEventHandler(mainForm_FormClosed);
            frm.Show();
            this.Hide();

        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // this method closes the splash form when the main form is closed
            this.Close();
        }
    }
}

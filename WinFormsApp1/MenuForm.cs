using System;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void btnTwoPlayers_Click(object sender, EventArgs e)
        {
            // false = режим двух игроков (без бота)
            var gameForm = new Form1(false);
            gameForm.Show();
            this.Hide();
            gameForm.FormClosed += (s, args) => this.Close();
        }

        private void btnWithBot_Click(object sender, EventArgs e)
        {
            // true = режим с ботом
            var gameForm = new Form1(true);
            gameForm.Show();
            this.Hide();
            gameForm.FormClosed += (s, args) => this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
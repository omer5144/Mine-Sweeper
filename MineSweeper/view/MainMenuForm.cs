using MineSweeper.model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MineSweeper.view
{
    public partial class MainMenuForm : Form
    {
        // Main Menu Form:
        // includes the game options
        
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            this.Location = new System.Drawing.Point(150, 30);
            this.Size = new Size(480, 620);
        }

        // easy difficulty
        private void Easy_Click(object sender, EventArgs e)
        {
            GameForm form = new GameForm(Difficulty.easy);
            this.Hide();
            form.Show();
        }

        // medium difficulty
        private void Medium_Click(object sender, EventArgs e)
        {
            GameForm form = new GameForm(Difficulty.medium);
            this.Hide();
            form.Show();
        }

        // hard difficulty
        private void Hard_Click(object sender, EventArgs e)
        {
            GameForm form = new GameForm(Difficulty.hard);
            this.Hide();
            form.Show();
        }

        // when form closed
        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

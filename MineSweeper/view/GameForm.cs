using MineSweeper.model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MineSweeper.view
{
    public partial class GameForm : Form
    {
        // Game Form:
        // includes the view components and the logic

        private readonly Difficulty difficulty; // the game difficulty: easy/medium/hard
        private ControlPanel controlPanel; // the control panel view
        private Board board; // tha board view
        private Game game; // the game logic

        public GameForm(Difficulty difficulty)
        {
            InitializeComponent();

            this.difficulty = difficulty;
        }
        private void GameForm_Load(object sender, EventArgs e)
        {
            int width = 480, height = 120, extraW = 0;
            if (difficulty == Difficulty.hard)
            {
                width = 980;
                height = 60;
                extraW = -17;
            }

            this.BackColor = Color.White;
            this.Size = new Size(width + 17 + extraW, 520 + height); ;
            this.Location = new System.Drawing.Point(30, 30);

            controlPanel = new ControlPanel(difficulty, width + extraW, height, 0, 0, Back, Replay, Mode, this.Controls);
            board = new Board(difficulty, width, 480, 0, height, Press, this.Controls);
            game = new Game(difficulty, controlPanel, board);
            
        }
        
        // when button on board pressed
        private void Press(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string[] location = b.Name.Split(',');

            MouseEventArgs mouseEvent = e as MouseEventArgs;
            if (mouseEvent.Button == MouseButtons.Right)
            {
                game.ChangePress();
                game.Click(new model.Point(int.Parse(location[0]), int.Parse(location[1])));
                game.ChangePress();
            }
            else
            {
                game.Click(new model.Point(int.Parse(location[0]), int.Parse(location[1])));
            }

        } 
       
        // when replay button pressed 
        private void Replay(object sender, EventArgs e)
        {
            this.Hide();
            GameForm form = new GameForm(difficulty);
            form.Show();
        }
        
        // when back button pressed
        private void Back(object sender, EventArgs e)
        {
            this.Hide();
            MainMenuForm form = new MainMenuForm();
            form.Show();
        }
        
        // when mode buttom pressed
        private void Mode(object sender, EventArgs e)
        {
            game.ChangePress();
        }
        
        // when form closed
        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}

using MineSweeper.model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MineSweeper.view
{
    class ControlPanel : IObserver
    {
        // this class represent the control panel view

        private readonly Button btnBack; // back buttom
        private readonly Button btnreplay; // replay button
        private readonly Button btnBombs; // number of bombs button
        private readonly Button btnPress; // change press mode button
        private readonly Label lblSituation; // win/lose label

        public ControlPanel(Difficulty difficulty,int width, int height, int xStart, int yStart, EventHandler clkBack, EventHandler clkreplay, EventHandler clkPress, Control.ControlCollection controls)
        {
            int W = 0, H = 0;
            switch (difficulty)
            {
                case Difficulty.easy:
                    W = width / 8;
                    H = height / 2;
                    break;
                case Difficulty.medium:
                    W = width / 8;
                    H = height / 2;
                    break;
                case Difficulty.hard:
                    W = width / 16;
                    H = height;
                    break;
            }

            btnBack = new Button();
            btnreplay = new Button();
            btnBombs = new Button();
            btnPress = new Button();
            lblSituation = new Label();

            btnBack.BackColor = Color.White;
            btnreplay.BackColor = Color.White;
            btnBombs.BackColor = Color.White;
            btnPress.BackColor = Color.White;
            lblSituation.BackColor = Color.White;

            btnreplay.Size = new Size(2 * W, H);
            btnBack.Size = new Size(2 * W, H);
            btnPress.Size = new Size(2 * W, H);
            btnBombs.Size = new Size(2 * W, H);
            lblSituation.Size = new Size(4 * W -10, H/2);

            int bombs = 0;
            switch (difficulty)
            {
                case Difficulty.easy:
                    btnBombs.Location = new System.Drawing.Point(xStart, yStart);
                    btnPress.Location = new System.Drawing.Point(xStart, yStart + H);
                    btnBack.Location = new System.Drawing.Point(xStart + 6 * W, yStart);
                    btnreplay.Location = new System.Drawing.Point(xStart + 6 * W, yStart + H);
                    lblSituation.Location = new System.Drawing.Point(xStart + 2 * W + 30, yStart + 30);
                    bombs = 10;
                    break;
                case Difficulty.medium:
                    btnBombs.Location = new System.Drawing.Point(xStart, yStart);
                    btnPress.Location = new System.Drawing.Point(xStart, yStart + H);
                    btnBack.Location = new System.Drawing.Point(xStart + 6 * W, yStart);
                    btnreplay.Location = new System.Drawing.Point(xStart + 6 * W, yStart + H);
                    lblSituation.Location = new System.Drawing.Point(xStart + 2 * W + 30, yStart + 30);
                    bombs = 40;
                    break;
                case Difficulty.hard:
                    btnBombs.Location = new System.Drawing.Point(xStart, yStart);
                    btnPress.Location = new System.Drawing.Point(xStart + 2 * W, yStart);
                    btnBack.Location = new System.Drawing.Point(xStart + 12 * W, yStart);
                    btnreplay.Location = new System.Drawing.Point(xStart + 14 * W, yStart);
                   lblSituation.Location = new System.Drawing.Point(xStart + 4 * W + 150, yStart + 10);
                    bombs = 99;
                    break;
            }

            btnreplay.Text = "replay";
            btnBack.Text = "Back";
            btnPress.Text = "💣";
            btnBombs.Text = bombs.ToString();
            lblSituation.Text = "";


            btnreplay.Font = new Font("Cooper Black", 22);
            btnBack.Font = new Font("Cooper Black", 22);
            btnPress.Font = new Font("Cooper Black", 22);
            btnBombs.Font = new Font("Cooper Black", 22);
            lblSituation.Font = new Font("Cooper Black", 22);

            btnreplay.Click += clkreplay;
            btnBack.Click += clkBack;
            btnPress.Click += clkPress;
            btnPress.Click += Press;
            btnBombs.Enabled = false;

            controls.Add(btnreplay);
            controls.Add(btnBack);
            controls.Add(btnPress);
            controls.Add(btnBombs);
            controls.Add(lblSituation);
        }
        
        // when game notify that control panel changed
        public void Update(Observable o, object arg)
        {
            if (!(arg is ControlPanelArgument))
                return;

            ControlPanelArgument argument = arg as ControlPanelArgument;

            btnBombs.Text = argument.Bombs.ToString();

            switch(argument.Situation)
            {
                case Situation.win:
                    lblSituation.Text = " YOU WIN!";
                    lblSituation.ForeColor = Color.DarkGreen;
                    break;
                case Situation.lose:
                    lblSituation.Text = "YOU LOSE!";
                    lblSituation.ForeColor = Color.DarkRed;
                    break;
            }
        }
       
        // when press change mode
        public void Press(object sender, EventArgs e)
        {
            Button press = sender as Button;
            if(press.Text.Equals("🚩"))
            {
                press.Text = "💣";
                press.ForeColor = Color.Black;
            }
            else
            {
                press.Text = "🚩";
                press.ForeColor = Color.Red;
            }
        }
    }
}

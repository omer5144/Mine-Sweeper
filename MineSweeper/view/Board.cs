using System.Windows.Forms;
using System.Drawing;
using MineSweeper.model;

namespace MineSweeper.view
{
    class Board : IObserver
    {
        // this class represent the board view

        private readonly Button[,] buttons; // matrix of buttons
        private readonly MouseEventHandler click; // click action for buttons in matrix

        public Board(Difficulty difficulty, int width, int height, int xStart, int yStart, MouseEventHandler click, Control.ControlCollection controls)
        {
            this.click = click;

            int rows = 0, cols = 0;
            switch (difficulty)
            {
                case Difficulty.easy:
                    rows = 8;
                    cols = 8;
                    break;
                case Difficulty.medium:
                    rows = 16;
                    cols = 16;
                    break;
                case Difficulty.hard:
                    rows = 16;
                    cols = 32;
                    break;
            }

            buttons = new Button[rows, cols];
            for(int i=0;i<rows;i++)
            {
                for(int j=0;j<cols;j++)
                {
                    Button b = new Button
                    {
                        Name = i + "," + j,
                        Size = new Size((int)(width / cols), (int)(height / rows)),
                        Location = new System.Drawing.Point((int)(xStart + j * (width / cols)), (int)(yStart + i * (height / rows)))
                    };
                    b.MouseDown += click;
                    b.Font = new Font("Cooper", (int)(width/(cols*3)), FontStyle.Bold);
                    b.ForeColor = Color.Black;
                    b.BackColor = Color.White;
                    b.Text = "";
                    controls.Add(b);
                    buttons[i, j] = b;
                }
            }
        }
        
        // whan game notify that the board has changed
        public void Update(Observable o, object arg)
        {
            if (!(arg is BoardArgument))
                return;
            BoardArgument argument = arg as BoardArgument;
            int x = argument.Point.X;
            int y = argument.Point.Y;
            Cube cube = argument.Cubes[x, y];

            if (argument.Situation == Situation.lose)
                Lose(argument);
            else if (argument.Situation == Situation.win)
                Win(argument);
            else
                switch (cube.State)
                {
                    case State.flag:
                        buttons[x, y].Text = "🚩";
                        break;
                    case State.none:
                        buttons[x, y].Text = "";
                        break;
                    case State.check:
                        switch (cube.Value)
                        {
                            case 0:
                                buttons[x, y].Text = "";
                                buttons[x, y].Enabled = false;
                                buttons[x, y].BackColor = Color.LightGray; // gray
                                break;
                            case 1:
                                buttons[x, y].Text = cube.Value.ToString();
                                buttons[x, y].BackColor = Color.FromArgb(179, 179, 255); // blue
                                break;
                            case 2:
                                buttons[x, y].Text = cube.Value.ToString();
                                buttons[x, y].BackColor = Color.FromArgb(179, 255, 179); // green
                                break;
                            case 3:
                                buttons[x, y].Text = cube.Value.ToString();
                                buttons[x, y].BackColor = Color.FromArgb(255, 179, 179); // red
                                break;
                            case 4:
                                buttons[x, y].Text = cube.Value.ToString();
                                buttons[x, y].BackColor = Color.FromArgb(255, 255, 179); // yellow
                                break;
                            case 5:
                                buttons[x, y].Text = cube.Value.ToString();
                                buttons[x, y].BackColor = Color.FromArgb(255, 209, 179); // orange
                                break;
                            case 6:
                                buttons[x, y].Text = cube.Value.ToString();
                                buttons[x, y].BackColor = Color.FromArgb(255, 179, 255); // pink
                                break;
                            case 7:
                                buttons[x, y].Text = cube.Value.ToString();
                                buttons[x, y].BackColor = Color.FromArgb(179, 255, 255); // light blue
                                break;
                            case 8:
                                buttons[x, y].Text = cube.Value.ToString();
                                buttons[x, y].BackColor = Color.FromArgb(209, 179, 255); // purple
                                break;
                        } 
                        break;
                }
        }
       
        // when user lose
        private void Lose(BoardArgument argument)
        {
            int x = argument.Point.X;
            int y = argument.Point.Y;
            Cube[,] cubes = argument.Cubes;

            for (int i = 0; i < buttons.GetLength(0); i++)
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    if (i == x && j == y)
                    {
                        buttons[i, j].BackColor = Color.Red;
                        buttons[i, j].ForeColor = Color.White;
                        buttons[i, j].Text = "💣";
                    }

                    if (cubes[i, j].State == State.flag)
                    {
                        if (cubes[i, j].Value != -1)
                        {
                            buttons[i, j].Text = "❌";
                            buttons[i, j].ForeColor = Color.Red;
                        }
                    }
                    else if (cubes[i, j].State == State.none)
                    {
                        switch (cubes[i,j].Value)
                        {
                            case 0:
                                buttons[i, j].Text = "";
                                buttons[i, j].Enabled = false;
                                buttons[i, j].BackColor = Color.LightGray; // gray
                                break;
                            case 1:
                                buttons[i, j].Text = cubes[i, j].Value.ToString();
                                buttons[i, j].BackColor = Color.FromArgb(179, 179, 255); // blue
                                break;
                            case 2:
                                buttons[i, j].Text = cubes[i, j].Value.ToString();
                                buttons[i, j].BackColor = Color.FromArgb(179, 255, 179); // green
                                break;
                            case 3:
                                buttons[i, j].Text = cubes[i, j].Value.ToString();
                                buttons[i, j].BackColor = Color.FromArgb(255, 179, 179); // red
                                break;
                            case 4:
                                buttons[i, j].Text = cubes[i, j].Value.ToString();
                                buttons[i, j].BackColor = Color.FromArgb(255, 255, 179); // yellow
                                break;
                            case 5:
                                buttons[i, j].Text = cubes[i, j].Value.ToString();
                                buttons[i, j].BackColor = Color.FromArgb(255, 209, 179); // orange
                                break;
                            case 6:
                                buttons[i, j].Text = cubes[i, j].Value.ToString();
                                buttons[i, j].BackColor = Color.FromArgb(255, 179, 255); // pink
                                break;
                            case 7:
                                buttons[i, j].Text = cubes[i, j].Value.ToString();
                                buttons[i, j].BackColor = Color.FromArgb(179, 255, 255); // light blue
                                break;
                            case 8:
                                buttons[i, j].Text = cubes[i, j].Value.ToString();
                                buttons[i, j].BackColor = Color.FromArgb(209, 179, 255); // purple
                                break;
                            case -1:
                                buttons[i, j].Text = "💣";
                                buttons[i, j].BackColor = Color.Black; // black
                                buttons[i, j].ForeColor = Color.White; // white
                                break;

                        }
                    }

                    buttons[i, j].MouseDown -= click;

                }
        }
        
        // when user win
        private void Win(BoardArgument argument)
        {
            Cube[,] cubes = argument.Cubes;

            for (int i = 0; i < buttons.GetLength(0); i++)
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    if (cubes[i, j].State == State.flag)
                    {
                        buttons[i, j].Text = "✔️";
                        buttons[i, j].ForeColor = Color.Green;
                    }
                    else if (cubes[i, j].State == State.none)
                    {
                        switch (cubes[i, j].Value)
                        {
                            case 0:
                                buttons[i, j].Text = "";
                                buttons[i, j].Enabled = false;
                                buttons[i, j].BackColor = Color.LightGray; // gray
                                break;
                            case 1:
                                buttons[i, j].Text = cubes[i, j].Value.ToString();
                                buttons[i, j].BackColor = Color.FromArgb(179, 179, 255); // blue
                                break;
                            case 2:
                                buttons[i, j].Text = cubes[i, j].Value.ToString();
                                buttons[i, j].BackColor = Color.FromArgb(179, 255, 179); // green
                                break;
                            case 3:
                                buttons[i, j].Text = cubes[i, j].Value.ToString();
                                buttons[i, j].BackColor = Color.FromArgb(255, 179, 179); // red
                                break;
                            case 4:
                                buttons[i, j].Text = cubes[i, j].Value.ToString();
                                buttons[i, j].BackColor = Color.FromArgb(255, 255, 179); // yellow
                                break;
                            case 5:
                                buttons[i, j].Text = cubes[i, j].Value.ToString();
                                buttons[i, j].BackColor = Color.FromArgb(255, 209, 179); // orange
                                break;
                            case 6:
                                buttons[i, j].Text = cubes[i, j].Value.ToString();
                                buttons[i, j].BackColor = Color.FromArgb(255, 179, 255); // pink
                                break;
                            case 7:
                                buttons[i, j].Text = cubes[i, j].Value.ToString();
                                buttons[i, j].BackColor = Color.FromArgb(179, 255, 255); // light blue
                                break;
                            case 8:
                                buttons[i, j].Text = cubes[i, j].Value.ToString();
                                buttons[i, j].BackColor = Color.FromArgb(209, 179, 255); // purple
                                break;
                            case -1:
                                buttons[i, j].Text = "💣";
                                buttons[i, j].BackColor = Color.Black; // black
                                buttons[i, j].ForeColor = Color.White; // white
                                break;

                        }
                    }

                    buttons[i, j].MouseDown -= click;

                }
        }
    }
}

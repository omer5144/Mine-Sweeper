using System;
using System.Collections.Generic;

namespace MineSweeper.model
{
    class Matrix : Observable
    {
        // Matrix:
        // this class represent matrix of cubes

        private Cube[,] matrix; // the cube matrix

        public Matrix(params IObserver[] observers)
        {
            foreach(IObserver observer in observers)
                AddObserver(observer);
        }

        // get state in a specific point location
        public State GetState(Point point)
        {
            return matrix[point.X, point.Y].State;
        }

        // set state in a specific point location
        public void SetState(Point point, State state, Situation situation)
        {
            matrix[point.X, point.Y].State = state;
            NotifyObservers(new BoardArgument(matrix, point, situation));
        }

        // notify to observers that number of bombs changed 
        public void SetBombs(int bombs, Situation situation)
        {
            NotifyObservers(new ControlPanelArgument(bombs, situation));
        }

        // get value in a specific point location
        public int GetValue(Point point)
        {
            return matrix[point.X, point.Y].Value;
        }

        // get win/lose situation (the current point location is the last one that pressed 
        public Situation GetSituation(Point point)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if ((i != point.X || j != point.Y) && matrix[i, j].Value == -1 && matrix[i, j].State != State.flag)
                    {
                        return Situation.lose;
                    }
                }
            }
            return Situation.win;
        }
        
        // create matrix values (don't put bomb in first point location)
        public void Create(int rows, int cols, int total_bombs, Point firstPoint)
        {
            matrix = new Cube[rows, cols];

            Random rnd = new Random();

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    matrix[i, j] = new Cube();

            int num = total_bombs;
            while (num > 0)
            {
                int i = rnd.Next(0, rows);
                int j = rnd.Next(0, cols);

                if (matrix[i, j].Value == 10 && !(i == firstPoint.X && j == firstPoint.Y))
                {
                    matrix[i, j].Value = -1;
                    num--;
                }
            }

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    if (matrix[i, j].Value == 10)
                        matrix[i, j].Value = BombsAround(new Point(i, j));
        }
        
        // number of bombs around specific point location
        public int BombsAround(Point point)
        {
            int start_row, end_row, start_col, end_col;
            int counter = 0;
            if (point.X == 0)
            {
                start_row = point.X;
                end_row = point.X + 1;
            }
            else if (point.X == matrix.GetLength(0) - 1)
            {
                start_row = point.X - 1;
                end_row = point.X;
            }
            else
            {
                start_row = point.X - 1;
                end_row = point.X + 1;
            }

            if (point.Y == 0)
            {
                start_col = point.Y;
                end_col = point.Y + 1;
            }
            else if (point.Y == matrix.GetLength(1) - 1)
            {
                start_col = point.Y - 1;
                end_col = point.Y;
            }
            else
            {
                start_col = point.Y - 1;
                end_col = point.Y + 1;
            }

            for (int x = start_row; x <= end_row; x++)
                for (int y = start_col; y <= end_col; y++)
                {
                    if (matrix[x, y].Value == -1)
                        counter++;
                }
            return counter;
        }
       
        // number of flags around specific point location
        public int FlagsAround(Point point)
        {
            int start_row, end_row, start_col, end_col;
            int counter = 0;
            if (point.X == 0)
            {
                start_row = point.X;
                end_row = point.X + 1;
            }
            else if (point.X == matrix.GetLength(0) - 1)
            {
                start_row = point.X - 1;
                end_row = point.X;
            }
            else
            {
                start_row = point.X - 1;
                end_row = point.X + 1;
            }

            if (point.Y == 0)
            {
                start_col = point.Y;
                end_col = point.Y + 1;
            }
            else if (point.Y == matrix.GetLength(1) - 1)
            {
                start_col = point.Y - 1;
                end_col = point.Y;
            }
            else
            {
                start_col = point.Y - 1;
                end_col = point.Y + 1;
            }

            for (int x = start_row; x <= end_row; x++)
                for (int y = start_col; y <= end_col; y++)
                {
                    if (matrix[x, y].State == State.flag)
                        counter++;
                }
            return counter;
        }
        
        // points locations around specific point location
        public List<Point> PointsAround(Point point)
        {
            int start_row, end_row, start_col, end_col;
            List<Point> list = new List<Point>();
            if (point.X == 0)
            {
                start_row = point.X;
                end_row = point.X + 1;
            }
            else if (point.X == matrix.GetLength(0) - 1)
            {
                start_row = point.X - 1;
                end_row = point.X;
            }
            else
            {
                start_row = point.X - 1;
                end_row = point.X + 1;
            }

            if (point.Y == 0)
            {
                start_col = point.Y;
                end_col = point.Y + 1;
            }
            else if (point.Y == matrix.GetLength(1) - 1)
            {
                start_col = point.Y - 1;
                end_col = point.Y;
            }
            else
            {
                start_col = point.Y - 1;
                end_col = point.Y + 1;
            }

            for (int x = start_row; x <= end_row; x++)
                for (int y = start_col; y <= end_col; y++)
                {
                    if(matrix[x,y].State == State.none)
                        list.Add(new Point(x, y));

                }

            return list;
        }
    }
}

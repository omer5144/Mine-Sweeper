using System.Collections.Generic;

namespace MineSweeper.model
{
    class Game
    {
        // Game:
        // this class represent the game logic

        private readonly Matrix matrix; // the matrix of cubes in the game
        public int Rows { get; } // number of rows in matrix
        public int Cols { get; } // number of columns in matrix
        public int Bombs_remain { get; private set; } // number of bombs that remain in the game
        public Press Press { get; private set; } // the current mine/tick press
        private bool first_press; // is pressed yet?

        public Game(Difficulty difficulty, params IObserver[] observers)
        {
            matrix = new Matrix(observers);

            this.first_press = true;
            this.Press = Press.mine;

            switch(difficulty)
            {
                case Difficulty.easy:
                    Rows = 8;
                    Cols = 8;
                    Bombs_remain = 10;
                    break;
                case Difficulty.medium:
                    Rows = 16;
                    Cols = 16;
                    Bombs_remain = 40;
                    break;
                case Difficulty.hard:
                    Rows = 16;
                    Cols = 32;
                    Bombs_remain = 99;
                    break;
            }
        }

        // get the value in the specific point location
        public int GetValue(Point point)
        {
            return matrix.GetValue(point);
        }
       
        // change press mode from mine to tick and oposite
        public void ChangePress()
        {
            if (Press == Press.mine)
                Press = Press.tick;
            else
                Press = Press.mine;
        }  
       
        // click the cube in the point position according to the press mode
        public void Click(Point point)
        {
            if (first_press) // create matrix just after first click
            {
                matrix.Create(Rows, Cols, Bombs_remain, point);
                first_press = false;
            }

            if (Press == Press.tick) // tick press mode
            {
                State state;
                switch (matrix.GetState(point))
                {
                    case State.none:
                        state =  State.flag;
                        Bombs_remain -= 1 ;
                        if (Bombs_remain == 0)
                        {
                            matrix.SetState(point, state, matrix.GetSituation(point));
                            matrix.SetBombs(Bombs_remain, matrix.GetSituation(point));
                        }
                        else
                        {
                            matrix.SetState(point, state, Situation.none);
                            matrix.SetBombs(Bombs_remain, Situation.none);
                        }
                        break;
                    case State.flag:
                        state =  State.none;
                        Bombs_remain +=1;
                        if (Bombs_remain == 0)
                        {
                            matrix.SetState(point, state, matrix.GetSituation(point));
                            matrix.SetBombs(Bombs_remain, matrix.GetSituation(point));
                        }
                        else
                        {
                            matrix.SetState(point, state, Situation.none);
                            matrix.SetBombs(Bombs_remain, Situation.none);
                        }
                        break;
                }
                
            }
            else // mine press mode
            {
                switch (matrix.GetState(point))
                {
                    case State.none:
                        if(matrix.GetValue(point) == -1)
                        {
                            matrix.SetState(point, State.check, Situation.lose);
                            matrix.SetBombs(Bombs_remain, Situation.lose);
                        }
                        else
                            matrix.SetState(point, State.check, Situation.none);

                        if (matrix.GetValue(point) == matrix.FlagsAround(point) || matrix.GetValue(point) == 0)
                        {
                            List<Point> points = matrix.PointsAround(point);
                            foreach (Point p in points)
                                SemiClick(p);
                        }
                        break;
                    case State.check:
                        if (matrix.GetValue(point) == matrix.FlagsAround(point) || matrix.GetValue(point) == 0)
                        {
                            List<Point> points = matrix.PointsAround(point);
                            foreach (Point p in points)
                                SemiClick(p);
                        }
                        break;
                }
            }
        }
        
        // used to create auto click when mine at value 0
        private void SemiClick(Point point)
        {
            switch (matrix.GetState(point))
            {
                case State.none:
                    if (matrix.GetValue(point) == -1)
                    {
                        matrix.SetState(point, State.check, Situation.lose);
                        matrix.SetBombs(Bombs_remain, Situation.lose);
                    }
                    else
                        matrix.SetState(point, State.check, Situation.none);

                    if (matrix.GetValue(point) == 0)
                    {
                        List<Point> points = matrix.PointsAround(point);
                        foreach (Point p in points)
                            SemiClick(p);
                    }
                    break;
                case State.check:
                    if (matrix.GetValue(point) == 0)
                    {
                        List<Point> points = matrix.PointsAround(point);
                        foreach (Point p in points)
                            SemiClick(p);
                    }
                    break;
            }
        }
       
    }
}

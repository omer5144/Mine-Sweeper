namespace MineSweeper.model
{
    class BoardArgument
    {
        // Board Argument:
        // this class represent the argument passes from Game to Board

        public Cube[,] Cubes { get;} // the cubes in the game
        public Point Point { get; } // the point that pressed
        public Situation Situation { get; } // the current win/lose/none situation

        public BoardArgument(Cube[,] cubes, Point point, Situation situation)
        {
            Cubes = cubes;
            Point = point;
            Situation = situation;
        }
    }
}

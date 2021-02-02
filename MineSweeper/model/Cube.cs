namespace MineSweeper.model
{
    class Cube
    {
        // Cube:
        // this class represent a single cube in the game

        public State State { get; set; } // the flag/check/none state
        public int Value { get; set; } // the value (-1 if bomb)

        public Cube()
        {
            State = State.none;
            Value = 10;
        }
    }
}

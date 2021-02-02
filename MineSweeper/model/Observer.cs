namespace MineSweeper.model
{
    interface IObserver
    {
        // this interface represent an observer class
        
        void Update(Observable o, object arg);
    }
}

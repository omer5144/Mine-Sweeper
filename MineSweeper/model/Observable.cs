using System.Collections.Generic;

namespace MineSweeper.model
{
    class Observable
    {
        // this class represent an observable class

        private readonly List<IObserver> observers; // list of observers

        public Observable()
        {
            observers = new List<IObserver>();
        }
        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }
        public void NotifyObservers(object arg)
        {
            foreach (IObserver observer in observers)
                observer.Update(this, arg);
        }
    }
}

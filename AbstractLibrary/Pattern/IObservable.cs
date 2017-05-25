using System;
using System.Collections.Generic;

namespace AbstractLibrary.Pattern
{
    public interface IObservable
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }
}

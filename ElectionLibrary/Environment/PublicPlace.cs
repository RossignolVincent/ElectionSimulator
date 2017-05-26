using System;
using System.Collections.Generic;
using AbstractLibrary.Pattern;

namespace ElectionLibrary.Environment
{
    public class PublicPlace : AbstractElectionArea, IObservable
    {
        public readonly List<IObserver> Buildings;

        public PublicPlace(List<PoliticalParty> parties, string name, Position position) : base(parties, name, position)
        {
        }

        public void Attach(IObserver observer)
        {
            this.Buildings.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            this.Buildings.Remove(observer);
        }

        public void Notify()
        {
            foreach(Building building in this.Buildings)
            {
                building.Update();
            }
        }
    }
}

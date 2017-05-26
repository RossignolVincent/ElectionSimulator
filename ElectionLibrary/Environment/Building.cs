using System;
using System.Collections.Generic;
using AbstractLibrary.Pattern;

namespace ElectionLibrary.Environment
{
    public class Building : AbstractElectionArea, IObserver
    {
        public Building(List<PoliticalParty> parties, string name, Position position) : base(parties, name, position)
        {
        }

        public void Update()
        {
            
        }
    }
}

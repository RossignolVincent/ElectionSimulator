using System;
using System.Collections.Generic;
using AbstractLibrary.Pattern;

namespace ElectionSimulator.Environment
{
    public class Building : AbstractElectionArea, IObserver
    {
        public Building(List<PoliticalParty> parties, string name) : base(parties, name)
        {
        }

        public void Update()
        {
            
        }
    }
}

using System;
using System.Collections.Generic;
using AbstractLibrary.Pattern;

namespace ElectionLibrary.Environment
{
    public class Building : AbstractElectionArea, IObserver
    {
        public Building(Opinion opinion, string name, Position position) : base(opinion, name, position)
        {
        }

        public void Update()
        {

        }
    }
}

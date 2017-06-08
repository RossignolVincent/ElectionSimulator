﻿using ElectionLibrary.Character.Behavior;
using ElectionLibrary.Character.State;
using ElectionLibrary.Environment;
using ElectionLibrary.Parties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionLibrary.Character
{
    [Serializable]
    public class Activist : PoliticalCharacter
    {
        public Activist(string name, Position position, PoliticalParty politicalParty) : base(name, new ActivistBehavior(), position, politicalParty)
        {
        }

        public override void Tired()
        {
            Moral--;

            if (Moral <= 0)
            {
                State = new IsGoingBackToHQState();
            }
        }
    }
}

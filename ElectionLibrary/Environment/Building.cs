﻿using System;
using AbstractLibrary.Pattern;
using ElectionLibrary.Character;

namespace ElectionLibrary.Environment
{
    public class Building : AbstractElectionArea, IObserver
    {
        public Building(Opinion opinion, string name, Position position) : base(opinion, name, position)
        {
        }

        public void Update(object o)
        {
            if(o != null && o is PoliticalCharacter)
            {
                ChangeOpinion((PoliticalCharacter) o);
            }
        }

        public override void ChangeOpinion(PoliticalCharacter politician)
		{
			opinion.InfluenceOpinion(politician.PoliticalParty, politician.Aura, politician.Moral, 1);
        }
    }
}

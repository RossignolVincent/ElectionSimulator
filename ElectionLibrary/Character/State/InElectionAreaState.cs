﻿using System;
using ElectionLibrary.Environment;

namespace ElectionLibrary.Character.State
{
    [Serializable]
    public class InElectionAreaState : PoliticalCharacterState
    {
        public InElectionAreaState()
        {
        }

        public override Position Handle(ElectionCharacter character, AbstractArea area)
        {
            // If the politician is in an ElectionArea, influence opinion, go out and move to a Street Area
            PoliticalCharacter politician = (PoliticalCharacter)character;

            AbstractArea street = null;

			// Influence the opinion of the Election area
			((AbstractElectionArea) area).ChangeOpinion(politician);

            // Get the next area
            if(area.Accesses.Count == 1)
            {
                street = (AbstractArea) area.Accesses[0].EndArea;
            }
            else
            {
                street = GetRandomStreet(area);
			}

			politician.State = new InStreetState();

            // Get tired
            politician.Tired();
            return street.Position;
        }
    }
}

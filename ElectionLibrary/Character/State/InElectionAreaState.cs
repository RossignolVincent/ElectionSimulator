using System;
using ElectionLibrary.Environment;

namespace ElectionLibrary.Character.State
{
    public class InElectionAreaState : PoliticalCharacterState
    {
        public InElectionAreaState()
        {
        }

        public override Position Handle(PoliticalCharacter politician, AbstractArea area)
        {
            // If the politician is in an ElectionArea, influence opinion, go out and move to a Street Area

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
            return street.position;
        }
    }
}

using System;
using ElectionLibrary.Environment;

namespace ElectionLibrary.Character.State
{
    public class InStreetState : PoliticalCharacterState
    {
        public InStreetState()
        {
        }

        public override Position Handle(PoliticalCharacter politician, AbstractArea area)
        {
            // If the politician is in a Street area, try to go in a PublicPlace, or in a Building if there is no politician in there

			AbstractArea bestMove = area;

			foreach (ElectionAccess access in area.Accesses)
			{
				if (access.EndArea is PublicPlace
					&& !politician.HasBeenVisited((AbstractElectionArea) access.EndArea)
                    && access.EndArea.Characters.Count == 0)
				{
					bestMove = (AbstractArea) access.EndArea;
					break;
				}
				else if (access.EndArea is Building
                         && !politician.HasBeenVisited((AbstractElectionArea) access.EndArea)
                         && access.EndArea.Characters.Count == 0)
				{
					bestMove = (AbstractArea) access.EndArea;
				}
			}

            // No Building available to go in, go to a Street area
            if(bestMove == area)
            {
                bestMove = GetRandomStreet(area);
            } else
            {
                politician.VisitedElectionAreas.Add((AbstractElectionArea) area);
                politician.State = new InElectionAreaState();
            }

			return bestMove.position;
        }
    }
}

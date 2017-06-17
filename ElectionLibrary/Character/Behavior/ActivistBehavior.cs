using System;
using ElectionLibrary.Environment;
using ElectionLibrary.Character.State;

namespace ElectionLibrary.Character.Behavior
{
    [Serializable]
    public class ActivistBehavior : AbstractBehavior
    {
        public override Position Move(ElectionCharacter character, AbstractArea area)
        {
            PoliticalCharacter politician = (PoliticalCharacter)character;
            AbstractArea bestMove = area;

            foreach (ElectionAccess access in area.Accesses)
            {
                if (access.EndArea is PublicPlace
                    && !politician.VisitedElectionAreas.Contains((AbstractElectionArea)access.EndArea)
                    && access.EndArea.Characters.Count == 0)
                {
                    bestMove = (AbstractArea)access.EndArea;
                    break;
                }
                else if (access.EndArea is Building
                         && !politician.VisitedElectionAreas.Contains((AbstractElectionArea)access.EndArea)
                         && access.EndArea.Characters.Count == 0)
                {
                    bestMove = (AbstractArea)access.EndArea;
                }
            }

            // No Building available to go in, go to a Street area
            if (bestMove == area)
            {
                bestMove = GetNextStreet(area, character);
            }
            else
            {
                politician.VisitedElectionAreas.Enqueue((AbstractElectionArea)bestMove);
                if (politician.VisitedElectionAreas.Count > 15)
                {
                    politician.VisitedElectionAreas.Dequeue();
                }

                politician.State = new InElectionAreaState();
            }

            return bestMove.Position;
        }

        public override Position DoSomethingInHQ(ElectionCharacter character, AbstractArea area)
        {
            PoliticalCharacter politician = (PoliticalCharacter)character;

            if(politician.NbTurnToRest == 0)
            {
                politician.Rest();
                politician.State = new InStreetState();
                return GetNextStreet(area, character).Position;
            }
            else
            {
                politician.NbTurnToRest -= 10;
                return politician.Position;
            }
        }

        public override Position Meeting(ElectionCharacter character, AbstractArea area)
        {
            // If the politician is in an ElectionArea, influence opinion, go out and move to a Street Area
            PoliticalCharacter politician = (PoliticalCharacter)character;

            AbstractArea street = null;

            // Influence the opinion of the Election area
            ((AbstractElectionArea)area).ChangeOpinion(politician);

            // Get the next area
            if (area.Accesses.Count == 1)
            {
                street = (AbstractArea)area.Accesses[0].EndArea;
            }
            else
            {
                street = GetNextStreet(area, character);
            }

            politician.State = new InStreetState();

            // Get tired
            politician.Tired();
            return street.Position;
        }
    }
}

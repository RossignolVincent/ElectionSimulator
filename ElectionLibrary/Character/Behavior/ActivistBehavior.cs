using System;
using ElectionLibrary.Environment;
using ElectionLibrary.Character.State;
using System.Collections.Generic;
using ElectionLibrary.Object;

namespace ElectionLibrary.Character.Behavior
{
    [Serializable]
    public class ActivistBehavior : AbstractBehavior
    {
        public override Position Move(AbstractElectionCharacter character, AbstractArea area)
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

        public override Position DoSomethingInHQ(AbstractElectionCharacter character, AbstractArea area)
        {
            PoliticalCharacter politician = (PoliticalCharacter)character;

            if(politician.NbTurnToRest == 0)
            {
                politician.Rest();

                // Take posters from HQ
                List<Poster> newPosters = TakePostersFromHQ(politician);
                politician.AddObjects(newPosters);

                politician.State = new InStreetState();
                return GetNextStreet(area, character).Position;
            }
            else
            {
                politician.NbTurnToRest -= 10;
                return politician.Position;
            }
        }

        public override Position Meeting(AbstractElectionCharacter character, AbstractArea area)
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

        private List<Poster> TakePostersFromHQ(PoliticalCharacter character)
        {
            List<Poster> result;
            List<Poster> hqPosters = character.PoliticalParty.HQ.GetPosters();
            Random random = new Random();
            int pickedNumber = random.Next(hqPosters.Count);

            result = hqPosters.GetRange(0, pickedNumber);

            foreach(Poster poster in result)
            {
                character.PoliticalParty.HQ.RemoveObject(poster);
            }

            return result;
        }
    }
}

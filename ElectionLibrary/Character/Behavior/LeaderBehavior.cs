﻿using System;
using ElectionLibrary.Environment;
using ElectionLibrary.Character.State;

namespace ElectionLibrary.Character.Behavior
{
    [Serializable]
    public class LeaderBehavior : AbstractBehavior
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
    }
}

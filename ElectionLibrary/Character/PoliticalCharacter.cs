using ElectionLibrary.Character.Behavior;
using ElectionLibrary.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ElectionLibrary.Character
{
    public class PoliticalCharacter : ElectionCharacter
    {
        public PoliticalParty politicalParty;
        public List<AbstractElectionArea> visitedElectionAreas;
        public bool inBuilding;

        public PoliticalCharacter(string name, AbstractBehavior behavior, Position position, PoliticalParty politicalParty) : base(name, behavior, position)
        {
            this.politicalParty = politicalParty;
            this.visitedElectionAreas = new List<AbstractElectionArea>();
            this.inBuilding = false;
        }

        public override Position MoveDecision(AbstractArea area)
        {
            AbstractArea bestMove = area;

            if (!inBuilding)
            {
                foreach (ElectionAccess access in area.Accesses)
                {
                    if (access.EndArea is PublicPlace &&
                        this.HaveNotVisited((AbstractElectionArea) access.EndArea))
                    {
                        bestMove = (AbstractArea) access.EndArea;
                        this.EnterBuilding();
                        break;
                    }
                    else if (access.EndArea is Building &&
                        this.HaveNotVisited((AbstractElectionArea) access.EndArea))
                    {
                        bestMove = (AbstractArea)access.EndArea;
                        this.EnterBuilding();
                    }
                }
            }
            else
            {
                this.OutBuilding((AbstractElectionArea) bestMove);
            }

            if (bestMove == area)
            {
                Random random = new Random();
                int newStreet = random.Next(area.Accesses.Count);
                bestMove = (AbstractArea) area.Accesses[newStreet].EndArea;
            }

            return bestMove.position;
        }

        private bool HaveNotVisited(AbstractElectionArea area)
        {
            return visitedElectionAreas.Contains(area);
        }

        public bool IsInABuilding()
        {
            return this.inBuilding;
        }

        public void EnterBuilding()
        {
            inBuilding = true;
        }

        public void OutBuilding(AbstractElectionArea visitedArea)
        {
            visitedElectionAreas.Add(visitedArea);
            inBuilding = false;
        }
    }
}

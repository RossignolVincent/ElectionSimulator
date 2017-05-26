using ElectionLibrary.Character.Behavior;
using ElectionLibrary.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ElectionLibrary.Character
{
    public abstract class PoliticalCharacter : ElectionCharacter
    {
        public PoliticalParty PoliticalParty { get; }
        public List<AbstractElectionArea> visitedElectionAreas;
        public bool inBuilding;

        protected PoliticalCharacter(string name, AbstractBehavior behavior, Position position, PoliticalParty politicalParty) : base(name, behavior, position)
        {
            this.PoliticalParty = politicalParty;
            this.visitedElectionAreas = new List<AbstractElectionArea>();
            this.inBuilding = false;
        }

        public override Position MoveDecision(AbstractArea area)
        {
            AbstractArea bestMove = area;

            if (!IsInABuilding())
            {
                foreach (ElectionAccess access in area.Accesses)
                {
                    if (access.EndArea is PublicPlace &&
                        this.HasNotVisited((AbstractElectionArea) access.EndArea))
                    {
                        bestMove = (AbstractArea) access.EndArea;
                        break;
                    }
                    else if (access.EndArea is Building &&
                        this.HasNotVisited((AbstractElectionArea) access.EndArea))
                    {
                        bestMove = (AbstractArea)access.EndArea;
                    }
                }
            }
            else
            {
                this.OutBuilding();
            }

            if (bestMove == area)
            {
                Random random = new Random();
                List<AbstractArea> streets = GetStreets(area.Accesses);
                int newStreet = random.Next(streets.Count);
                bestMove = streets[newStreet];
            }
            else
            {
                EnterBuilding((AbstractElectionArea) bestMove);
            }

            return bestMove.position;
        }

        private List<AbstractArea> GetStreets(List<AbstractLibrary.Environment.AbstractAccess> list)
        {
            List<AbstractArea> streets = new List<AbstractArea>();
            foreach (AbstractLibrary.Environment.AbstractAccess access in list)
            {
                if (access.EndArea is Street)
                {
                    streets.Add((AbstractArea) access.EndArea);
                }
            }
            return streets;
        }

        private bool HasNotVisited(AbstractElectionArea area)
        {

            foreach (AbstractElectionArea visitedArea in visitedElectionAreas)
            {
                if (area.position.X == visitedArea.position.X &&
                    area.position.Y == visitedArea.position.Y)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsInABuilding()
        {
            return this.inBuilding;
        }

        public void EnterBuilding(AbstractElectionArea visitedArea)
        {
            visitedElectionAreas.Add(visitedArea);
            inBuilding = true;
        }

        public void OutBuilding()
        {
            inBuilding = false;
        }
    }
}

using System;
using AbstractLibrary.Character;
using ElectionLibrary.Environment;
using ElectionLibrary.Character.Behavior;

namespace ElectionLibrary.Character
{ 
    public abstract class ElectionCharacter : AbstractCharacter 
    {
        public AbstractBehavior behavior { get; set; }

        public Position position { get; set; }

        private int moral;
        public int Moral 
        {
            get { return moral; }
            set
            {
                if (value < 0)
                {
                    moral = 0;
                }
                moral = value;
            }
        }

        public bool inBuilding;

        protected ElectionCharacter(string name, AbstractBehavior behavior, Position position) : base(name)
        {
            this.behavior = behavior;
            this.position = position;
            this.moral = 100;
            this.inBuilding = false;
        }

        public void NextTurn(AbstractArea area)
        {
            // OBJECT HERE


            // MOVE
            this.MoveTo(MoveDecision(area));
        }

        private Position MoveDecision(AbstractArea area)
        {
            AbstractArea bestMove = area;

            foreach (ElectionAccess access in area.Accesses)
            {
                if (access.EndArea is PublicPlace)
                {
                    bestMove = (AbstractArea)access.EndArea;
                    break;
                }
                else if (access.EndArea is Building)
                {
                    bestMove = (AbstractArea)access.EndArea;
                }
            }
            if (bestMove == area)
            {
                Random random = new Random();
                int newStreet = random.Next(area.Accesses.Count);
                bestMove = (AbstractArea)area.Accesses[newStreet].EndArea;
            }

            return bestMove.position;
        }

        public bool IsInABuilding()
        {
            return this.inBuilding;
        }

        public void EnterBuilding()
        {
            if (!this.inBuilding)
                inBuilding = true;
        }

        public void OutBuilding()
        {
            if (this.inBuilding)
                inBuilding = false;
        }

        public void MoveTo(Position position)
        {
            this.position = position;
        }
    }
}

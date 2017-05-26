using System;
using AbstractLibrary.Factory;
using AbstractLibrary.Character;
using ElectionLibrary.Environment;
using ElectionLibrary.Character.Behavior;

namespace ElectionLibrary.Character
{ 
    public abstract class ElectionCharacter : AbstractCharacter 
    {
        private AbstractBehavior behavior { get; set; }

        private Position position { get; set; }

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

        private bool inBuilding;

        public ElectionCharacter(string name, AbstractBehavior behavior, Position position, int moral) : base(name)
        {
            this.behavior = behavior;
            this.position = position;
            this.moral = moral;
            this.inBuilding = false;
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

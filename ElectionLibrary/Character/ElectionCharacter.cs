﻿﻿using System;
using AbstractLibrary.Character;
using ElectionLibrary.Environment;
using ElectionLibrary.Character.Behavior;

namespace ElectionLibrary.Character
{ 
    public abstract class ElectionCharacter : AbstractCharacter 
    {
        public AbstractBehavior Behavior { get; set; }

        public Position position { get; set; }

        public int Aura { get; set; }

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

        protected ElectionCharacter(string name, AbstractBehavior behavior, Position position) : base(name)
        {
            this.Behavior = behavior;
            this.position = position;
            this.moral = 100;   
        }

        public void NextTurn(AbstractArea area)
        {
            // OBJECT HERE


            // MOVE
            this.MoveTo(MoveDecision(area));
        }

        public abstract Position MoveDecision(AbstractArea area);

        public void MoveTo(Position position)
        {
            this.position = position;
        }
    }
}

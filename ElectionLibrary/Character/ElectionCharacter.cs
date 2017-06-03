﻿using System;
using AbstractLibrary.Character;
using ElectionLibrary.Character.Behavior;
using ElectionLibrary.Environment;
using System.Collections.Generic;

namespace ElectionLibrary.Character
{ 
    public abstract class ElectionCharacter : AbstractCharacter 
    {
        public static int INIT_MORAL = 25;

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
            this.moral = INIT_MORAL;
            this.Aura = 2;
        }

        public void NextTurn(AbstractArea area, List<List<AbstractArea>> areas)
        {
            // OBJECT HERE

            // MOVE
            this.MoveTo(MoveDecision(area, areas));
        }

        public abstract Position MoveDecision(AbstractArea area, List<List<AbstractArea>> areas);

        public void MoveTo(Position position)
        {
            this.position = position;
        }

        public abstract void Rest();

        public abstract void Tired();
    }
}

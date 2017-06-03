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
            if(area.Objects.Count > 0)
            {

            }

            // CHARACTERS INTERACTION
            if (area.Characters.Count > 1)
            {
                ComputeCharactersInteraction(area.Characters);
            }

            // MOVE
            this.MoveTo(MoveDecision(area, areas));
        }

        public void MoveTo(Position position)
        {
            this.position = position;
        }

        protected abstract void ComputeCharactersInteraction(List<AbstractCharacter> characters);

        public abstract Position MoveDecision(AbstractArea area, List<List<AbstractArea>> areas);

        public abstract void Rest();

        public abstract void Tired();
    }
}

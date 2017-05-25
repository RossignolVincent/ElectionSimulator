using System;
using AbstractLibrary.Factory;
using AbstractLibrary.Character;

namespace ElectionLibrary.Character
{ 
    public abstract class ElectionCharacter : AbstractCharacter 
    {
        private Behavior behavior { get; set; }

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

        public ElectionCharacter(string name, Behavior behavior, int moral) : base(name)
        {
            this.behavior = behavior;
            this.moral = moral;
        }
    }
}

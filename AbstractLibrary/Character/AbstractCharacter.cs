using System;
using AbstractLibrary.Factory;
using AbstractLibrary.Environment;

namespace AbstractLibrary.Character
{
    public abstract class AbstractCharacter : IDomain
    {
        public string Name { get; set; }
        public AbstractArea Position { get; set; }

        protected AbstractCharacter(string name)
        {
            this.Name = name;
        }
    }
}

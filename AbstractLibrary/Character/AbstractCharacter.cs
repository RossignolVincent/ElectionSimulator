using System;
using AbstractLibrary.Factory;
using AbstractLibrary.Environment;

namespace AbstractLibrary.Character
{
    public abstract class AbstractCharacter
    {
        public string Name { get; private set; }

        protected AbstractCharacter(string name)
        {
            this.Name = name;
        }
    }
}

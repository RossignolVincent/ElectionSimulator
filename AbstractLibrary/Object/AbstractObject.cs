using System;
using AbstractLibrary.Factory;
using AbstractLibrary.Environment;

namespace AbstractLibrary.Object
{
    public abstract class AbstractObject
    {
		public string Name { get; private set; }

        protected AbstractObject(string name)
        {
            this.Name = name;
        }
    }
}

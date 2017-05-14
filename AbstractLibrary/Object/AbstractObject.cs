using System;
using AbstractLibrary.Factory;

namespace AbstractLibrary.Object
{
    public abstract class AbstractObject
    {
		public string Name { get; private set; }
		public AbstractArea Position { get; set; }

        private AbstractObject(string name)
        {
            this.Name = name;
        }
    }
}

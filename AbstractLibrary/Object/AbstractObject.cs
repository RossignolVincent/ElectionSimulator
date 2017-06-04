using System;
using AbstractLibrary.Factory;
using AbstractLibrary.Environment;

namespace AbstractLibrary.Object
{
    public abstract class AbstractObject : IDomain
    {
		public string Name { get; set; }
		public AbstractArea Position { get; set; }

        protected AbstractObject(string name)
        {
            this.Name = name;
        }
    }
}

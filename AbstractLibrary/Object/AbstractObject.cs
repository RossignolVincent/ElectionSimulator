using System;
using AbstractLibrary.Factory;
using AbstractLibrary.Environment;

namespace AbstractLibrary.Object
{
    [Serializable]
    public abstract class AbstractObject : IDomain
    {
		public string Name { get; set; }

        protected AbstractObject(string name)
        {
            this.Name = name;
        }
    }
}

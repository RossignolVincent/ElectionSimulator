using System;

namespace AbstractLibrary.Object
{
    [Serializable]
    public abstract class AbstractObject
    {
		public string Name { get; set; }

        protected AbstractObject(string name)
        {
            this.Name = name;
        }
    }
}

using System;

namespace ElectionLibrary.Environment
{
    public abstract class AbstractArea : AbstractLibrary.Environment.AbstractArea
    {
        public Position position { get;}

        protected AbstractArea(string name, Position position) : base(name)
        {
            this.position = position;
        }
    }
}

using System;
namespace AbstractLibrary.Environment
{
    public abstract class AbstractAccess
    {
        public AbstractArea FirstArea { get; }
        public AbstractArea EndArea { get; }

        protected AbstractAccess(AbstractArea firstArea, AbstractArea endArea)
        {
            this.FirstArea = firstArea;
            this.EndArea = endArea;
        }
    }
}

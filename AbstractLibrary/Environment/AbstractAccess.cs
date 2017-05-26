using System;
namespace AbstractLibrary.Environment
{
    public abstract class AbstractAccess
    {
        public AbstractArea FirstArea { get; }
        public AbstractArea EndArea { get; }

        public AbstractAccess(AbstractArea firstArea, AbstractArea endArea)
        {
            this.FirstArea = firstArea;
            this.EndArea = endArea;
        }
    }
}

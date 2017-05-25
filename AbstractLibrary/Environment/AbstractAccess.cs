using System;
namespace AbstractLibrary.Environment
{
    public abstract class AbstractAccess
    {
        private AbstractArea FirstArea { get; set; }
        private AbstractArea EndArea { get; set; }

        private AbstractAccess(AbstractArea firstArea, AbstractArea endArea)
        {
            this.FirstArea = firstArea;
            this.EndArea = endArea;
        }
    }
}

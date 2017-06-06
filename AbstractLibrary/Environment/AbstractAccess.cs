using System;
namespace AbstractLibrary.Environment
{
    [Serializable]
    public abstract class AbstractAccess : IDomain
    {
        public AbstractArea FirstArea { get; set;  }
        public AbstractArea EndArea { get; set; }

        protected AbstractAccess(AbstractArea firstArea, AbstractArea endArea)
        {
            this.FirstArea = firstArea;
            this.EndArea = endArea;
        }
    }
}

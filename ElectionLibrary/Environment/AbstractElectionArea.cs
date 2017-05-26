using System;
using System.Collections.Generic;

namespace ElectionLibrary.Environment
{
    public abstract class AbstractElectionArea : AbstractArea
    {
        private Opinion opinion { get; set; }

        public AbstractElectionArea(Opinion opinion, string name, Position position) : base(name, position)
        {
            this.opinion = opinion;
        }
    }
}

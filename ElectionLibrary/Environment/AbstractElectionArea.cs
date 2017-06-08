﻿using System;
using System.Collections.Generic;
using ElectionLibrary.Character;
using AbstractLibrary.Object;

namespace ElectionLibrary.Environment
{
    [Serializable]
    public abstract class AbstractElectionArea : AbstractArea
    {
        public Opinion opinion { get; }

        protected AbstractElectionArea(Opinion opinion, string name, Position position) : base(name, position)
        {
            this.opinion = opinion;
        }

        public abstract void ChangeOpinion(PoliticalCharacter politician);
    }
}

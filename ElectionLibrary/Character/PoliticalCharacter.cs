﻿﻿﻿using ElectionLibrary.Character.Behavior;
using ElectionLibrary.Environment;
using System;
using System.Collections.Generic;
using ElectionLibrary.Algorithm;
using ElectionLibrary.Character.State;
using ElectionLibrary.Parties;

namespace ElectionLibrary.Character
{
    public abstract class PoliticalCharacter : ElectionCharacter
    {
		public PoliticalParty PoliticalParty { get; }
		public PoliticalCharacterState State { get; set; }
        public Queue<AbstractElectionArea> VisitedElectionAreas { get; }
        public Stack<Position> PathToHQ { get; set; }
        private List<List<AbstractArea>> areas;

        protected PoliticalCharacter(string name, AbstractBehavior behavior, Position position, PoliticalParty politicalParty) : base(name, behavior, position)
        {
            PoliticalParty = politicalParty;
            VisitedElectionAreas = new Queue<AbstractElectionArea>();
            State = new InHQState();
            PathToHQ = null;
        }

        public override Position MoveDecision(AbstractArea area, List<List<AbstractArea>> areas)
        {
            if (this.areas == null)
                this.areas = areas;

            return State.Handle(this, area);
        }

		public bool HasBeenVisited(AbstractElectionArea area)
		{
			foreach (AbstractElectionArea visitedArea in VisitedElectionAreas)
			{
				if (area.position.X == visitedArea.position.X
					&& area.position.Y == visitedArea.position.Y)
				{
					return true;
				}
			}

			return false;
		}

        public void SetPathToHQ()
        {
			AStar aStar = new AStar(position, PoliticalParty.HQ.position, areas);
			aStar.Compute();
			PathToHQ = aStar.GetPath();
        }

        public override void Rest()
        {
            Moral = INIT_MORAL;
        }

		public override void Tired()
		{
			Moral--;

			if (Moral <= 0)
			{
                State = new IsGoingBackToHQState();
			}
		}
    }
}

﻿using ElectionLibrary.Character.Behavior;
using ElectionLibrary.Environment;
using System;
using System.Collections.Generic;
using ElectionLibrary.Algorithm;
using ElectionLibrary.Character.State;

namespace ElectionLibrary.Character
{
    public abstract class PoliticalCharacter : ElectionCharacter
    {
		public PoliticalParty PoliticalParty { get; }
		public PoliticalCharacterState State { get; set; }
        public List<AbstractElectionArea> VisitedElectionAreas { get; }
        public Stack<Position> PathToHQ { get; set; }

        protected PoliticalCharacter(string name, AbstractBehavior behavior, Position position, PoliticalParty politicalParty) : base(name, behavior, position)
        {
            PoliticalParty = politicalParty;
            VisitedElectionAreas = new List<AbstractElectionArea>();
            State = new InHQState();
            PathToHQ = null;
        }

        public override Position MoveDecision(AbstractArea area)
        {
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

        public Stack<Position> GetPathToHQ(Position HQPosition, List<List<AbstractArea>> grid)
        {
			AStar aStar = new AStar(position, HQPosition, grid);
			aStar.Compute();
			PathToHQ = aStar.GetPath();

			State = new IsGoingBackToHQState();

            return PathToHQ;
        }
    }
}

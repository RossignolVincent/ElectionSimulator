﻿﻿﻿using ElectionLibrary.Character.Behavior;
using ElectionLibrary.Environment;
using System;
using System.Collections.Generic;
using ElectionLibrary.Algorithm;
using ElectionLibrary.Character.State;
using ElectionLibrary.Parties;
using AbstractLibrary.Character;

namespace ElectionLibrary.Character
{
    [Serializable]
    public abstract class PoliticalCharacter : ElectionCharacter
    {
		public PoliticalParty PoliticalParty { get; }
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

        public void SetPathToHQ()
        {
			AStar aStar = new AStar(Position, PoliticalParty.HQ.Position, areas);
			aStar.Compute();
			PathToHQ = aStar.GetPath();
        }

        protected override void ComputeCharactersInteraction(List<AbstractCharacter> characters)
        {
            List<PoliticalCharacter> politicians = new List<PoliticalCharacter>();
            List<Journalist> journalists = new List<Journalist>();
            Random random = new Random();

            foreach (ElectionCharacter character in characters)
            {
                if (character != this)
                {
                    if (character is PoliticalCharacter && ((PoliticalCharacter)character).PoliticalParty != PoliticalParty)
                    {
                        politicians.Add((PoliticalCharacter)character);
                    }
                    else if (character is Journalist)
                    {
                        journalists.Add((Journalist)character);
                    }
                }
            }

            // Priority to Political Debate
            if(politicians.Count > 0)
            {
                int pickedNumber = random.Next(politicians.Count);
                Debate(politicians[pickedNumber]);
            }
            else if(journalists.Count > 0) // Then Interview
            {
                int pickedNumber = random.Next(journalists.Count);
                journalists[pickedNumber].Interview(this);
            }
        }

        protected void Debate(PoliticalCharacter opponent)
        {
            Random random = new Random();
            PoliticalCharacter winner = null;
            PoliticalCharacter looser = null;
            PoliticalCharacter moreAura = (Aura > opponent.Aura) ? this : opponent;

            int diffAura = Math.Abs(Aura - opponent.Aura);
            int diffMoral = (int) ((double)Moral / INIT_MORAL - (double)opponent.Moral / INIT_MORAL) * 100 / 2;

            // The politician with more Aura could try multiple times to win
            for (int i = 0; i < diffAura + 1; i++)
            {
                int pickedNumber = random.Next(100);
                winner = (pickedNumber < 50 + diffMoral) ? this : opponent;
                
                if (winner == moreAura)
                {
                    break;
                }
            }

            diffMoral = Math.Abs(diffMoral) / 10;
            looser = (winner == this) ? opponent : this;

            // Update moral
            winner.Moral += (1 + diffMoral);
            looser.Moral -= (1 + diffMoral);

            //Console.WriteLine("Débat : " + winner.PoliticalParty.Name + " bat (" + (1 + diffMoral) + ")" + looser.PoliticalParty.Name);

            //winner.AddRandomAura();
        }

        public override void Rest()
        {
            Moral = INIT_MORAL;
        }
    }
}

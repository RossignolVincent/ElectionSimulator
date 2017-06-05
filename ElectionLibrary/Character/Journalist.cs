using ElectionLibrary.Character.Behavior;
using ElectionLibrary.Environment;
using System;
using System.Collections.Generic;
using AbstractLibrary.Character;
using ElectionLibrary.Character.State;

namespace ElectionLibrary.Character
{
    public class Journalist : ElectionCharacter
    {
        public Journalist(string name, Position position) : base(name, new JournalistBehavior(), position)
        {
            this.State = new InStreetState();
        }

        public override Position MoveDecision(AbstractArea area, List<List<AbstractArea>> areas)
        {
            return State.Handle(this, area);
        }
    
        public override void Rest()
        {
            throw new NotImplementedException();
        }

        public override void Tired()
        {
            throw new NotImplementedException();
        }

        protected override void ComputeCharactersInteraction(List<AbstractCharacter> characters)
        {
            List<PoliticalCharacter> politicians = new List<PoliticalCharacter>();
            Random random = new Random();

            foreach (ElectionCharacter character in characters)
            {
                if (character != this && character is PoliticalCharacter)
                {
                    politicians.Add((PoliticalCharacter)character);
                }
            }

            // Priority to Political Debate
            if (politicians.Count > 0)
            {
                int pickedNumber = random.Next(politicians.Count);
                Interview(politicians[pickedNumber]);
            }
        }

        public void Interview(PoliticalCharacter politician)
        {
            int diffMoralPercentage = (int) ((double)politician.Moral / INIT_MORAL - (double)Moral / INIT_MORAL) * 100 / 2;
            int diffAura = politician.Aura - Aura;
            Random random = new Random();
            int pickedNumber = random.Next(100);
            
            if (pickedNumber < 50 + diffAura * 10 + diffMoralPercentage / 2)
            {
                // The politician wins
                politician.Moral += (1 + diffMoralPercentage / 10);
                Moral -= (1 + diffMoralPercentage / 10);
                //politician.AddRandomAura();
            }
            else
            {
                // The journalist wins
                Moral += (1 + diffMoralPercentage / 10);
                politician.Moral -= (1 + diffMoralPercentage / 10);
                //AddRandomAura();
            }
        }
    }
}

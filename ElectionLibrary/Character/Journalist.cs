using ElectionLibrary.Character.Behavior;
using ElectionLibrary.Environment;
using System.Collections.Generic;
using AbstractLibrary.Character;
using ElectionLibrary.Character.State;
using AbstractLibrary.Pattern;
using ElectionLibrary.Event;

namespace ElectionLibrary.Character
{
    [System.Serializable]
    public class Journalist : AbstractElectionCharacter, IObservable<ElectionEvent>
    {
        private readonly List<IObserver<ElectionEvent>> medias;
        private System.Random random;

        public Journalist(string name, Position position) : base(name, new JournalistBehavior(), position)
        {
            this.State = new InStreetState();
            medias = new List<IObserver<ElectionEvent>>();
            random = new System.Random();
        }

        public override Position MoveDecision(AbstractArea area, List<List<AbstractArea>> areas)
        {
            return State.Handle(this, area);
        }
    
        public override void Rest()
        {
        }

        public override void Tired()
        {
        }

        protected override void ComputeCharactersInteraction(List<AbstractCharacter> characters)
        {
            List<PoliticalCharacter> politicians = new List<PoliticalCharacter>();

            foreach (AbstractElectionCharacter character in characters)
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
        
        protected override void ComputeObjectsInteraction(Street area)
        {
        }

        public void Interview(PoliticalCharacter politician)
        {
            int diffMoralPercentage = (int) ((double)politician.Moral / INIT_MORAL - (double)Moral / INIT_MORAL) * 100 / 2;
            int diffAura = politician.Aura - Aura;
            int pickedNumber = random.Next(100);
            Article article;
            
            if (pickedNumber < 50 + diffAura * 10 + diffMoralPercentage / 2)
            {
                // The politician wins
                politician.Moral += (1 + diffMoralPercentage / 10);
                Moral -= (1 + diffMoralPercentage / 10);
                //politician.AddRandomAura();
                article = new Article(politician.PoliticalParty, true);
            }
            else
            {
                // The journalist wins
                Moral += (1 + diffMoralPercentage / 10);
                politician.Moral -= (1 + diffMoralPercentage / 10);
                //AddRandomAura();
                article = new Article(politician.PoliticalParty, false);
            }

            Notify(article);
        }

        public void Attach(IObserver<ElectionEvent> observer)
        {
            medias.Add(observer);
        }

        public void Detach(IObserver<ElectionEvent> observer)
        {
            medias.Remove(observer);
        }

        public void Notify(ElectionEvent article)
        {
            foreach(IObserver<ElectionEvent> observer in medias)
            {
                observer.Update(article);
            }
        }
    }
}

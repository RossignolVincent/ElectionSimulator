using System.Collections.Generic;
using AbstractLibrary.Pattern;
using ElectionLibrary.Character;

namespace ElectionLibrary.Environment
{
    [System.Serializable]
    public class PublicPlace : AbstractElectionArea, IObservable<PoliticalCharacter>
    {
        public readonly List<IObserver<PoliticalCharacter>> Buildings;

        private PoliticalCharacter LastPolitician { get; set; }

        public PublicPlace(Opinion opinion, string name, Position position) : base(opinion, name, position)
        {
            Buildings = new List<IObserver<PoliticalCharacter>>();
        }

        public void Attach(IObserver<PoliticalCharacter> observer)
        {
            Buildings.Add(observer);
        }

        public void Detach(IObserver<PoliticalCharacter> observer)
        {
            Buildings.Remove(observer);
        }

        public void Notify(PoliticalCharacter politician)
        {
            foreach(IObserver<PoliticalCharacter> observer in Buildings)
            {
                observer.Update(politician);
            }
        }

        public override void ChangeOpinion(PoliticalCharacter politician)
        {
            opinion.InfluenceOpinion(politician.PoliticalParty, politician.Aura, politician.Moral, 1);
            Notify(politician);
        }
    }
}

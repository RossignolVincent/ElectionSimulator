using AbstractLibrary.Pattern;
using ElectionLibrary.Character;

namespace ElectionLibrary.Environment
{
    public class Building : AbstractElectionArea, IObserver<PoliticalCharacter>
    {
        public Building(Opinion opinion, string name, Position position) : base(opinion, name, position)
        {
        }

        public void Update(PoliticalCharacter politician)
        {
            ChangeOpinion(politician);
        }

        public override void ChangeOpinion(PoliticalCharacter politician)
		{
			opinion.InfluenceOpinion(politician.PoliticalParty, politician.Aura, politician.Moral, 1);
        }
    }
}

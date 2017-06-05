using ElectionLibrary.Parties;

namespace ElectionLibrary.Event
{
    public class Article : ElectionEvent
    {
        public PoliticalParty Party { get; set; }

        public bool IsPositive { get; set; }

        public Article(PoliticalParty party, bool isPositive)
        {
            Party = party;
            IsPositive = isPositive;
        }
    }
}

using ElectionLibrary.Environment;
using ElectionLibrary.Parties;

namespace ElectionLibrary.Object
{
    public class Poster : AbstractElectionObject
    {
        public PoliticalParty Party { get; set; }

        public Poster(string name, Position position, PoliticalParty party) : base(name, position)
        {
            Party = party;
        }
    }
}

using ElectionLibrary.Parties;
using System;
namespace ElectionLibrary.Environment
{
    public class HQ : AbstractArea
    {
        public PoliticalParty Party { get; set; }

        public HQ(string name, Position position, PoliticalParty party) : base(name, position)
        {
            Party = party;
        }
    }
}

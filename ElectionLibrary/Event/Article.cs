using ElectionLibrary.Parties;
using System;

namespace ElectionLibrary.Event
{
    [Serializable]
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

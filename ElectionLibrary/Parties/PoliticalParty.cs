using System;
namespace ElectionLibrary.Parties
{
    public abstract class PoliticalParty
    {
        public static string name { get; set; }
        protected PoliticalParty instance;

        protected PoliticalParty()
        {
        }

        public abstract PoliticalParty Instance();
    }
}

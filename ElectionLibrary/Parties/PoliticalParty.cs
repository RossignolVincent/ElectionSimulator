using ElectionLibrary.Environment;
using System;
namespace ElectionLibrary.Parties
{
    public abstract class PoliticalParty
    {
        public string Name { get; set; }
        public HQ HQ { get; set; }

        protected PoliticalParty(HQ hq)
        {
            HQ = hq;
        }
    }
}

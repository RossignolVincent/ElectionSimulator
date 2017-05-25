using System;
using System.Collections.Generic;

namespace ElectionSimulator.Environment
{
    public abstract class AbstractElectionArea : AbstractArea
    {
        private Dictionary<PoliticalParty, double> OpinionList { get; set; }

        public AbstractElectionArea(List<PoliticalParty> parties, string name) : base(name)
        {
            this.OpinionList = new Dictionary<PoliticalParty, double>();
            foreach(PoliticalParty party in parties) {
                this.OpinionList.Add(party, 100/parties.Count);
            }
        }
    }
}

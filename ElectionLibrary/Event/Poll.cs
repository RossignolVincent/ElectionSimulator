using ElectionLibrary.Environment;
using ElectionLibrary.Parties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionLibrary.Event
{
    public class Poll : ElectionEvent
    {
        public Opinion Result { get; set; }

        public enum PollType
        {
            Poll,
            End
        }

        public PollType Type { get; set; }

        public Poll(PollType type)
        {
            Type = type;
        }

        public void GenerateResult(List<Opinion> opinions)
        {
            int countOpininons = opinions.Count;

            Opinion result = new Opinion(opinions[0].GetParties());

            foreach (PoliticalParty party in result.GetParties())
            {
                result.opinionList[party] = 0;
            }

            for (int i = 0; i < countOpininons; i++)
            {
                Dictionary<PoliticalParty, double> partiesOpinions = opinions[i].GetPartiesOpinions();
                foreach (PoliticalParty party in partiesOpinions.Keys)
                {
                    result.opinionList[party] = result.opinionList[party] + partiesOpinions[party];
                }
            }

            foreach (PoliticalParty party in result.GetParties())
            {
                result.opinionList[party] = result.opinionList[party] / countOpininons;
            }

            Result = result;
        }
    }
}

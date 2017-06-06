using ElectionLibrary.Parties;
using System;
using System.Collections.Generic;

namespace ElectionLibrary.Environment
{
    [Serializable]
    public class Opinion
    {
        public Dictionary<PoliticalParty, double> opinionList;

        private static readonly Random random = new Random();

        public Opinion(List<PoliticalParty> parties)
        {
            opinionList = new Dictionary<PoliticalParty, double>();
            foreach (PoliticalParty party in parties)
            {
                opinionList.Add(party, 100 / parties.Count);
            }
        }

        public Dictionary<PoliticalParty, double> GetPartiesOpinions()
        {
            return opinionList;
        }

        public List<PoliticalParty> GetParties()
        {
            List<PoliticalParty> parties = new List<PoliticalParty>();

            foreach (PoliticalParty party in opinionList.Keys)
            {
                parties.Add(party);
            }

            return parties;
        }

        public double InfluenceOpinion(PoliticalParty party, int aura, int moral, int nbTurn)
        {
            if (opinionList.ContainsKey(party))
            {
                double calcul = 10.0; //TODO : Calcule party.opinion +*/- aura +*/- moral +*/- nbTurn
				UpdateNewOpinion(party, calcul);

                return calcul;
            }

            return -1;
        }

        public void UpdateNewOpinion(PoliticalParty party, double opinionToAdd)
        {   
            // Compute new opinion for the party of the politician
            double influencePartyOpinion = opinionList[party] + opinionToAdd;
			opinionList[party] = influencePartyOpinion;

            // Compute new opinion for the others parties
			List<PoliticalParty> concurrents = GetConcurrentParties(party);
            List<double> newConcurrentsOpinions = GetNewConcurrentsOpinions(concurrents.Count, opinionToAdd);

			foreach (PoliticalParty concurrent in concurrents)
			{
				opinionList[concurrent] = opinionList[concurrent] - GetRandomNumberFromList(newConcurrentsOpinions);
			}
        }

        private double GetRandomNumberFromList(List<double> numbers)
        {
            double choosenNumber = numbers[random.Next(numbers.Count)];
            numbers.Remove(choosenNumber);

            return choosenNumber;
        }

        private List<double> GetNewConcurrentsOpinions(int nbConcurrents, double calcul)
        {
            int maxRange = (int) calcul;
            int totalOpinions = 0;
            List<double> newDecreasedOpinions = new List<double>();

            for (int i = 0; i < nbConcurrents - 1; i++)
            {
                int nb = random.Next(0, maxRange);
                newDecreasedOpinions.Add(nb);
                maxRange = maxRange - nb;
                totalOpinions += nb;
            }
            newDecreasedOpinions.Add(calcul - totalOpinions);

            return newDecreasedOpinions;
        }

        private List<PoliticalParty> GetConcurrentParties(PoliticalParty party)
        {
            List<PoliticalParty> listConcurrent = new List<PoliticalParty>();
            foreach (KeyValuePair<PoliticalParty, double> entry in opinionList)
            {
                if (entry.Key != party)
                {
                    listConcurrent.Add(entry.Key);
                }
            }

            return listConcurrent;
        }
    }
}
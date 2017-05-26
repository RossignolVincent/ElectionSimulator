using System;
using System.Collections.Generic;

namespace ElectionLibrary.Environment
{
    public class Opinion
    {
        private readonly Dictionary<PoliticalParty, double> opinionList;

        private static readonly Random random = new Random();

        public Opinion(List<PoliticalParty> parties)
        {
            opinionList = new Dictionary<PoliticalParty, double>();
            foreach (PoliticalParty party in parties)
            {
                opinionList.Add(party, 100 / parties.Count);
            }
        }

        public void InfluenceOpinion(PoliticalParty party, int aura, int moral, int nbTurn)
        {
            double currentPartyOpinion = 0;
            
            if (opinionList.ContainsKey(party))
                currentPartyOpinion = opinionList[party];

            double calcul = 10.0; //TODO : Calcule party.opinion +*/- aura +*/- moral +*/- nbTurn;
            double influencePartyOpinion = currentPartyOpinion + calcul;

            opinionList[party] = influencePartyOpinion;

            List<PoliticalParty> concurrents = getConcurrentParties(party);
            List<double> newConcurrentsOpinions = getNewConcurrentsOpinions(concurrents.Count, calcul);
            
            foreach(PoliticalParty concurrent in concurrents)
            {
                opinionList[concurrent] = getRandomNumberFromList(newConcurrentsOpinions);
            }
        }

        private double getRandomNumberFromList(List<double> numbers)
        {
            double choosenNumber = numbers[random.Next(numbers.Count)];
            numbers.Remove(choosenNumber);

            return choosenNumber;
        }

        private List<double> getNewConcurrentsOpinions(int nbConcurrents, double calcul)
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

        private List<PoliticalParty> getConcurrentParties(PoliticalParty party)
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

using AbstractLibrary.Pattern;
using ElectionLibrary.Character;
using ElectionLibrary.Environment;
using ElectionLibrary.Parties;
using System.Collections.Generic;

namespace ElectionLibrary.Event
{
    public class Media : IObserver<ElectionEvent>
    {
        private static Media media;

        private readonly List<AbstractElectionCharacter> characters;
        private System.Random random;
        private Opinion lastPollResult;

        public Media(List<AbstractElectionCharacter> characters)
        {
            this.characters = characters;
            random = new System.Random();
            lastPollResult = null;

            RegisterAllJournalistsToMedia();
        }

        public static Media GetInstance(List<AbstractElectionCharacter> characters)
        {
            if(media == null)
            {
                media = new Media(characters);
            }

            return media;
        }

        private void RegisterAllJournalistsToMedia()
        {
            foreach(AbstractElectionCharacter character in characters)
            {
                if(character is Journalist journalist)
                {
                    journalist.Attach(this);
                }
            }
        }

        public void Update(ElectionEvent electionEvent)
        {
            foreach (AbstractElectionCharacter character in characters)
            {
                if(character is Journalist)
                {
                    continue;
                }

                PoliticalCharacter politician = (PoliticalCharacter)character;
                int pickedNumber = random.Next(3) + 1;

                if (electionEvent is Article article)
                {
                    // Update Moral's politician characters of the party described in the article
                    if (politician.PoliticalParty == article.Party)
                    {
                        politician.Moral += (article.IsPositive) ? pickedNumber : -pickedNumber;
                    }
                }
                else if(electionEvent is Poll poll)
                {
                    // Influence Moral's characters depending on the result of the poll
                    Opinion result = poll.Result;
                    Dictionary<PoliticalParty, double> resultOpinion = result.GetPartiesOpinions();
                    double refValue = (lastPollResult == null) ? 100 / result.GetParties().Count : lastPollResult.GetPartiesOpinions()[politician.PoliticalParty];
                    
                    politician.Moral += (resultOpinion[politician.PoliticalParty] >= refValue) ? pickedNumber : -pickedNumber;
                    
                    lastPollResult = result;
                }
            }
        }
    }
}


using AbstractLibrary.Pattern;
using ElectionLibrary.Character;
using System.Collections.Generic;

namespace ElectionLibrary.Event
{
    public class Media : IObserver<Article>
    {
        private static Media media;

        private readonly List<ElectionCharacter> characters;
        private System.Random random;

        public Media(List<ElectionCharacter> characters)
        {
            this.characters = characters;
            random = new System.Random();

            RegisterAllJournalistsToMedia();
        }

        public static Media GetInstance(List<ElectionCharacter> characters)
        {
            if(media == null)
            {
                media = new Media(characters);
            }

            return media;
        }

        private void RegisterAllJournalistsToMedia()
        {
            foreach(ElectionCharacter character in characters)
            {
                if(character is Journalist journalist)
                {
                    journalist.Attach(this);
                }
            }
        }

        public void Update(Article article)
        {
            // Update politician characters Moral of the party described in the article
            foreach(ElectionCharacter character in characters)
            {
                if(character is PoliticalCharacter politicalCharacter && politicalCharacter.PoliticalParty == article.Party)
                {
                    int pickedNumber = random.Next(3) + 1;

                    politicalCharacter.Moral += (article.IsPositive) ? pickedNumber : -pickedNumber ;
                }
            }
        }
    }
}

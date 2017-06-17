using AbstractLibrary.Object;
using ElectionLibrary.Character.Behavior;
using ElectionLibrary.Character.State;
using ElectionLibrary.Environment;
using ElectionLibrary.Object;
using ElectionLibrary.Parties;
using System;
using System.Collections.Generic;

namespace ElectionLibrary.Character
{
    [Serializable]
    public class Activist : PoliticalCharacter
    {
        public Activist(string name, Position position, PoliticalParty politicalParty) : base(name, new ActivistBehavior(), position, politicalParty)
        {
        }

        protected override void ComputeObjectsInteraction(Street street)
        {
            // Try to remove other party posters
            TryRemovePoster(street);

            // Try to put a poster on the area
            if(!street.IsThereAlreadyPosterOfParty(PoliticalParty))
            {
                TryPutPoster(street);
            }
        }

        private void TryRemovePoster(Street street)
        {
            Poster posterToRemove = null;

            foreach (AbstractObject obj in street.Objects)
            {
                if (obj is Poster poster && poster.Party != PoliticalParty)
                {
                    Random random = new Random();
                    int pickedNumber = random.Next(4);

                    if (pickedNumber == 0)
                    {
                        posterToRemove = poster;
                    }
                }
            }

            if(posterToRemove != null)
            {
                street.RemoveObject(posterToRemove);
            }
        }

        private void TryPutPoster(Street street)
        {
            List<Poster> posters = GetPosters();

            if(posters.Count > 0)
            {
                Poster poster = posters[0];
                UseObject(poster);
                street.AddObject(poster);
            }
        }

        public override void Tired()
        {
            Moral--;

            if (Moral <= 0)
            {
                State = new IsGoingBackToHQState();
            }
        }

        public List<Poster> GetPosters()
        {
            List<Poster> posters = new List<Poster>();

            foreach(AbstractElectionObject obj in Objects)
            {
                if(obj is Poster poster)
                {
                    posters.Add(poster);
                }
            }

            return posters;
        }
    }
}

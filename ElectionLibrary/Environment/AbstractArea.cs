using ElectionLibrary.Object;
using System;
using System.Collections.Generic;

namespace ElectionLibrary.Environment
{
    [Serializable]
    public abstract class AbstractArea : AbstractLibrary.Environment.AbstractArea
    {
        public Position Position { get;}

        protected AbstractArea(string name, Position position) : base(name)
        {
            this.Position = position;
        }

        public List<Poster> GetPosters()
        {
            List<Poster> posters = new List<Poster>();

            foreach (AbstractElectionObject obj in Objects)
            {
                if (obj is Poster poster)
                {
                    posters.Add(poster);
                }
            }

            return posters;
        }
    }
}

using ElectionLibrary.Environment;
using System;
using System.Collections.Generic;

namespace ElectionLibrary.Character.Behavior
{
    public abstract class AbstractBehavior
    {
        private Random random = new Random();

        public abstract Position Move(ElectionCharacter character, AbstractArea area);

        protected AbstractArea GetRandomStreet(AbstractArea area)
        {
            List<AbstractArea> streets = new List<AbstractArea>();

            foreach (ElectionAccess access in area.Accesses)
            {
                if (access.EndArea is Street)
                {
                    streets.Add((AbstractArea)access.EndArea);
                }
            }

            int newStreet = random.Next(streets.Count);
            return streets[newStreet];
        }
    }
}
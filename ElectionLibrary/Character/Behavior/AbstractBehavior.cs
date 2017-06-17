using ElectionLibrary.Environment;
using System;
using System.Collections.Generic;

namespace ElectionLibrary.Character.Behavior
{
    [Serializable]
    public abstract class AbstractBehavior
    {
        private readonly Random random = new Random();

        public abstract Position Move(ElectionCharacter character, AbstractArea area);

        public abstract Position DoSomethingInHQ(ElectionCharacter character, AbstractArea area);

        public abstract Position Meeting(ElectionCharacter character, AbstractArea area);

        protected AbstractArea GetNextStreet(AbstractArea area, ElectionCharacter character)
        {
            List<Street> streets = GetStreets(area);

            foreach(Street street in streets)
            {
                if(!character.LastStreets.Contains(street))
                {
                    character.LastStreets.Enqueue(street);
                    if(character.LastStreets.Count > 2)
                    {
                        character.LastStreets.Dequeue();
                    }
                    return street;
                }
            }

            return streets[0];
        }

        private List<Street> GetStreets(AbstractArea area)
        {
            Random rnd = new Random();
            List<Street> streets = new List<Street>();
			foreach (ElectionAccess access in area.Accesses)
			{
				if (access.EndArea is Street street)
				{
                    streets.Add(street);
				}
			}

            return streets;
        }
    }
}
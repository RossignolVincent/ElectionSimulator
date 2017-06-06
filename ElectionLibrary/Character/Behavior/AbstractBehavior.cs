using ElectionLibrary.Environment;
using System;
using System.Collections.Generic;

namespace ElectionLibrary.Character.Behavior
{
    public abstract class AbstractBehavior
    {
        private readonly Random random = new Random();

        public abstract Position Move(ElectionCharacter character, AbstractArea area);

        protected AbstractArea GetNextStreet(AbstractArea area, ElectionCharacter character)
        {
            List<Street> streets = GetStreets(area);

            foreach(Street street in streets)
            {
                if(street != character.LastStreet)
                {
                    character.LastStreet = street;
                    return street;
                }
            }

            return streets[0];
        }

        private List<Street> GetStreets(AbstractArea area)
        {
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
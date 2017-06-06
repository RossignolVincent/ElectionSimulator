using System;
using System.Collections.Generic;
using ElectionLibrary.Environment;

namespace ElectionLibrary.Character.State
{
    [Serializable]
    public abstract class PoliticalCharacterState
    {
        public abstract Position Handle(PoliticalCharacter politician, AbstractArea area);

        protected AbstractArea GetRandomStreet(AbstractArea area)
		{
			Random random = new Random();
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

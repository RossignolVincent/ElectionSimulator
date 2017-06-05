using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectionLibrary.Environment;

namespace ElectionLibrary.Character.Behavior
{
    public class JournalistBehavior : AbstractBehavior
    {
        public override Position Move(ElectionCharacter character, AbstractArea area)
        {
            AbstractArea bestMove = area;
            List<AbstractArea> streetList = new List<AbstractArea>();

            foreach (ElectionAccess access in area.Accesses)
            {
                if (access.EndArea is Street)
                {
                    streetList.Add((AbstractArea)access.EndArea);
                }
            }

            if (bestMove == area)
            {
                Random random = new Random();
                int newStreet = random.Next(streetList.Count);
                bestMove = (AbstractArea)streetList[newStreet];
            }

            return bestMove.Position;
        }
    }
}

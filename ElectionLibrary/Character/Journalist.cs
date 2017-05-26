﻿using ElectionLibrary.Character.Behavior;
using ElectionLibrary.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionLibrary.Character
{
    class Journalist : ElectionCharacter
    {
        public Journalist(string name, AbstractBehavior behavior, Position position) : base(name, behavior, position)
        {
        }

        public override Position MoveDecision(AbstractArea area)
        {
            AbstractArea bestMove = area;
            List<AbstractArea> streetList= new List<AbstractArea>();

            foreach (ElectionAccess access in area.Accesses)
            {
                if (access.EndArea is Street)
                {
                    streetList.Add((AbstractArea) access.EndArea);
                }
            }

            if (bestMove == area)
            {
                Random random = new Random();
                int newStreet = random.Next(streetList.Count);
                bestMove = (AbstractArea)streetList[newStreet];
            }

            return bestMove.position;
        }
    }
}

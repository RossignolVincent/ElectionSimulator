﻿using System;
using ElectionLibrary.Environment;

namespace ElectionLibrary.Character.State
{
    [Serializable]
    public class InStreetState : PoliticalCharacterState
    {
        public InStreetState()
        {
        }

        public override Position Handle(ElectionCharacter character, AbstractArea area)
        {
            // If the politician is in a Street area, try to go in a PublicPlace, or in a Building if there is no politician in there
            return character.Behavior.Move(character, area);
        }
    }
}

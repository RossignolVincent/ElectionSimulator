﻿﻿using System;
using ElectionLibrary.Environment;

namespace ElectionLibrary.Character.State
{
    [Serializable]
    public class InElectionAreaState : PoliticalCharacterState
    {
        public InElectionAreaState()
        {
        }

        public override Position Handle(ElectionCharacter character, AbstractArea area)
        {
            return character.Behavior.Meeting(character, area);
        }
    }
}

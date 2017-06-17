using System;
using System.Collections.Generic;
using ElectionLibrary.Environment;

namespace ElectionLibrary.Character.State
{
    [Serializable]
    public abstract class PoliticalCharacterState
    {
        public abstract Position Handle(AbstractElectionCharacter character, AbstractArea area);
    }
}

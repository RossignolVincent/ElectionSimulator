using System;
using ElectionLibrary.Environment;

namespace ElectionLibrary.Character.State
{
    public class InHQState : PoliticalCharacterState
    {
        public InHQState()
        {
        }

        public override Position Handle(PoliticalCharacter politician, AbstractArea area)
        {
            return GetRandomStreet(area).position;
        }
    }
}

using System;
using ElectionLibrary.Environment;

namespace ElectionLibrary.Character.State
{
    public class InHQState : PoliticalCharacterState
    {
        public InHQState()
        {
        }

        public override Position Handle(ElectionCharacter character, AbstractArea area)
        {
            PoliticalCharacter politician = (PoliticalCharacter)character;

            politician.Rest();
            politician.State = new InStreetState();
            return GetRandomStreet(area).Position;
        }
    }
}

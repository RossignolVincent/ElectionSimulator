using System;
using ElectionLibrary.Environment;

namespace ElectionLibrary.Character.State
{
    [Serializable]
    public class InHQState : PoliticalCharacterState
    {
        public InHQState()
        {
        }

        public override Position Handle(PoliticalCharacter politician, AbstractArea area)
        {
            politician.Rest();
            politician.State = new InStreetState();
            return GetRandomStreet(area).position;
        }
    }
}

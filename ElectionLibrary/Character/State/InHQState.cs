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

        public override Position Handle(ElectionCharacter character, AbstractArea area)
        {
            return character.Behavior.DoSomethingInHQ(character, area);
        }
    }
}

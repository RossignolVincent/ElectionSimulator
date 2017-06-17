using ElectionLibrary.Environment;
using System;

namespace ElectionLibrary.Character.Behavior
{
    [Serializable]
    public class JournalistBehavior : AbstractBehavior
    {
        public override Position Move(AbstractElectionCharacter character, AbstractArea area)
        {
            return GetNextStreet(area, character).Position;
        }

        public override Position DoSomethingInHQ(AbstractElectionCharacter character, AbstractArea area)
        {
            throw new NotImplementedException();
        }

        public override Position Meeting(AbstractElectionCharacter character, AbstractArea area)
        {
            throw new NotImplementedException();
        }
    }
}

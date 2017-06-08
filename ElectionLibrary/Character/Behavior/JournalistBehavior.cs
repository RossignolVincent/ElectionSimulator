using ElectionLibrary.Environment;
using System;

namespace ElectionLibrary.Character.Behavior
{
    [Serializable]
    public class JournalistBehavior : AbstractBehavior
    {
        public override Position Move(ElectionCharacter character, AbstractArea area)
        {
            return GetNextStreet(area, character).Position;
        }
    }
}

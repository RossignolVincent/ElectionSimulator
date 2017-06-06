using ElectionLibrary.Environment;

namespace ElectionLibrary.Character.Behavior
{
    public class JournalistBehavior : AbstractBehavior
    {
        public override Position Move(ElectionCharacter character, AbstractArea area)
        {
            return GetNextStreet(area, character).Position;
        }
    }
}

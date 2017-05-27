using System;
using ElectionLibrary.Environment;

namespace ElectionLibrary.Character.State
{
    public class IsGoingBackToHQState : PoliticalCharacterState
    {
        public IsGoingBackToHQState()
        {
        }

        public override Position Handle(PoliticalCharacter politician, AbstractArea area)
        {
            if(politician.PathToHQ == null)
            {
                politician.SetPathToHQ();
                // Pop the stack to clear the actual position of the character in the path
                politician.PathToHQ.Pop();
            }

            Position nextPosition = politician.PathToHQ.Pop();

            if(politician.PathToHQ.Count == 0)
            {
                politician.State = new InHQState();
                politician.PathToHQ = null;
            }

            return nextPosition;
        }
    }
}

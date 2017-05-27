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
            Position nextPosition = politician.PathToHQ.Pop();

            if(politician.PathToHQ.Count == 0)
            {
                politician.State = new InHQState();
            }

            return nextPosition;
        }
    }
}

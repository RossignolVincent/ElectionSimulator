using System;
using ElectionLibrary.Environment;

namespace ElectionLibrary.Character.State
{
    [Serializable]
    public class IsGoingBackToHQState : PoliticalCharacterState
    {
        public IsGoingBackToHQState()
        {
        }

        public override Position Handle(ElectionCharacter character, AbstractArea area)
        {
            PoliticalCharacter politician = (PoliticalCharacter)character;

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
                politician.NbTurnToRest = 10;
            }

            return nextPosition;
        }
    }
}

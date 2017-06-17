using ElectionLibrary.Character.Behavior;
using ElectionLibrary.Environment;
using ElectionLibrary.Parties;
using ElectionLibrary.Character.State;
using System;

namespace ElectionLibrary.Character
{
    public class Leader : PoliticalCharacter
    {
        public Leader(string name, Position position, PoliticalParty politicalParty) : base(name, new LeaderBehavior(), position, politicalParty)
        {
            Aura = 10;
        }

        protected override void ComputeObjectsInteraction(Street street)
        {
        }

        public override void Tired()
        {
            Moral = 0;
            State = new IsGoingBackToHQState();
        }
    }
}

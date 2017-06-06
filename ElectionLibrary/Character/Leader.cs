using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectionLibrary.Character.Behavior;
using ElectionLibrary.Environment;
using ElectionLibrary.Parties;
using ElectionLibrary.Character.State;

namespace ElectionLibrary.Character
{
    public class Leader : PoliticalCharacter
    {
        public Leader(string name, Position position, PoliticalParty politicalParty) : base(name, new LeaderBehavior(), position, politicalParty)
        {
            Aura = 10;
        }

        public override void Tired()
        {
            Moral = 0;
            State = new IsGoingBackToHQState();
        }
    }
}

using ElectionLibrary.Character.Behavior;
using ElectionLibrary.Environment;
using ElectionLibrary.Parties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionLibrary.Character
{
    [Serializable]
    public class Activist : PoliticalCharacter
    {
        public Activist(string name, Position position, PoliticalParty politicalParty) : base(name, null, position, politicalParty)
        {
            this.Behavior = new ActivistBehavior();
        }
    }
}

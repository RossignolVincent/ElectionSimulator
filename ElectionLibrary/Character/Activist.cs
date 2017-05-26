using ElectionLibrary.Character.Behavior;
using ElectionLibrary.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionLibrary.Character
{
    public class Activist : PoliticalCharacter
    {
        public Activist(string name, Position position, PoliticalParty politicalParty) : base(name, null, position, politicalParty)
        {
            this.behavior = new ActivistBehavior();
        }
    }
}

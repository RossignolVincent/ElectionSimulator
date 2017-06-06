using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectionLibrary.Environment;

namespace ElectionLibrary.Character.Behavior
{
    [Serializable]
    public class JournalistBehavior : AbstractBehavior
    {
        public override void NextTurn(AbstractArea area)
        {
            throw new NotImplementedException();
        }

        public override int Influence(int moral, int nbTurn)
        {
            throw new NotImplementedException();
        }
    }
}

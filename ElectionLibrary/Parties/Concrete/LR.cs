using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionLibrary.Parties.Concrete
{
    public class LR : PoliticalParty
    {

        public override PoliticalParty Instance()
        {
            if (instance == null)
            {
                instance = new LR();
                name = "Les Républicains";
            }

            return instance;
        }
    }
}

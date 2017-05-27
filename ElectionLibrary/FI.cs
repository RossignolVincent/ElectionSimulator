using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionLibrary.Parties.Concrete
{
    public class FI : PoliticalParty
    {

        public override PoliticalParty Instance()
        {
            if (instance == null)
            {
                instance = new FI();
                name = "France Insoumise";
            }

            return instance;
        }
    }
}

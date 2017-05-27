using ElectionLibrary.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionLibrary.Parties.Concrete
{
    public class LR : PoliticalParty
    {
        public LR(HQ hq) : base(hq)
        {
            Name = "Les Républicains";
        }
    }
}

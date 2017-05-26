using AbstractLibrary.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionLibrary.Environment
{
    class ElectionAccess : AbstractAccess
    {

        public ElectionAccess(AbstractArea firstArea, AbstractArea endArea) : base(firstArea, endArea)
        {

        }
    }
}

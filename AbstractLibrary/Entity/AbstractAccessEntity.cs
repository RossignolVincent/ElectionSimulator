using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractLibrary.Entity
{
    public abstract class AbstractAccessEntity : AbstractEntity
    {
        public AbstractAreaEntity FirstArea { get; set; }
        public AbstractAreaEntity EndArea { get; set; }
    }
}

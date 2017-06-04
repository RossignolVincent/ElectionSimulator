using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractLibrary.Entity
{
    public class AbstractAreaEntity : AbstractEntity
    {
        public string Name { get; set; }
        //public List<AbstractAccessEntity> Accesses { get; set; }
        public List<AbstractObjectEntity> Objects { get; set; }
        public List<AbstractCharacterEntity> Characters { get; set; }
    }
}

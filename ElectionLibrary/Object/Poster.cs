using AbstractLibrary.Object;
using ElectionLibrary.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionLibrary.Object
{
    public class Poster : AbstractObject
    {
        public Position position { get; set; }

        public Poster(string name, Position position) : base(name)
        {
        }
    }
}

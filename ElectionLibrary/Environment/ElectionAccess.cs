using AbstractLibrary.Environment;
using System;

namespace ElectionLibrary.Environment
{
    [Serializable]
    public class ElectionAccess : AbstractAccess
    {
        public ElectionAccess(AbstractArea firstArea, AbstractArea endArea) : base(firstArea, endArea)
        {
        }
    }
}

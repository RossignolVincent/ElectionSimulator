using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractLibrary.Factory;
using AbstractLibrary.Environment;
using AbstractLibrary.Character;
using AbstractLibrary.Object;

namespace ElectionSimulator.Factory
{
    public class ElectionFactory : AbstractFactory
    {
        public override AbstractEnvironment CreateEnvironment()
        {
            Console.Write("Create Environment");
            throw new NotImplementedException();
        }

        public override AbstractArea CreateArea()
        {
            Console.Write("Create Environment");
            throw new NotImplementedException();
        }

        public override AbstractAccess CreateAccess(AbstractArea startArea, AbstractArea endArea)
        {
            Console.Write("Create Access");
            throw new NotImplementedException();
        }

        public override AbstractCharacter CreateCharacter()
        {
            Console.Write("Create Character");
            throw new NotImplementedException();
        }

        public override AbstractObject CreateObject()
        {
            Console.Write("Create Object");
            throw new NotImplementedException();
        }
    }
}

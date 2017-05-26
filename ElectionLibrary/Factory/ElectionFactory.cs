using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractLibrary.Environment;
using AbstractLibrary.Factory;
using AbstractLibrary.Character;
using AbstractLibrary.Object;
using ElectionLibrary.Environment;

namespace ElectionLibrary.Factory
{
    public class ElectionFactory : AbstractFactory
    {
        public override AbstractEnvironment CreateEnvironment()
        {
            Console.Write("Create Environment");
            throw new NotImplementedException();
        }

        public override AbstractAccess CreateAccess(AbstractLibrary.Environment.AbstractArea startArea, AbstractLibrary.Environment.AbstractArea endArea)
        {
            Console.Write("Create Access");
            throw new NotImplementedException();
        }

        public override AbstractObject CreateObject()
        {
            Console.Write("Create Object");
            throw new NotImplementedException();
        }

        /********************************************************************
         *                              AREA                                *
         ********************************************************************/

		public override AbstractLibrary.Environment.AbstractArea CreateArea()
		{
			return null;
		}

        public ElectionLibrary.Environment.AbstractArea CreateBuilding(Opinion opinion, Position position)
        {
            if(opinion == null || position == null)
            {
                throw new ArgumentException();
            }

            return new Building(opinion, "", position);
        }

        public AbstractLibrary.Environment.AbstractArea CreatePublicPlace(Opinion opinion, Position position)
        {
			if (opinion == null || position == null)
			{
				throw new ArgumentException();
			}

            return new PublicPlace(opinion, "", position);
        }

        public ElectionLibrary.Environment.AbstractArea CreateStreet(Position position)
        {
			if (position == null)
			{
				throw new ArgumentException();
			}

            return new Street("", position);
        }

		public AbstractLibrary.Environment.AbstractArea CreateHQ(Position position)
		{
			if (position == null)
			{
				throw new ArgumentException();
			}

            return new HQ("", position);
		}

		/********************************************************************
         *                           CHARACTERS                             *
         ********************************************************************/

		public override AbstractCharacter CreateCharacter()
		{
            return null;
		}
	}
}

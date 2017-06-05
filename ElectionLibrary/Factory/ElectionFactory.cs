﻿using System;
using AbstractLibrary.Environment;
using AbstractLibrary.Factory;
using AbstractLibrary.Character;
using AbstractLibrary.Object;
using ElectionLibrary.Environment;
using ElectionLibrary.Character;
using ElectionLibrary.Parties;
using ElectionLibrary.Character.Behavior;

namespace ElectionLibrary.Factory
{
    public class ElectionFactory : AbstractFactory
    {
        public override AbstractEnvironment CreateEnvironment()
        {
            Console.Write("Create Environment");
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

        public Environment.AbstractArea CreatePublicPlace(Opinion opinion, Position position)
        {
			if (opinion == null || position == null)
			{
				throw new ArgumentException();
			}

            return new PublicPlace(opinion, "", position);
        }

        public Environment.AbstractArea CreateStreet(Position position)
        {
			if (position == null)
			{
				throw new ArgumentException();
			}

            return new Street("", position);
        }

		public Environment.AbstractArea CreateHQ(Position position, PoliticalParty party)
		{
            if (position == null || party == null)
			{
				throw new ArgumentException();
			}

            return new HQ("", position, party);
		}

		public Environment.AbstractArea CreateEmptyArea(Position position)
		{
			if (position == null)
			{
				throw new ArgumentException();
			}

            return new EmptyArea("", position);
		}

        /********************************************************************
         *                           CHARACTERS                             *
         ********************************************************************/

        public override AbstractCharacter CreateCharacter()
		{
            return null;
		}

        public ElectionCharacter CreateActivist(Position position, PoliticalParty party)
        {
            if(position == null || party == null)
            {
                throw new ArgumentException();
            }

            return new Activist("", position, party);
        }


        public Journalist CreateJournalist(Position position)
        {
            if (position == null)
                throw new ArgumentException();

            return new Journalist("", position);
        }

        public Leader CreateLeader(Position position, PoliticalParty party)
        {
            if (position == null || party == null)
            {
                throw new ArgumentException();
            }

            return new Leader("", position, party);
        }

        /********************************************************************
         *                              ACCESSES                            *
         ********************************************************************/

        public override AbstractAccess CreateAccess(AbstractLibrary.Environment.AbstractArea startArea, AbstractLibrary.Environment.AbstractArea endArea)
		{
            return null;
		}

        public ElectionAccess CreateElectionAccess(Environment.AbstractArea startArea, Environment.AbstractArea endArea)
        {
            if(startArea == null || endArea == null)
            {
                throw new ArgumentException();
            }

            return new ElectionAccess(startArea, endArea);
        }
	}
}

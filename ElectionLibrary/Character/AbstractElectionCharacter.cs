﻿﻿using System;
using AbstractLibrary.Character;
using ElectionLibrary.Character.Behavior;
using ElectionLibrary.Environment;
using System.Collections.Generic;
using ElectionLibrary.Character.State;
using ElectionLibrary.Object;

namespace ElectionLibrary.Character
{
    [Serializable]
    public abstract class AbstractElectionCharacter : AbstractCharacter 
    {
        public static int INIT_MORAL = 25;

        public AbstractBehavior Behavior { get; set; }
		public PoliticalCharacterState State { get; set; }
		public Position Position { get; set; }
        public Queue<Street> LastStreets { get; set; }

        public int Aura { get; set; }
        public string Role { get; }

        public List<AbstractElectionObject> Objects { get; set; }

        private int moral;
        public int Moral 
        {
            get { return moral; }
            set
            {
                if (value < 0)
                {
                    moral = 0;
                }
                else if(value > INIT_MORAL)
                {
                    moral = INIT_MORAL;
                }
                else
                {
                    moral = value;
                }
            }
        }

        protected AbstractElectionCharacter(string name, AbstractBehavior behavior, Position position) : base(name)
        {
            this.Behavior = behavior;
            this.Position = position;
            this.moral = INIT_MORAL;
            this.Aura = 2;
            this.Role = GetType().Name;
            LastStreets = new Queue<Street>();
            Objects = new List<AbstractElectionObject>();
        }

        public void NextTurn(AbstractArea area, List<List<AbstractArea>> areas)
        {
            if(area is Street street)
            {
                // OBJECTS INTERACTION
                ComputeObjectsInteraction(street);
                
                // CHARACTERS INTERACTION
                if (area.Characters.Count > 1)
                {
                    ComputeCharactersInteraction(area.Characters);
                }
            }

            // MOVE
            this.MoveTo(MoveDecision(area, areas));
        }

        public void MoveTo(Position position)
        {
            this.Position = position;
        }

        public void AddRandomAura()
        {
            Random random = new Random();
            int pickedNumber = random.Next(100);

            // 10% chance to get more Aura
            if(pickedNumber >= 90)
            {
                if (pickedNumber < 96) // 6% chance to get 1 extra Aura
                {
                    Aura += 1;
                    Console.WriteLine("Aura + 1 pour " + ((PoliticalCharacter) this).PoliticalParty.Name);
                }
                else if(pickedNumber < 99) // 3% chance to get 2 extra Aura
                {
                    Aura += 2;
                    Console.WriteLine("Aura + 2 pour " + ((PoliticalCharacter)this).PoliticalParty.Name);
                }
                else // 1% chance to get 3 extra Aura and get max of Moral
                {
                    Aura += 3;
                    Moral = INIT_MORAL;
                    Console.WriteLine("Aura + 3  et Moral au max pour " + ((PoliticalCharacter)this).PoliticalParty.Name);
                }
            }
        }

        protected abstract void ComputeObjectsInteraction(Street area);

        protected abstract void ComputeCharactersInteraction(List<AbstractCharacter> characters);

        public abstract Position MoveDecision(AbstractArea area, List<List<AbstractArea>> areas);

        public abstract void Rest();

        public abstract void Tired();

        public int ObjectsNumber()
        {
            return Objects.Count;
        }

        public void UseObject(AbstractElectionObject useThisObject)
        {
            Objects.Remove(useThisObject);
        }

        public void AddAnObject(AbstractElectionObject newObject)
        {
            Objects.Add(newObject);
        }

        public void AddObjects(List<AbstractElectionObject> listObject)
        {
            foreach (AbstractElectionObject newObject in listObject)
            {
                Objects.Add(newObject);
            }
        }

        public void AddObjects(List<Poster> listObject)
        {
            foreach (Poster newObject in listObject)
            {
                Objects.Add(newObject);
            }
        }

        public List<Poster> GetPosters()
        {
            List<Poster> posters = new List<Poster>();

            foreach (AbstractElectionObject obj in Objects)
            {
                if (obj is Poster poster)
                {
                    posters.Add(poster);
                }
            }

            return posters;
        }
    }
}

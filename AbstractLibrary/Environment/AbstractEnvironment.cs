using System;
using System.Collections.Generic;
using AbstractLibrary.Character;
using AbstractLibrary.Factory;
using AbstractLibrary.Object;

namespace AbstractLibrary.Environment
{
    public abstract class AbstractEnvironment
    {
        public List<AbstractArea> Areas { get; }
        public List<AbstractAccess> Accesses { get; }
        public List<AbstractObject> Objects { get; }
        public List<AbstractCharacter> Characters { get; }

        protected AbstractEnvironment()
        {
            this.Areas = new List<AbstractArea>();
            this.Accesses = new List<AbstractAccess>();
            this.Objects = new List<AbstractObject>();
            this.Characters = new List<AbstractCharacter>();
        }

        public void AddArea(AbstractArea newArea)
        {
            this.Areas.Add(newArea);
        }

        public void AddAreas(AbstractArea[] newAreas)
        {
            this.Areas.AddRange(newAreas);
        }

        public void AddAccess(AbstractAccess newAccess)
        {
            this.Accesses.Add(newAccess);
        }

        public void AddObject(AbstractObject newObject)
        {
            this.Objects.Add(newObject);
        }

        public void AddCharacter(AbstractCharacter newCharacter)
        {
            this.Characters.Add(newCharacter);
        }

        public void LoadEnvironment(AbstractFactory factory)
        {
            
        }

        public void LoadObject(AbstractFactory factory)
        {
            
        }

        public void LoadCharacter(AbstractFactory factory)
        {
            
        }

        public void MoveCharacter(AbstractCharacter character, AbstractArea source, AbstractArea target)
        {
            character.Position = target;
            source.RemoveCharacter(character);
            target.AddCharacter(character);
        }

        public string Simulate()
        {
            return null;
        }

        public string Statistics()
        {
            return null;
        }
    }
}

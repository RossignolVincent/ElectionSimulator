using System;
using System.Collections.Generic;
using AbstractLibrary.Character;
using AbstractLibrary.Object;

namespace AbstractLibrary.Factory
{
    public abstract class AbstractArea
    {
		public string Name { get; private set; }
		public List<AbstractAccess> Accesses { get; }
        public List<AbstractObject> Objects { get; }
        public List<AbstractCharacter> Characters { get; }

        private AbstractArea(string name)
        {
			this.Name = name;
			this.Accesses = new List<AbstractAccess>();
            this.Objects = new List<AbstractObject>();
            this.Characters = new List<AbstractCharacter>();
        }

		/**********************************************************************
		 *                          ACCESS
         **********************************************************************/
		public void AddAccess(AbstractAccess newAccess)
        {
            this.Accesses.Add(newAccess);
        }

        
        /**********************************************************************
         *                          OBJECT
         **********************************************************************/
        public void AddObject(AbstractObject newObject)
        {
            this.Objects.Add(newObject);
        }

		public void RemoveObject(AbstractObject oldObject)
		{
			if (Objects.Contains(oldObject))
			{
				Objects.Remove(oldObject);
			}
		}


        /**********************************************************************
         *                          CHARACTER
         **********************************************************************/
        public void AddCharacter(AbstractCharacter newCharacter)
        {
            this.Characters.Add(newCharacter);
        }

        public void RemoveCharacter(AbstractCharacter oldCharacter)
        {
            if(Characters.Contains(oldCharacter))
            {
                Characters.Remove(oldCharacter);
            }
        }


    }
}

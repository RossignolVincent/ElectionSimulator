using System;
using AbstractLibrary.Character;
using AbstractLibrary.Environment;
using AbstractLibrary.Object;

namespace AbstractLibrary.Factory
{
    [Serializable]
    public abstract class AbstractFactory
    {
        public string Title { get; }

        protected AbstractFactory()
        {
        }

		public abstract AbstractEnvironment CreateEnvironment();
        public abstract AbstractArea CreateArea();
        public abstract AbstractAccess CreateAccess(AbstractArea startArea, AbstractArea endArea);

        public abstract AbstractCharacter CreateCharacter();
        public abstract AbstractObject CreateObject();
    }
}
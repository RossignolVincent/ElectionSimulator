using AbstractLibrary.Character;
using AbstractLibrary.Entity;
using AbstractLibrary.Environment;
using AbstractLibrary.Object;
using FizzWare.NBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractLibrary.Adapter
{
    public class AbstractAreaAdapter : AbstractAdapter
    {
        protected override AbstractEntity buildEntityFromDomain(IDomain domain)
        {
            if (domain == null)
                return null;
            AbstractArea obj = (AbstractArea)domain;
            return Builder<AbstractAreaEntity>.CreateNew()
                    .With(x => x.Name = obj.Name)
                    //.With(x => x.Accesses = obj.Accesses.ConvertAll((curr) => (AbstractAccessEntity) new AbstractAccessAdapter().FromDomain(curr)))
                    .With(x => x.Objects = obj.Objects.ConvertAll((curr) => (AbstractObjectEntity) new AbstractObjectsAdapter().FromDomain(curr)))
                    .With(x => x.Characters = obj.Characters.ConvertAll((curr) => (AbstractCharacterEntity)new AbstractCharacterAdapter().FromDomain(curr)))
                    .Build();
        }

        protected override IDomain buildDomainFromEntity(AbstractEntity entity)
        {
            if (entity == null)
                return null;
            AbstractAreaEntity obj = (AbstractAreaEntity)entity;
            return Builder<AbstractArea>.CreateNew()
                    .With(x => x.Name = obj.Name)
                    //.With(x => x.Accesses = obj.Accesses.ConvertAll((curr) => (AbstractAccess) new AbstractAccessAdapter().FromEntity(curr)))
                    .With(x => x.Objects = obj.Objects.ConvertAll((curr) => (AbstractObject) new AbstractObjectsAdapter().FromEntity(curr)))
                    .With(x => x.Characters = obj.Characters.ConvertAll((curr) => (AbstractCharacter) new AbstractCharacterAdapter().FromEntity(curr)))
                    .Build();
        }
    }
}

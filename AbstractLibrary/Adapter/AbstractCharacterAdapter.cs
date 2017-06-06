using AbstractLibrary.Character;
using AbstractLibrary.Entity;
using AbstractLibrary.Environment;
using FizzWare.NBuilder;
using System.Security.Cryptography;

namespace AbstractLibrary.Adapter
{
    public class AbstractCharacterAdapter : AbstractAdapter
    {
        protected override AbstractEntity buildEntityFromDomain(IDomain domain)
        {
            SHA512 sha512 = new SHA512Managed();
            AbstractCharacter obj = (AbstractCharacter)domain;
            return Builder<AbstractCharacterEntity>.CreateNew()
                    .With(x => x.Name = obj.Name)
                    .With(x => x.Position = (AbstractAreaEntity) new AbstractAreaAdapter().FromDomain(obj.Position))
                    .Build();
        }

        protected override IDomain buildDomainFromEntity(AbstractEntity entity)
        {
            AbstractCharacterEntity obj = (AbstractCharacterEntity)entity;
            return Builder<AbstractCharacter>.CreateNew()
                    .With(x => x.Name = obj.Name)
                    .With(x => x.Position = (AbstractArea) new AbstractAreaAdapter().FromEntity(obj.Position))
                    .Build();
        }
    }
}
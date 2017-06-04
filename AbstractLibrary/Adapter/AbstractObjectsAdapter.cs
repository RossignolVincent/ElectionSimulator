using AbstractLibrary.Entity;
using AbstractLibrary.Environment;
using AbstractLibrary.Object;
using FizzWare.NBuilder;

namespace AbstractLibrary.Adapter
{
    public class AbstractObjectsAdapter : AbstractAdapter
    {
        protected override AbstractEntity buildEntityFromDomain(IDomain domain)
        {
            AbstractObject obj = (AbstractObject)domain;
            return Builder<AbstractObjectEntity>.CreateNew()
                    .With(x => x.Name = obj.Name)
                    .With(x => x.Position = (AbstractAreaEntity)new AbstractAreaAdapter().FromDomain(obj.Position))
                    .Build();
        }

        protected override IDomain buildDomainFromEntity(AbstractEntity entity)
        {
            AbstractObjectEntity obj = (AbstractObjectEntity)entity;
            return Builder<AbstractObject>.CreateNew()
                    .With(x => x.Name = obj.Name)
                    .With(x => x.Position = (AbstractArea)new AbstractAreaAdapter().FromEntity(obj.Position))
                    .Build();
        }
    }
}
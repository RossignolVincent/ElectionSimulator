using AbstractLibrary.Entity;
using AbstractLibrary.Environment;
using FizzWare.NBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractLibrary.Adapter
{
    public class AbstractAccessAdapter : AbstractAdapter
    {
        protected override AbstractEntity buildEntityFromDomain(IDomain domain)
        {
            AbstractAccess obj = (AbstractAccess)domain;
            return Builder<AbstractAccessEntity>.CreateNew()
                    .With(x => x.FirstArea = (AbstractAreaEntity) new AbstractAreaAdapter().FromDomain(obj.FirstArea))
                    .With(x => x.EndArea = (AbstractAreaEntity) new AbstractAreaAdapter().FromDomain(obj.EndArea))
                    .Build();
        }

        protected override IDomain buildDomainFromEntity(AbstractEntity entity)
        {
            AbstractAccessEntity obj = (AbstractAccessEntity) entity;
            return Builder<AbstractAccess>.CreateNew()
                    .With(x => x.FirstArea = (AbstractArea) new AbstractAreaAdapter().FromEntity(obj.FirstArea))
                    .With(x => x.EndArea = (AbstractArea) new AbstractAreaAdapter().FromEntity(obj.EndArea))
                    .Build();
        }
    }
}

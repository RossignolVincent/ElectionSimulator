using AbstractLibrary.Entity;
using AbstractLibrary.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractLibrary.Adapter
{
    public abstract class AbstractAdapter
    {
        public IDomain FromEntity(AbstractEntity entity)
        {
            return buildDomainFromEntity(entity);
        }

        public IDomain ToDomain(AbstractEntity entity)
        {
            return buildDomainFromEntity(entity);
        }

        public AbstractEntity FromDomain(IDomain domain)
        {
            return buildEntityFromDomain(domain);
        }
        
        public AbstractEntity ToEntity(IDomain domain)
        {
            return buildEntityFromDomain(domain);
        }

        protected virtual AbstractEntity buildEntityFromDomain(IDomain domain)
        {
            throw new NotImplementedException();
        }

        protected virtual IDomain buildDomainFromEntity(AbstractEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}

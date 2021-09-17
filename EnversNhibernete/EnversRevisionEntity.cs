using NHibernate.Envers;
using NHibernate.Envers.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EnversNhibernete
{
    [RevisionEntity(typeof(EnversRevisionListener))]
    public class EnversRevisionEntity : DefaultRevisionEntity
    {
        public virtual string UserName { get; set; }
    }
}

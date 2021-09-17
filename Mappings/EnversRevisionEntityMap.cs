using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;
using WebApplication1.EnversNhibernete;

namespace WebApplication1.Mappings
{
    //public class EnversRevisionEntityMap : ClassMapping<EnversRevisionEntity>
    //{
    //    public EnversRevisionEntityMap()
    //    {
    //        Property(x => x.Id);
    //        Property(x => x.RevisionDate);
    //        Property(x => x.UserName);
    //    }
    //}

    public class EnversRevisionEntityMap : ClassMap<EnversRevisionEntity>
    {
        //la revision entity debe ser mapeada
        public EnversRevisionEntityMap()
        {
            Id(x => x.Id);
            Map(x => x.RevisionDate);
            Map(x => x.UserName);
        }
    }
}


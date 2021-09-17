using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace WebApplication1.Mappings
{
    //public class PersonaMap : ClassMapping<Persona>
    //{
    //    public PersonaMap()
    //    {
    //        //Id(x => x.Id).GeneratedBy.Identity()
    //        //    .UnsavedValue(0);
    //        //Map(x => x.Nombre);
    //        //Map(x => x.Apellido);
    //        //Id(x => x.Id, m =>
    //        //{
    //        //    m.Generator(Generators.Increment);
    //        //    m.UnsavedValue(0);
    //        //});
    //        //Property(x => x.Nombre);
    //        //Property(x => x.Apellido);
    //    }
    //}

    public class PersonaMap : ClassMap<Persona>
    {
        public PersonaMap()
        {
            Id(x => x.Id).GeneratedBy.Identity()
                .UnsavedValue(0);
            Map(x => x.Nombre);
            Map(x => x.Apellido);
            //Id(x => x.Id, m =>
            //{
            //    m.Generator(Generators.Increment);
            //    m.UnsavedValue(0);
            //});
            //Property(x => x.Nombre);
            //Property(x => x.Apellido);
        }
    }
}

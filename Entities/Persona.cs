using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate.Envers.Configuration.Attributes;

namespace WebApplication1.Entities
{
    public class Persona : BaseEntity
    {
        public virtual string Nombre { get; set; }

        public virtual string Apellido { get; set; }
    }
}

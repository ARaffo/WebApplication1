using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DTOs
{
    public class PersonaDTO
    {
        public virtual long Id { get; set; }

        public virtual string Nombre { get; set; }

        public virtual string Apellido { get; set; }
    }
}

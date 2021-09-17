using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.DataComponents
{
    public class FilterAndSearchPersonResult
    {
        public virtual IList<Persona> Personas { get; set; }

        public virtual string NextPage { get; set; }

        public virtual string PreviusPage { get; set; }

        public virtual int NumeroDePaginas { get; set; }

    }
}

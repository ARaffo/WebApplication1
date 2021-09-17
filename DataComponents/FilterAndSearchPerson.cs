using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DTOs;


namespace WebApplication1.DataComponents
{
    public class FilterAndSearchPerson
    {
        public virtual string StringSearch { get; set;}

        public virtual int PageNumber { get; set; }

        public virtual int Limit { get; set; }

        public virtual bool OrderAsc { get; set; }

        public virtual string OrderField { get; set; }


    }
}

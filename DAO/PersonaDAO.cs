using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace WebApplication1.DAO
{
    public class PersonaDAO : GenericDAO<Persona,long>
    {
        public PersonaDAO(ISession session) : base(session)
        {

        }

        public IList<Persona> FindByName(string name, int limit)
        {
            return this.session.QueryOver<Persona>()
                .WhereRestrictionOn(x => x.Nombre).IsLike($"{name}" + "%")
                .SetFetchSize(limit)
                .List<Persona>();
        }

        public IList<Persona> FindByLastName(string lastName, int limit)
        {
            return this.session.QueryOver<Persona>()
                .WhereRestrictionOn(x => x.Apellido).IsLike($"{lastName}" + "%")
                .SetFetchSize(limit)
                .List<Persona>();
        }

        public IList<Persona> FilterPerson(int pageNumber, int limit, bool orderAsc, string orderField)
        {
            return orderField switch
            {
                "nombre" => orderAsc ? FilterAndOrder(orderField, pageNumber, limit, orderAsc) : FilterAndOrder(orderField, pageNumber, limit, orderAsc),
                "apellido" => orderAsc ? FilterAndOrder(orderField, pageNumber, limit, orderAsc) : FilterAndOrder(orderField, pageNumber, limit, orderAsc),
                _ => orderAsc ? FilterAndOrder("id", pageNumber, limit, orderAsc) : FilterAndOrder(orderField, pageNumber, limit, orderAsc),
            };
        }

        private IList<Persona> FilterAndOrder(string orderField, int pageNumber, int limit, bool orderAsc)
        {
            if (orderAsc)
            {
                return this.session.CreateCriteria<Persona>()
                       .AddOrder(Order.Asc($"{orderField}"))
                       .SetFirstResult(pageNumber)
                       .SetMaxResults(limit)                      
                        .List<Persona>();
            }
            else
            {
                return this.session.CreateCriteria<Persona>()
                       .AddOrder(Order.Desc($"{orderField}"))
                       .SetFirstResult(pageNumber)
                       .SetMaxResults(limit)                      
                        .List<Persona>();
            }
        }
    }
}

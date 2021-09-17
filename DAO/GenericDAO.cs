using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DAO
{
    public abstract class GenericDAO<T, ID>
    {
        protected ISession session;
        public GenericDAO(ISession session)
        {
            this.session = session;
        }

        internal T Save(T entity)
        {
            this.session.Save(entity);
            this.session.Flush();
            return entity;
        }

        internal T GetId(T entity)
        {
            this.session.GetIdentifier(entity);
            this.session.Flush();
            return entity;
        }
        internal void Delete(T entity)
        {
            this.session.Delete(entity);
            this.session.Flush();
        }

        internal void Update(T entity)
        {
            this.session.Update(entity);
            this.session.Flush();
        }

        internal void ClearObjectCache(T entity)
        {
            this.session.Evict(entity);
            this.session.Flush();
        }

        internal void SaveOrUpdate(T entity)
        {
            this.session.SaveOrUpdate(entity);
            this.session.Flush();
        }
        internal void UpdateMerge(T entity)
        {
            this.session.Merge(entity);
            this.session.Flush();
        }

        internal T FindByID(ID id)
        {
            return this.session.Get<T>(id);
        }

        internal T LoadById(ID id)
        {
            return this.session.Load<T>(id);
        }

        internal IList<T> FindAll()
        {
            return this.session.CreateCriteria(typeof(T)).List<T>();
        }

        internal int CountAllElement()
        {
            return this.session.CreateCriteria(typeof(T))
                .SetProjection(Projections.RowCount())
                .UniqueResult<int>();
        }
        
    }
}

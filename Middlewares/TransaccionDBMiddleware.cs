using Microsoft.AspNetCore.Http;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Middlewares
{
    public class TransaccionDBMiddleware
    {
        private readonly RequestDelegate next;
        public TransaccionDBMiddleware(RequestDelegate next)
        {
            this.next = next;

        }
        public async Task Invoke(HttpContext context, IServiceProvider serviceProvider)
        {
            var session = (NHibernate.ISession)serviceProvider.GetService(typeof(NHibernate.ISession));

            try
            {
                session.BeginTransaction();
                await next.Invoke(context);
                session.GetCurrentTransaction().Commit();
            }
            catch (Exception)
            {
                session.GetCurrentTransaction().Rollback();
                throw; //poner una exepcin personalizada aca
            }
            finally
            {
                session.Close();
            }



        }
    }
}

using NHibernate.Envers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EnversNhibernete
{
    public class EnversRevisionListener : IRevisionListener
    {
        //private readonly string _userName;

        //public EnversRevisionListener(string username) : base()
        //{
        //    _userName = username;
        //}

        public void NewRevision(object revisionEntity)
        {
            if (revisionEntity is EnversRevisionEntity casted)
            {
                casted.UserName = "pruebarda";
            }
            //if (revisionEntity is EnversRevisionEntity casted)
            //{
            //    casted.UserName = "hola";
            //}
        }


        //private static readonly string Data = "test data";
        //public void NewRevision(object revisionEntity)
        //{
        //    ((EnversRevisionEntity)revisionEntity).UserName = Data;
        //}
    }
}

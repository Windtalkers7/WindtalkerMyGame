using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;

namespace MyGameServer
{
    public class NHibernateHandler
    {
        private static ISessionFactory sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if(sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure();
                    configuration.AddAssembly("MyGameServer");

                    sessionFactory = configuration.BuildSessionFactory();
                }
                return sessionFactory;
            }
        }

       public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}

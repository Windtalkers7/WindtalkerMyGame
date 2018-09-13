using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGameServer.Model;
using NHibernate;
using NHibernate.Criterion;

namespace MyGameServer.Manager
{
    public class UserManager : IUserManager
    {
        public void Add(User user)
        {
            using (ISession session = NHibernateHandler.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(user);
                    transaction.Commit();
                }
            }
        }

        public void Delete(User user)
        {
            using (ISession session = NHibernateHandler.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(user);
                    transaction.Commit();
                }
            }
        }

        public void Update(User user)
        {
            using (ISession session = NHibernateHandler.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(user);
                    transaction.Commit();
                }
            }
        }


        public User GetById(int id)
        {
            using (ISession session = NHibernateHandler.OpenSession())
            {
                User user = session.CreateCriteria(typeof(User))
                            .Add(Restrictions.Eq("Id", id)).UniqueResult<User>();
                return user;
            }
        }

        public User GetByUsername(string username)
        {
            using (ISession session = NHibernateHandler.OpenSession())
            {
                User user = session.CreateCriteria(typeof(User))
                            .Add(Restrictions.Eq("Username", username)).UniqueResult<User>();

                return user;
            }
        }


        public ICollection<User> GetAllUsers()
        {
            using (ISession session = NHibernateHandler.OpenSession())
            {
                IList<User> users = session.CreateCriteria(typeof(User)).List<User>();
                return users;
            }

        }

        public bool VerifyUser(string username, string password)
        {
            using (ISession session = NHibernateHandler.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(User));
                criteria.Add(Restrictions.Eq("Username", username));
                criteria.Add(Restrictions.Eq("Password", password));
                User user = criteria.UniqueResult<User>();

                if (user == null)
                    return false;
                else
                    return true;
            }
        }

    }
}

using NHibernate;
using NHibernateDemo.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ISession Session
        {
            get { return SessionManager.GetCurrentSession(); }
        }

        public IList<T> GetAll()
        {
            IList<T> list = Session.CreateCriteria<T>().List<T>();
            return list;
        }

        public IQueryable<T> Query()
        {
            var result = Session.Query<T>();
            return result;
        }

        public T GetById(int id)
        {
            T obj = Session.Get<T>(id);
            return obj;
        }

        public T LoadById(int id)
        {
            T obj = Session.Load<T>(id);
            return obj;
        }

        public int Insert(T obj, bool includeInTransaction = false)
        {
            var identifier = Session.Save(obj);
            if (!includeInTransaction)
            {
                Session.Flush();
            }
            return Convert.ToInt32(identifier);
        }

        public void Update(T obj, bool includeInTransaction = false)
        {
            Session.SaveOrUpdate(obj);
            if (!includeInTransaction)
            {
                Session.Flush();
            }
        }

        public void Delete(int id, bool includeInTransaction = false)
        {
            var obj = Session.Get<T>(id);
            Session.Delete(obj);
            if (!includeInTransaction)
            {
                Session.Flush();
            }
        }
    }
}

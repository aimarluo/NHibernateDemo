using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAll();
        IQueryable<T> Query();
        T GetById(int id);
        T LoadById(int id);
        int Insert(T obj, bool includeInTransaction = false);
        void Update(T obj, bool includeInTransaction = false);
        void Delete(int id, bool includeInTransaction = false);
    }
}

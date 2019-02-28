using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo.Repository.Interface
{
    public interface IOrderService : IRepository<Order>
    {
    }
}

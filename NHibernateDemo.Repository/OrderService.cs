using NHibernate.Criterion;
using NHibernateDemo.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo.Repository
{
    public class OrderService : Repository<Order>, IOrderService
    {

    }
}

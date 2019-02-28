using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo.Entities.Domain
{
    public class User
    {

        public User()
        { 
            Orders = new HashSet<Order>();
        }


        public virtual int Id { get; set; }
        public virtual int Version { get; set; }
        public virtual string Name { get; set; }
        public virtual int Age { get; set; }
        public virtual Address Address { get; set; }
        public virtual ISet<Order> Orders { get; set; }

        public virtual void AddOrder(Order order)
        {
            Orders.Add(order);
            order.Customer = this;
        }

    }
}

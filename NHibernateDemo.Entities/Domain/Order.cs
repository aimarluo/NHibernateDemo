using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo.Entities.Domain
{
    public class Order
    {
        public Order()
        {
            Products = new List<Product>();
        }


        public virtual int Id { get; set; }
        public virtual DateTime Ordered { get; set; }

        public virtual User Customer { get; set; }

        public virtual IList<Product> Products { get; set; } 

    }
}

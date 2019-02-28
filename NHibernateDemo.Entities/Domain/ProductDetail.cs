using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo.Entities.Domain
{
    public class ProductDetail
    {
        public virtual int Id { get; set; }
        public virtual string Description { get; set; }
        public virtual Product Product { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemo.Entities.Domain
{
    public class Product
    {
        public Product()
        {
            Orders = new List<Order>();
        }

        public virtual int Id { get; set; }

        public virtual string ProductCode { get; set; }

        public virtual string ProductName { get; set; }

        public virtual string Description { get; set; }

        public virtual IList<Order> Orders { get; set; }


        public virtual ProductDetail ProductDetail { get; set; }

        public virtual void SetDetailInfo(ProductDetail detail)
        {
            if (this.ProductDetail != null)
            {
                this.ProductDetail.Product = null;
            }
            this.ProductDetail = detail;
            detail.Product = this;
        }
    }
}

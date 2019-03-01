using NHibernateDemo.Entities.Domain;
using NHibernateDemo.Repository;
using NHibernateDemo.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<User> repository = new Repository<User>();

        private readonly IUserService userService = new UserService();
        private readonly IOrderService orderService = new OrderService();
        private readonly IProductService productService = new ProductService();


        public ActionResult Index()
        {
            var list = repository.GetAll();

            var ulist = userService.GetAboutUser(3);


            return View();
        }

        public ActionResult Add()
        {
            var userModel = new User
            {
                Address = new Address { City = "nb", Country = "CH", Province = "zj", Street = "mx" },
                Age = 10,
                Name = "wangwang"
            };

            var order1 = new Order
            {
                Ordered = DateTime.Now
            };
            userModel.AddOrder(order1);
            var order2 = new Order
            {
                Ordered = DateTime.Now.AddDays(-1), 
            };
            userModel.AddOrder(order2);

            userService.Insert(userModel); 


            return View();
        }


        public ActionResult AddProduct()
        {
            ProductDetail detail = new ProductDetail { Description = "This is a very good product" };
            Product product = new Product { ProductCode = "3333", ProductName = "orange" };
            product.SetDetailInfo(detail);

            int productId = productService.Insert(product);

            product.ProductDetail.Description = "This is an excellent product";
            productService.Update(product);

            //productService.Delete(productId);

            return Content("ok");

        }

    }
}
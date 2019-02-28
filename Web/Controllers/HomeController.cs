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


        public ActionResult Index()
        {
            var list = repository.GetAll();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IOC_Web.Entity;

namespace IOC_Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IOC_DbContext db = new IOC_DbContext();
           var d=  db.Students.ToList();
            return View(d);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoBoothPortal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult profile()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult payment_onsite()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult photoupload()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult selection()
        {
            ViewBag.Title = "Home Page";
            
            return View();
        }
        public ActionResult summary()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}

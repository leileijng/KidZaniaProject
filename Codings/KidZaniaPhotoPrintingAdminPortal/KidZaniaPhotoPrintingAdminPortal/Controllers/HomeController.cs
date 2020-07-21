using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KidZaniaPhotoPrintingAdminPortal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Title = "Login Page";

            return View();
        }

        public ActionResult Register()
        {
            ViewBag.Title = "Register Page";

            return View();
        }

        public ActionResult Data()
        {
            ViewBag.Title = "Data Page";

            return View();
        }

        [Authorize(Roles="admin")]
        public ActionResult TestAuth()
        {
            ViewBag.Title = "Test Page";

            return View();
        }
    }
}

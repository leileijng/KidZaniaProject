using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KidZaniaPhotoPrintingAdminPortal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            ViewBag.Title = "Login Page";

            return View();
        }

        //[Authorize(Roles = "Admin")]
        public ActionResult Register()
        {
            ViewBag.Title = "Register Page";

            return View();
        }
        
    }
}

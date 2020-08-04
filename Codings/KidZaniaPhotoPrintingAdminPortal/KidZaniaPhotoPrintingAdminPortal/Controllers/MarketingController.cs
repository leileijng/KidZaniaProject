using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KidZaniaPhotoPrintingAdminPortal.Controllers
{
    public class MarketingController : Controller
    {
        [Authorize(Roles = "Admin,Marketing")]
        public ActionResult Dashboard()
        {
            ViewBag.Title = "Dashboard";

            return View();
        }
    }
}
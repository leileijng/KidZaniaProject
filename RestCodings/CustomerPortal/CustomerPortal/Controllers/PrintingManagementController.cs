using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace CustomerPortal.Controllers
{
    public class PrintingManagementController : Controller
    {
        public ActionResult Hardcopy()
        {
            ViewBag.Title = "Hardcopy Printint";
            return View();
        }
        public ActionResult Others()
        {
            ViewBag.Title = "Keychain Magnet Printing";
            return View();
        }
        public ActionResult PausedOrder()
        {
            ViewBag.Title = "PausedOrder";
            return View();
        }
    }
}
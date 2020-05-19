using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace CustomerPortal.Controllers
{
    public class ItemManagementController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Index";
            return View();
        }
    }
}
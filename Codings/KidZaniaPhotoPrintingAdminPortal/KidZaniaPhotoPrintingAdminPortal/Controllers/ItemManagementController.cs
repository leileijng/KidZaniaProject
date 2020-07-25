using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace KidZaniaPhotoPrintingAdminPortal.Controllers
{
    public class ItemManagementController : Controller
    {
        [Authorize(Roles = "Admin,Marketing")]
        public ActionResult Index()
        {
            ViewBag.Title = "Index";
            return View();
        }
    }
}
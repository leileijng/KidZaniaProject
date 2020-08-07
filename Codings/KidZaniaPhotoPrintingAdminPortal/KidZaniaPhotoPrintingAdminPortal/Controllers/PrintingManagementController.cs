using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using KidZaniaPhotoPrintingAdminPortal.Models;

namespace KidZaniaPhotoPrintingAdminPortal.Controllers
{
    public class PrintingManagementController : Controller
    {
        [Authorize(Roles = "Admin,Hardcopy")]
        public ActionResult Hardcopy()
        {
            ViewBag.Title = "Hardcopy Printing";
            return View();
        }

        [Authorize(Roles = "Admin,Keychain & Magnet")]
        public ActionResult Others()
        {
            ViewBag.Title = "Keychain Magnet Printing";
            return View();
        }

        [Authorize(Roles = "Admin,Hardcopy,Keychain & Magnet")]
        public ActionResult PausedOrder()
        {
            ViewBag.Title = "Paused Order";
            return View();
        }
    }
}
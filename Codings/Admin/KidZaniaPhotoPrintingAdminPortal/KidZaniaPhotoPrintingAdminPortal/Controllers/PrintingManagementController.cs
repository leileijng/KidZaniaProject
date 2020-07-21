using Neodynamic.SDK.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace KidZaniaPhotoPrintingAdminPortal.Controllers
{
    public class PrintingManagementController : Controller
    {
        public ActionResult Hardcopy()
        {
            ViewBag.WCPScript = Neodynamic.SDK.Web.WebClientPrint.CreateScript(Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme), "", HttpContext.Session.SessionID);
            ViewBag.Title = "Hardcopy Printing";
            return View();
        }
        public ActionResult Others()
        {
            ViewBag.WCPScript = Neodynamic.SDK.Web.WebClientPrint.CreateScript(Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme), Url.Action("PrintOthers", "PrintingManagement", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);
            ViewBag.Title = "Keychain Magnet Printing";
            return View();
        }
        public ActionResult PausedOrder()
        {
            ViewBag.Title = "Paused Order";
            return View();
        }

        [System.Web.Mvc.AllowAnonymous]
        public void PrintOthers(string files, string printerName, string ordertype)
        {
            if (ordertype == "keychain")
            {
                ClientPrintJob cpj = new ClientPrintJob();

                string[] keychain = files.Split(',');
                foreach (string newfile in keychain)
                {
                    string fileName = Guid.NewGuid().ToString("N") + "." + "jpg";
                    string filePath = "~/Content/OrderPhotos/Keychain/" + newfile;
                    PrintFile file = null;

                    //byte[] fileData = Convert.FromBase64String(newfile);

                    file = new PrintFile(System.Web.HttpContext.Current.Server.MapPath(filePath), fileName);
                    cpj.PrintFileGroup.Add(file);
                    if (printerName == "null")
                        cpj.ClientPrinter = new DefaultPrinter();
                    else
                        cpj.ClientPrinter = new InstalledPrinter(System.Web.HttpUtility.UrlDecode(printerName));
                }
                System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                System.Web.HttpContext.Current.Response.BinaryWrite(cpj.GetContent());
                System.Web.HttpContext.Current.Response.End();
            }
            else
            {
                string fileName = Guid.NewGuid().ToString("N") + "." + "jpg";
                string filePath = "~/Content/OrderPhotos/Magnet/" + files;
                PrintFile file = null;

                //byte[] fileData = Convert.FromBase64String(newfile);

                file = new PrintFile(System.Web.HttpContext.Current.Server.MapPath(filePath), fileName);
                ClientPrintJob cpj = new ClientPrintJob();
                cpj.PrintFile = file;
                if (printerName == "null")
                    cpj.ClientPrinter = new DefaultPrinter();
                else
                    cpj.ClientPrinter = new InstalledPrinter(System.Web.HttpUtility.UrlDecode(printerName));

                System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                System.Web.HttpContext.Current.Response.BinaryWrite(cpj.GetContent());
                System.Web.HttpContext.Current.Response.End();
            }
        }
    }
}
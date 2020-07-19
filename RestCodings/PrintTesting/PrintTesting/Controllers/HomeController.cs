using Neodynamic.SDK.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PrintTesting.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string url = Url.RouteUrl(
            "DefaultApi",
            new { httproute = "", controller = "printing" }
        );
            ViewBag.WCPScript = Neodynamic.SDK.Web.WebClientPrint.CreateScript(Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme), Url.Action(url), HttpContext.Session.SessionID);
           
            return View();
        }

        public ActionResult JSPrint()
        {
            return View();
        }

        

        [AllowAnonymous]
        public void PrintFile(string useDefaultPrinter, string printerName)
        {
            string fileName = Guid.NewGuid().ToString("N") + "." + "jpg";
            string filePath = "~/Content/1.jpg";


            if (filePath != null)
            {
                PrintFile file = null;
                file = new PrintFile(System.Web.HttpContext.Current.Server.MapPath(filePath), fileName);


                ClientPrintJob cpj = new ClientPrintJob();
                cpj.PrintFile = file;
                if (useDefaultPrinter == "checked" || printerName == "null")
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

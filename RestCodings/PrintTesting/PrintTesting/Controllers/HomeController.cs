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
            ViewBag.WCPScript = Neodynamic.SDK.Web.WebClientPrint.CreateScript(Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme), Url.Action("PrintFile", "Home", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);

            return View();
        }


        [AllowAnonymous]
        public void PrintFile()
        {
            Task.Run(async delegate
            {
                while (true)
                {
                    PrintFile file = new PrintFile(System.Web.HttpContext.Current.Server.MapPath("~/Content/1.jpg"), "1.jpg");
                    //Create a ClientPrintJob and send it back to the client!
                    ClientPrintJob cpj = new ClientPrintJob();
                    //set file to print...
                    cpj.PrintFile = file;
                    cpj.ClientPrinter = new InstalledPrinter("Canon iP8700 series (A5P1)");
                    Debug.WriteLine("Hi:" + cpj.GetContent());
                    System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                    System.Web.HttpContext.Current.Response.BinaryWrite(cpj.GetContent());
                    System.Web.HttpContext.Current.Response.End();
                    await Task.Delay(1000000000);
                }

            });
        }
        
    }
}

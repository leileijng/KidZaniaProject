using Neodynamic.SDK.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace PrintTesting.APIs
{
    public class PrintingController : ApiController
    {
        [HttpPost]
        [Route("api/printing")]
        public IHttpActionResult uploadFile(string printername)
        {
            var httpContext = HttpContext.Current;
            try
            {
                // Check for any uploaded file  
                if (httpContext.Request.Files.Count > 0)
                {
                    //Loop through uploaded files  
                    for (int i = 0; i < httpContext.Request.Files.Count; i++)
                    {
                        HttpPostedFile httpPostedFile = httpContext.Request.Files[i];
                        if (httpPostedFile != null)
                        {


                            InstalledPrinter installedPrinter = new InstalledPrinter();
                            installedPrinter.PaperName = "A4";
                            installedPrinter.PrinterName = printername;
                            Stream fs = httpPostedFile.InputStream;
                            BinaryReader br = new BinaryReader(fs);
                            byte[] bytes = br.ReadBytes((Int32)fs.Length);
                            //Create a PrintFile object with the image file
                            PrintFile file = new PrintFile(System.Web.HttpContext.Current.Server.MapPath("~/Content/1.jpg"), httpPostedFile.FileName);
                            //Create a ClientPrintJob and send it back to the client!
                            ClientPrintJob cpj = new ClientPrintJob();
                            //set file to print...
                            cpj.PrintFile = file;
                            cpj.ClientPrinter = new InstalledPrinter(printername);
                            Debug.WriteLine("Hi:" + cpj.GetContent());
                            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                            System.Web.HttpContext.Current.Response.BinaryWrite(cpj.GetContent());
                            System.Web.HttpContext.Current.Response.End();
                            return Ok();
                        }

                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return BadRequest();
        }
    }
}

using Neodynamic.SDK.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace PrintTesting.APIs
{
    public class PrintingController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/printing")]
        public HttpResponseMessage uploadFile(string printername)
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
                            string fileName = Guid.NewGuid().ToString("N") + "." + "jpg";
                            string filePath = "~/Content/1.jpg";


                            if (filePath != null)
                            {
                                PrintFile file = null;
                                file = new PrintFile(System.Web.HttpContext.Current.Server.MapPath(filePath), fileName);


                                ClientPrintJob cpj = new ClientPrintJob();
                                cpj.PrintFile = file;
                                cpj.ClientPrinter = new DefaultPrinter();

                                System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                                System.Web.HttpContext.Current.Response.BinaryWrite(cpj.GetContent());
                                System.Web.HttpContext.Current.Response.End();
                            }
                            //InstalledPrinter installedPrinter = new InstalledPrinter();
                            //installedPrinter.PaperName = "A4";
                            //installedPrinter.PrinterName = printername;
                            //Stream fs = httpPostedFile.InputStream;
                            //BinaryReader br = new BinaryReader(fs);
                            //byte[] bytes = br.ReadBytes((Int32)fs.Length);
                            ////Create a PrintFile object with the image file
                            //PrintFile file = new PrintFile(System.Web.HttpContext.Current.Server.MapPath("~/Content/1.jpg"), httpPostedFile.FileName);
                            ////Create a ClientPrintJob and send it back to the client!
                            //ClientPrintJob cpj = new ClientPrintJob();
                            ////set file to print...
                            //cpj.PrintFile = file;
                            //cpj.ClientPrinter = new InstalledPrinter(printername);
                            //HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                            //Stream stream = new MemoryStream(cpj.GetContent());
                            //result.Content = new StreamContent(stream);
                            //result.Content.Headers.ContentType =
                            //    new MediaTypeHeaderValue("application/octet-stream");
                            //return result;
                        }

                    }
                }
            }
            catch (Exception e)
            {
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.BadRequest);

                return result;
            }
            HttpResponseMessage result1 = new HttpResponseMessage(HttpStatusCode.OK);

            return result1;
        }
    }
}

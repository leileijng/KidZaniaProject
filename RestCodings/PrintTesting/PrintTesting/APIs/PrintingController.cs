using Neodynamic.SDK.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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


        [HttpPost]
        [Route("api/startPrinting")]
        [AllowAnonymous]
        public IHttpActionResult startPrinting()
        {
            var httpContext = HttpContext.Current;
            try
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
                            return Ok("Job Created");
                        

                    
                }
            
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("api/printingJobs")]
        public IHttpActionResult GetPrintingJobs()
        {
            try
            {
                Task.Run(async delegate
                {
                    while (true)
                    {
                        string searchQuery = "SELECT * FROM Win32_PrintJob";
                        ManagementObjectSearcher searchPrintJobs = new ManagementObjectSearcher(searchQuery);
                        ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();
                        foreach (ManagementObject prntJob in prntJobCollection)
                        {
                            string jobName = prntJob.Properties["Name"].Value.ToString();
                            string jobStatus = Convert.ToString(prntJob.Properties["JobStatus"]?.Value);
                            Debug.WriteLine("name: " + jobName + "; status:" + jobStatus + "!");
                        }
                        Debug.WriteLine("Okay here");
                        await Task.Delay(100);
                    }
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
                // handle exception
            }
        }
    }
}

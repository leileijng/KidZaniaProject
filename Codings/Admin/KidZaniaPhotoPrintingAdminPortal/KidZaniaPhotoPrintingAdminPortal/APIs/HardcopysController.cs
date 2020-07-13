using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using KidZaniaPhotoPrintingAdminPortal.Models;
using Newtonsoft.Json.Linq;
using Neodynamic.SDK.Web;
using System.Management;

namespace KidZaniaPhotoPrintingAdminPortal.APIs
{
    public class HardcopysController : ApiController
    {
        private Database database = new Database();


        [HttpGet]
        [Route("api/hardcopys")]
        public IHttpActionResult getHardCopys()
        {
            try
            {
                var prod = database.itemphotoes.Where(i => i.lineitem.product_id.Equals("a5")).OrderBy(n => n.updated_at).OrderBy(m=>m.lineitem_id).Select(x => new
                {
                    order_id = x.lineitem.order.order_id,
                    lineItem_id = x.lineitem_id,
                    photo_id = x.itemphoto_id,
                    photo_qty = x.lineitem.quantity,
                    photo_path = x.photo,
                    photo_status = x.printing_status,
                    assigned_printer = x.printer.name,
                    lineitem_status = x.lineitem.status,
                    updated_time = x.updated_at,
                    lineItem_name = x.lineitem.product.name
                }
                ).ToList();
                return Ok(prod);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

       
        [HttpPut]
        [Route("api/hardcopys")]
        public IHttpActionResult updatePhotoStatus([FromBody]JObject data)
        {
            try
            {
                string photoId = data["photoId"].ToString();
                var itemPhoto = database.itemphotoes.SingleOrDefault(x => x.itemphoto_id == photoId);
                var status = data["status"].ToString();
                itemPhoto.printing_status = status;
                itemPhoto.updated_at = DateTime.Now;

                var lineItem = itemPhoto.lineitem;
                if(status == "Printing")
                {
                    lineItem.status = status;
                }
                else if (status == "Completed")
                {
                    var itemPhotos = database.itemphotoes.Where(x => x.lineitem_id == lineItem.lineitem_id);
                    bool allCompleted = true;
                    foreach(var photo in itemPhotos)
                    {
                        if(photo.printing_status != "Completed")
                        {
                            allCompleted = false;
                        }
                    }
                    if (allCompleted)
                    {
                        lineItem.status = "Completed";
                    }
                    else
                    {
                        lineItem.status = "Printing";
                    }
                }
                
                database.SaveChanges();
                return Ok(new { message = "Photo status has been updated to " + status });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }


        [HttpPut]
        [Route("api/hardcopys/{lineItemId}")]
        public IHttpActionResult updateLineItemStatus(string lineItemId, [FromBody]JObject data)
        {
            try
            {
                var lineitem = database.lineitems.SingleOrDefault(x => x.lineitem_id == lineItemId);
                lineitem.status = data["status"].ToString();

                var photos = database.itemphotoes.Where(i => i.lineitem_id == lineItemId).ToList();

                for (int i=0; i<photos.Count; i++)
                {
                    string photoId = photos[i].itemphoto_id;
                    var photoItem = database.itemphotoes.SingleOrDefault(m => m.itemphoto_id == photoId);
                    photoItem.printing_status = data["status"].ToString();
                }

                database.SaveChanges();
                return Ok(new { message = "Photo status has been updated to " + data["status"] });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

        [HttpPost]
        [Route("api/hardcopys/startPrintings")]
        public IHttpActionResult hereTryOut()
        {
                        string query = string.Format("SELECT * from Win32_Printer");

                        using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                        using (ManagementObjectCollection coll = searcher.Get())
                        {
                            try
                            {
                                foreach (ManagementObject printer in coll)
                                {
                                    string printerName = printer.Properties["Caption"].Value.ToString();
                                    if (printerName.Contains("A5"))
                                    {
                                        string portNumber = printer.Properties["PortName"].Value.ToString();
                            
                                        foreach (PropertyData property in printer.Properties)
                                        {
                                            Debug.WriteLine(string.Format("{0}: {1}", property.Name, property.Value));
                                        }
                                    }
                                }
                    return Ok();
                            }
                            catch (ManagementException ex)
                            {
                                Debug.WriteLine(ex.Message);
                    return BadRequest();
                            }
                        }
                        
        }

        [HttpPost]
        [Route("api/hardcopys/startPrinting")]
        public IHttpActionResult startPrinting()
        {
            try
            {
                Task.Run(async delegate
                {
                    while (true)
                    {
                        string query = string.Format("SELECT * from Win32_Printer");
                        List<string> availablePrinters = new List<string>();
                        using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                        using (ManagementObjectCollection coll = searcher.Get())
                        {
                            try
                            {
                                foreach (ManagementObject printer in coll)
                                {
                                    string printerName = printer.Properties["Caption"].Value.ToString();
                                    if (printerName.Contains("A5"))
                                    {
                                        printer localPrinter = new printer();
                                        
                                        int start = printerName.IndexOf("(") + 1;
                                        int end = printerName.IndexOf(")", start);
                                        localPrinter.printer_id = printerName.Substring(start, end - start);
                                        localPrinter.name = printerName;
                                        string portNumber = printer.Properties["PortName"].Value.ToString();
                                        localPrinter.port = portNumber;
                                        if (printer.Properties["WorkOffline"].Value.ToString().Equals("False"))
                                        {
                                            // the printer in connected
                                            localPrinter.status = true;
                                            //check error!!!
                                            int printerState = int.Parse(printer.Properties["PrinterState"].Value.ToString());
                                            switch (printerState)
                                            {
                                                case 16:
                                                    localPrinter.error = "Out of Paper";
                                                    break;
                                                case 5:
                                                    localPrinter.error = "Out of Paper";
                                                    break;
                                                case 4:
                                                    localPrinter.error = "Paper Jam";
                                                    break;
                                                case 144:
                                                    localPrinter.error = "Out of Paper";
                                                    break;
                                                case 4194432:
                                                    localPrinter.error = "Lid Open";
                                                    break;
                                                default:
                                                    localPrinter.error = null;
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            localPrinter.status = false;
                                            //check error!!!
                                        }
                                        localPrinter.updated_at = DateTime.Now;
                                        var foundPrinter = database.printers.SingleOrDefault(i => i.printer_id.Equals(localPrinter.printer_id));
                                        if (foundPrinter != null)
                                        {
                                            foundPrinter.name = localPrinter.name;
                                            foundPrinter.port = localPrinter.port;
                                            foundPrinter.status = localPrinter.status;
                                            foundPrinter.updated_at = DateTime.Now;
                                            foundPrinter.error = localPrinter.error;
                                        }
                                        else
                                        {
                                            database.printers.Add(localPrinter);
                                        }
                                        database.SaveChanges();
                                    }
                                }
                            }
                            catch (ManagementException ex)
                            {
                                Debug.WriteLine(ex.Message);
                            }
                        }
                        leastJobs(availablePrinters);
                        await Task.Delay(1000);
                    }
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        public string leastJobs(List<string> availablePrinters)
        {
            string searchQuery = "SELECT * FROM Win32_PrintJob";
            ManagementObjectSearcher searchPrintJobs = new ManagementObjectSearcher(searchQuery);
            ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();
            foreach (ManagementObject prntJob in prntJobCollection)
            {
                string jobName = prntJob.Properties["Name"].Value.ToString();
                string jobStatus = Convert.ToString(prntJob.Properties["JobStatus"]?.Value);
                foreach(var pro in prntJob.Properties)
                {
                    Debug.WriteLine("property: " + pro.Name + "; value:" + pro.Value);
                }
                
                Debug.WriteLine("name: " + jobName + "; status:" + jobStatus + "!");
            }
            return ("ss");
        }
    }
}
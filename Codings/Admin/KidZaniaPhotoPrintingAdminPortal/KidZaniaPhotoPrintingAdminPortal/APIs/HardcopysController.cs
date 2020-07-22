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
using System.Drawing.Printing;
using System.Drawing;

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
                var prod = database.itemphotoes.Where(i => i.lineitem.product_id.Equals("a5") && !i.lineitem.status.Equals("Unpaid")).OrderBy(n => n.updated_at).OrderBy(m => m.lineitem_id).Select(x => new
                {
                    order_id = x.lineitem.order.order_id,
                    lineItem_id = x.lineitem_id,
                    photo_id = x.itemphoto_id,
                    photo_qty = x.lineitem.quantity,
                    photo_path = x.photo,
                    photo_status = x.printing_status,
                    assigned_printer = x.assigned_printer_id,
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

        [HttpGet]
        [Route("api/hardcopys/getPhotoByOrderId/{orderId}")]
        public IHttpActionResult getHardCopysByOrder(string orderId)
        {
            try
            {
                var photos = database.itemphotoes.Where(i => i.lineitem.product_id.Equals("a5") && i.order.order_id.Equals(orderId)).OrderBy(n => n.updated_at).Select(x => new
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
                return Ok(photos);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpGet]
        [Route("api/hardcopys/waitingPhotos")]
        public IHttpActionResult getwaitingPhotos()
        {
            try
            {
                var waitingPhotos = database.itemphotoes.Where(i => i.lineitem.product_id.Equals("a5") && i.printing_status.Equals("Waiting")).OrderBy(n => n.updated_at).OrderBy(m => m.lineitem_id).Select(x => new
                {
                    photo_id = x.itemphoto_id
                }).ToList();
                return Ok(waitingPhotos);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpGet]
        [Route("api/hardcopys/printerInfo")]
        public IHttpActionResult getprinterInfo()
        {
            try
            {
                var printers = database.printers.Where(i => i.name.Contains("A5")).Select(x => new
                {
                    printerId = x.printer_id,
                    printerName = x.name,
                    port = x.port,
                    error = x.error,
                    status = x.status,
                    manuallyOff = x.manuallyOff
                }
                ).ToList();
                return Ok(printers);
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
                if (status == "Printing")
                {
                    lineItem.status = status;
                }
                else if (status == "Completed")
                {
                    var itemPhotos = database.itemphotoes.Where(x => x.lineitem_id == lineItem.lineitem_id);
                    bool allCompleted = true;
                    foreach (var photo in itemPhotos)
                    {
                        if (photo.printing_status != "Completed")
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
        [Route("api/hardcopys/{orderCode}")]
        public IHttpActionResult updateLineItemStatus(string orderCode, [FromBody]JObject data)
        {
            try
            {
                var lineitem = database.lineitems.SingleOrDefault(x => x.product.product_id.Equals("a5") && x.order.order_id == orderCode);
                lineitem.status = data["status"].ToString();
                string lineItemId = lineitem.lineitem_id;
                var photos = database.itemphotoes.Where(i => i.lineitem_id == lineItemId).ToList();

                for (int i = 0; i < photos.Count; i++)
                {
                    string photoId = photos[i].itemphoto_id;
                    var photoItem = database.itemphotoes.SingleOrDefault(m => m.itemphoto_id == photoId);
                    photoItem.printing_status = data["status"].ToString();
                }

                database.SaveChanges();
                return Ok(new { message = "Photo status has been updated to " + data["status"] + "!" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

        [HttpPost]
        [Route("api/hardcopys/updatePrinterInfo")]
        public IHttpActionResult updatePrinterInfoAsync()
        {
            try
            {
                Task.Run(async delegate
                {
                    while (true)
                    {
                        string query = string.Format("SELECT * from Win32_Printer");
                        using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                        using (ManagementObjectCollection coll = searcher.Get())
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
                                    await Task.Delay(100);
                                }
                            }
                        }
                    }
                });
                return Ok();

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }


        [HttpPost]
        [Route("api/hardcopys/updatePrintingStatus")]
        public IHttpActionResult updatePrintingJobStatus()
        {
            string searchQuery = "SELECT * FROM Win32_PrintJob";
            try
            {
                Task.Run(async delegate
                {
                    while (true)
                    {
                        ManagementObjectSearcher searchPrintJobs = new ManagementObjectSearcher(searchQuery);
                        ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();

                        foreach (ManagementObject prntJob in prntJobCollection)
                        {
                            string jobDocument = prntJob.Properties["Document"].Value.ToString();
                            string jobStatus = Convert.ToString(prntJob.Properties["JobStatus"]?.Value);
                            string jobPrinter = prntJob.Properties["Caption"].Value.ToString();
                            string[] jobNames = jobDocument.Split('_');
                            string orderCode = jobNames[0];
                            string fileName = jobNames[1];
                            var photoItem = database.itemphotoes.SingleOrDefault(x => x.lineitem_id.Equals(orderCode + "_a5") && x.photo.Contains(fileName));
                            if (photoItem != null)
                            {
                                photoItem.printing_status = jobStatus;
                                if (jobStatus.Equals("Printing"))
                                {
                                    photoItem.lineitem.status = "Printing";
                                }
                                photoItem.updated_at = DateTime.Now;
                            }
                            else
                            {
                                Debug.WriteLine("It cannot get the item photo");
                            }
                            database.SaveChanges();
                        }


                        var printingPhotos = database.itemphotoes.Where(x => x.printing_status.Equals("Printing") && x.lineitem.product.product_id.Equals("a5")).ToList();

                        for (int i = 0; i < printingPhotos.Count; i++)
                        {
                            bool complete = true;
                            string photoPath = printingPhotos[i].photo;
                            string[] photoNames = printingPhotos[i].photo.Split('/');
                            string photoName = photoNames[photoNames.Length - 1];
                            string printingDocument = printingPhotos[i].order.order_id + "_" + photoName;

                            ManagementObjectSearcher searchPrintingJobs = new ManagementObjectSearcher(searchQuery);
                            ManagementObjectCollection prntingJobCollection = searchPrintingJobs.Get();
                            foreach (ManagementObject prntJob in prntingJobCollection)
                            {
                                string jobDocument = prntJob.Properties["Document"].Value.ToString();
                                if (jobDocument.Equals(printingDocument))
                                {
                                    complete = false;
                                }
                            }
                            if (complete)
                            {
                                printingPhotos[i].printing_status = "Completed";
                                int lineItemComplete = 0;
                                var photolineItemId = printingPhotos[i].lineitem.lineitem_id;
                                var checkStatusPhotos = database.itemphotoes.Where(x => x.lineitem.lineitem_id.Equals(photolineItemId)).ToList();
                                foreach (var p in checkStatusPhotos)
                                {
                                    if (p.printing_status.Equals("Completed"))
                                    {
                                        lineItemComplete++;
                                    }
                                }
                                if (lineItemComplete == printingPhotos[i].lineitem.quantity)
                                {
                                    printingPhotos[i].lineitem.status = "Completed";
                                }
                            }
                        }
                        database.SaveChanges();
                        await Task.Delay(1000);
                    }
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        
        
        [HttpPost]
        [Route("api/hardcopys/autoPrinting")]
        public IHttpActionResult AutoPrinting([FromBody]JObject data)
        {
            string photoId = data["photoId"].ToString();
            Debug.WriteLine("Now Print " + photoId);
            try
            {
                var photos = database.itemphotoes.SingleOrDefault(i => i.itemphoto_id.Equals(photoId));

                List<string> availablePris = availablePrinters();
                string bestPrinterId = leastJobs(availablePris);
                string bestPrinter = database.printers.SingleOrDefault(p => p.printer_id.Equals(bestPrinterId)).name.ToString();

                photos.assigned_printer_id = bestPrinterId;
                photos.printing_status = "Queueing";
                photos.updated_at = DateTime.Now;

                if (bestPrinter != null && bestPrinter != "")
                {
                    database.SaveChanges();
                    string photoPath = photos.photo;
                    string[] photoNames = photos.photo.Split('/');
                    string photoName = photoNames[photoNames.Length - 1];
                    string orderCode = photos.order.order_id;

                    PrintDocument pd = new PrintDocument();
                    pd.DocumentName = orderCode + "_" + photoName;
                    pd.PrintController = new StandardPrintController();
                    string current_photo_path = System.Web.HttpContext.Current.Server.MapPath("~/Content/Photos/" + photoName);
                    pd.PrintPage += (sndr, args) =>
                    {
                        System.Drawing.Image i = System.Drawing.Image.FromFile(current_photo_path);
                        System.Drawing.Rectangle m = args.MarginBounds;
                        if (i.Width > i.Height)
                            i.RotateFlip(RotateFlipType.Rotate90FlipNone);


                        //Logic below maintains Aspect Ratio.
                        if ((double)i.Width / (double)i.Height > (double)m.Width / (double)m.Height) // image is wider
                        {
                            m.Height = (int)((double)i.Height / (double)i.Width * (double)m.Width);
                        }
                        else
                        {
                            m.Width = (int)((double)i.Width / (double)i.Height * (double)m.Height);
                        }
                        //Calculating optimal orientation.
                        //pd.DefaultPageSettings.Landscape = m.Width > m.Height;

                        //Putting image in center of page.
                        m.Y = (int)((((System.Drawing.Printing.PrintDocument)(sndr)).DefaultPageSettings.PaperSize.Height - m.Height) / 2);
                        m.X = (int)((((System.Drawing.Printing.PrintDocument)(sndr)).DefaultPageSettings.PaperSize.Width - m.Width) / 2);


                        if ((i.Width == 6000 && i.Height == 4000) || (i.Width == 4000 && i.Height == 6000))
                            args.Graphics.DrawImage(i, 120, -5, m.Width - 70, m.Height - 130);
                        else
                            args.Graphics.DrawImage(i, 120, -5, m.Width - 70, m.Height - 80);

                    };
                    pd.PrinterSettings.PrinterName = bestPrinter;
                    
                    pd.Print();
                    return Ok();
                }
                else
                {
                    return BadRequest("No Printer is found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }

        }

        
        public List<string> availablePrinters()
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
                                if (!foundPrinter.manuallyOff && foundPrinter.status && foundPrinter.error == null)
                                {
                                    availablePrinters.Add(foundPrinter.printer_id);
                                }
                            }
                            else
                            {
                                database.printers.Add(localPrinter);
                                if (localPrinter.status && localPrinter.error == null)
                                {
                                    availablePrinters.Add(localPrinter.printer_id);
                                }
                            }
                            database.SaveChanges();
                        }
                    }
                    return availablePrinters;
                }
                catch (ManagementException ex)
                {
                    Debug.WriteLine(ex.Message);
                    return null;
                }
            }

        }


        public string leastJobs(List<string> availablePrinters)
        {
            int[] numberOfJobs = new int[availablePrinters.Count];
            for (int i = 0; i < numberOfJobs.Length; i++)
            {
                numberOfJobs[i] = 0;
            }
            string searchQuery = "SELECT * FROM Win32_PrintJob";
            ManagementObjectSearcher searchPrintJobs = new ManagementObjectSearcher(searchQuery);
            ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();
            foreach (ManagementObject prntJob in prntJobCollection)
            {
                string jobName = prntJob.Properties["Name"].Value.ToString();
                string jobStatus = Convert.ToString(prntJob.Properties["JobStatus"]?.Value);
                string jobPrinter = prntJob.Properties["Caption"].Value.ToString();
                for (int i = 0; i < availablePrinters.Count; i++)
                {
                    if (jobPrinter.Contains(availablePrinters[i]))
                    {
                        numberOfJobs[i]++;
                    }
                }
                /*
                foreach (var pro in prntJob.Properties)
                {
                    Debug.WriteLine("property: " + pro.Name + "; value:" + pro.Value);
                }
                */
            }
            int leastNumberOfJob = numberOfJobs[0];
            int leastNumberOfJobId = 0;
            for (int i = 0; i < numberOfJobs.Length; i++)
            {
                if (numberOfJobs[i] <= leastNumberOfJob)
                {
                    leastNumberOfJob = numberOfJobs[i];
                    leastNumberOfJobId = i;
                }
            }
            string bestPrinter = availablePrinters[leastNumberOfJobId];

            return bestPrinter;
        }

    }
}
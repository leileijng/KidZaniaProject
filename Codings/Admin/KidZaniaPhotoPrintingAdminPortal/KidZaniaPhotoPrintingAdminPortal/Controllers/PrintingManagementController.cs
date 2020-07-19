using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using KidZaniaPhotoPrintingAdminPortal.Models;
using Neodynamic.SDK.Web;

namespace KidZaniaPhotoPrintingAdminPortal.Controllers
{
    public class PrintingManagementController : Controller
    {
        private Database database = new Database();

        public ActionResult Hardcopy()
        {
            ViewBag.WCPScript = Neodynamic.SDK.Web.WebClientPrint.CreateScript(Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme), Url.Action("AutoPrinting", "PrintingManagement", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);
            ViewBag.Title = "Hardcopy Printing";
            return View();
        }
        public ActionResult Others()
        {
            ViewBag.Title = "Keychain Magnet Printing";
            return View();
        }
        public ActionResult PausedOrder()
        {
            ViewBag.Title = "Paused Order";
            return View();
        }

        [System.Web.Mvc.AllowAnonymous]
        public void AutoPrinting(string photoId)
        {
            Debug.WriteLine("Now Print " + photoId);
            try
            {
                var photos = database.itemphotoes.SingleOrDefault(i => i.itemphoto_id.Equals(photoId));

                List<string> availablePris = availablePrinters();
                string bestPrinterId = leastJobs(availablePris);
                string bestPrinter = database.printers.SingleOrDefault(p => p.printer_id.Equals(bestPrinterId)).name.ToString();
                if (bestPrinter != null && bestPrinter != "")
                {
                    string photoPath = photos.photo;
                    string[] photoNames = photos.photo.Split('/');
                    string photoName = photoNames[photoNames.Length - 1];
                    string orderCode = photos.order.order_id;
                    //PrintFile file = new PrintFile(System.Web.HttpContext.Current.Server.MapPath("~" + photoPath), orderCode + "_" + photoName);
                    PrintFile file = new PrintFile(System.Web.HttpContext.Current.Server.MapPath("~/Content/AutoPrintingTest/" + photoName), orderCode + "_" + photoName);
                    //Create a ClientPrintJob and send it back to the client!

                    ClientPrintJob cpj = new ClientPrintJob();
                    //set file to print...
                    cpj.PrintFile = file;
                    cpj.ClientPrinter = new InstalledPrinter(bestPrinter);

                    //photos.printing_status = "Printing";
                    //photos.lineitem.status = "Printing";
                    photos.assigned_printer_id = bestPrinterId;
                    photos.printing_status = "Queueing";
                    photos.updated_at = DateTime.Now;

                    database.SaveChanges();

                    System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                    System.Web.HttpContext.Current.Response.BinaryWrite(cpj.GetContent());
                    System.Web.HttpContext.Current.Response.End();

                }
                else
                {
                    System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                    System.Web.HttpContext.Current.Response.Write("No Printer Available");
                    System.Web.HttpContext.Current.Response.End();
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message.ToString());
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
using KidZaniaPhotoPrintingAdminPortal.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace KidZaniaPhotoPrintingAdminPortal.APIs
{
    public class OthersOrderController : ApiController
    {
        private Database database = new Database();

        [Route("api/others")]
        [HttpGet]
        public IHttpActionResult getOrder()
        {
            try
            {
                var order = database.orders.Select(x => new
                {
                    order_id = x.order_id,
                    p_id = x.pid,
                    product = database.lineitems.Select(y => new
                    {
                        p_id = y.p_id,
                        product_id = y.product_id,
                        status = y.status,
                        lineitem_id = y.lineitem_id
                    }).Where(y => y.p_id == x.pid && (y.product_id == "ec" || y.product_id == "kc" || y.product_id == "mg")).ToList(),
                    status = x.status
                }
                ).Where(x => (x.status == "Waiting" || x.status == "Collected") && x.product.Count != 0).ToList();
                return Ok(order);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [Route("api/others/{id}")]
        [HttpGet]
        public IHttpActionResult getOrderById(string id)
        {
            try
            {
                var order = database.orders.Select(x => new
                {
                    order_id = x.order_id,
                    p_id = x.pid,
                    product = database.lineitems.Select(y => new
                    {
                        p_id = y.p_id,
                        product_id = y.product_id,
                        quantity = y.quantity,
                        productname = y.product.name,
                        lineitem_id = y.lineitem_id,
                        photos = y.photos,
                        status = y.status
                    }).Where(y => y.p_id == x.pid && (y.product_id == "ec" || y.product_id == "kc" || y.product_id == "mg")).ToList(),
                    status = x.status
                }
                ).SingleOrDefault(x => (x.status == "Waiting" || x.status== "Collected") && x.product.Count != 0 && x.order_id == id);
                return Ok(order);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [Route("api/others/{id}")]
        [HttpPut]
        public IHttpActionResult changeStatus(string id, string status)
        {
            try
            {
                var lineitems = database.lineitems.SingleOrDefault(y => y.lineitem_id == id);
                lineitems.status = status;
                var itemphotoes = database.itemphotoes.Where(y => y.lineitem_id == id).ToList();
                foreach (var itemphoto in itemphotoes)
                {
                    itemphoto.printing_status = status;
                }
                var order = lineitems.order;

                List<lineitem> linesInOrder = database.lineitems.Where(i => i.order.order_id == order.order_id).ToList();
                bool allCollected = true;

                for (int i = 0; i < linesInOrder.Count; i++)
                {
                    if (!linesInOrder[i].status.Equals("Collected"))
                    {
                        allCollected = false;
                    }
                }
                if (allCollected)
                {
                    order.status = "Ready";
                }
                database.SaveChanges();
                return Ok("Update Successful!");
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [Route("api/others/pause")]
        [HttpPut]
        public IHttpActionResult pauseOrder(string orderid)
        {
            try
            {
                var lineitems = database.lineitems.Where(y => y.lineitem_id.Contains(orderid) && (y.lineitem_id.Contains("mg") || y.lineitem_id.Contains("kc") || y.lineitem_id.Contains("ec"))).ToList();
                foreach(var oneline in lineitems)
                {
                    oneline.status = "Paused";
                }
                var itemphotoes = database.itemphotoes.Where(y => y.lineitem_id.Contains(orderid) && (y.lineitem_id.Contains("mg") || y.lineitem_id.Contains("kc") || y.lineitem_id.Contains("ec"))).ToList();
                foreach (var itemphoto in itemphotoes)
                {
                    itemphoto.printing_status = "Paused";
                }
                database.SaveChanges();
                return Ok("Pause order successful!");
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [Route("api/others/downloadImage/{order_id}")]
        [HttpPut]
        public IHttpActionResult UploadFile(string order_id, string photos, string path)
        {

            try
            {
                if (photos.Contains('|'))
                {
                    string[] photo = photos.Split('|');
                    foreach (string onephoto in photo)
                    {
                        string[] filename = onephoto.Split('/');
                        string newfilename = filename[filename.Length - 1];
                        if (File.Exists(Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename)))
                        {
                            File.Delete(Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename));
                        }
                        if (File.Exists(HostingEnvironment.MapPath(onephoto)))
                        {
                            File.Copy(HostingEnvironment.MapPath(onephoto), Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename));
                        }
                    }
                }
                else
                {
                    if (File.Exists(HostingEnvironment.MapPath(photos)))
                    {
                        string[] filename = photos.Split('/');
                        string newfilename = filename[filename.Length - 1];
                        if (File.Exists(Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename)))
                        {
                            File.Delete(Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename));
                        }
                        File.Copy(HostingEnvironment.MapPath(photos), Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename));
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok();
        }

        [Route("api/others/moveImage/{order_id}")]
        [HttpPut]
        public IHttpActionResult moveFile(string order_id, string photos, string product_id)
        {
            try
            {
                string path = "";
                string newpath = "";

                if (product_id == "mg")
                {
                    path = "~/Content/OrderPhotos/Magnet/";
                    newpath = "~/Content/OrderPhotos/Magnet/Completed_Magnet/";
                }
                else if (product_id == "kc")
                {
                    path = "~/Content/OrderPhotos/Keychain/";
                    newpath = "~/Content/OrderPhotos/Keychain/Completed_Keychain/";
                }

                if (photos.Contains('|'))
                {
                    string[] photo = photos.Split('|');
                    foreach (string onephoto in photo)
                    {
                        string[] filename = onephoto.Split('/');
                        string newfilename = filename[filename.Length - 1];
                        if (File.Exists(HostingEnvironment.MapPath(onephoto)))
                        {
                            if (File.Exists(Path.Combine(HostingEnvironment.MapPath(newpath), order_id + "_" + newfilename)))
                            {
                                File.Delete(Path.Combine(HostingEnvironment.MapPath(newpath), order_id + "_" + newfilename));
                            }
                            File.Copy(Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename), Path.Combine(HostingEnvironment.MapPath(newpath), order_id + "_" + newfilename));
                            File.Delete(Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename));
                        }
                    }
                }
                else
                {
                    if (File.Exists(HostingEnvironment.MapPath(photos)))
                    {
                        string[] filename = photos.Split('/');
                        string newfilename = filename[filename.Length - 1];
                        if (File.Exists(Path.Combine(HostingEnvironment.MapPath(newpath), order_id + "_" + newfilename)))
                        {
                            File.Delete(Path.Combine(HostingEnvironment.MapPath(newpath), order_id + "_" + newfilename));
                        }
                        File.Copy(Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename), Path.Combine(HostingEnvironment.MapPath(newpath), order_id + "_" + newfilename));
                        File.Delete(Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename));
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok();
        }

        [Route("api/others/removeImage/{order_id}")]
        [HttpPut]
        public IHttpActionResult removeFile(string order_id, string photos, string product_id)
        {
            try
            {
                string path = "";
                string newpath = "";

                if (product_id == "mg")
                {
                    path = "~/Content/OrderPhotos/Magnet/";
                    newpath = "~/Content/OrderPhotos/Magnet/Completed_Magnet/";
                }
                else if (product_id == "kc")
                {
                    path = "~/Content/OrderPhotos/Keychain/";
                    newpath = "~/Content/OrderPhotos/Keychain/Completed_Keychain/";
                }

                if (photos.Contains('|'))
                {
                    string[] photo = photos.Split('|');
                    foreach (string onephoto in photo)
                    {
                        string[] filename = onephoto.Split('/');
                        string newfilename = filename[filename.Length - 1];
                        if (File.Exists(HostingEnvironment.MapPath(onephoto)))
                        {
                            if (File.Exists(Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename)))
                            {
                                File.Delete(Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename));
                            }
                            File.Copy(Path.Combine(HostingEnvironment.MapPath(newpath), order_id + "_" + newfilename), Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename));
                            File.Delete(Path.Combine(HostingEnvironment.MapPath(newpath), order_id + "_" + newfilename));
                        }
                    }
                }
                else
                {
                    if (File.Exists(HostingEnvironment.MapPath(photos)))
                    {
                        string[] filename = photos.Split('/');
                        string newfilename = filename[filename.Length - 1];
                        if (File.Exists(Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename)))
                        {
                            File.Delete(Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename));
                        }
                        File.Copy(Path.Combine(HostingEnvironment.MapPath(newpath), order_id + "_" + newfilename), Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename));
                        File.Delete(Path.Combine(HostingEnvironment.MapPath(newpath), order_id + "_" + newfilename));
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok();
        }

        [HttpGet]
        [Route("api/others/getPrinters")]
        public IHttpActionResult getPrinters()
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
                        if (printerName.Contains("MG"))
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
                                    availablePrinters.Add(foundPrinter.name);
                                }
                            }
                            else
                            {
                                database.printers.Add(localPrinter);
                                if (localPrinter.status && localPrinter.error == null)
                                {
                                    availablePrinters.Add(localPrinter.name);
                                }
                            }
                            database.SaveChanges();
                        }
                    }
                    return Ok(availablePrinters);
                }
                catch (ManagementException ex)
                {
                    Debug.WriteLine(ex.Message);
                    return BadRequest(ex.ToString());
                }
            }
        }

        [HttpPost]
        [Route("api/others/printKeychain")]
        public IHttpActionResult printKeychain(string printername)
        {
            try
            {
                var httpContext = HttpContext.Current;
                // Check for any uploaded file  
                if (httpContext.Request.Files.Count == 2)
                {
                    short printcopy = 1;
                    PrinterSettings ps = new PrinterSettings();
                    PrintDocument recordDoc = new PrintDocument();
                    ps.PrinterName = printername;
                    recordDoc.PrinterSettings = ps;
                    recordDoc.PrinterSettings.Copies = printcopy;

                    IEnumerable<PaperSize> paperSizes = ps.PaperSizes.Cast<PaperSize>();
                    PaperSize sizeA5 = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A5); // setting paper size to A5 size
                    recordDoc.DefaultPageSettings.PaperSize = sizeA5;

                    PrintDocument pd = new PrintDocument();
                    pd.PrinterSettings.Copies = printcopy;
                    pd.PrinterSettings.PrinterName = printername;

                    HttpPostedFile httpPostedFile1 = httpContext.Request.Files[0];
                    HttpPostedFile httpPostedFile2 = httpContext.Request.Files[1];

                    pd.PrintPage += (sndr, args) =>
                        {
                            System.Drawing.Image i = Image.FromStream(httpPostedFile1.InputStream, true, true);
                            //Adjust the size of the image to the page to print the full image without loosing any part of the image.
                            Rectangle m = args.MarginBounds;

                            if (i.Height > i.Width)
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
                            //Putting image in center of page.
                            m.Y = (int)((((System.Drawing.Printing.PrintDocument)(sndr)).DefaultPageSettings.PaperSize.Height - m.Height) / 2);
                            m.X = (int)((((System.Drawing.Printing.PrintDocument)(sndr)).DefaultPageSettings.PaperSize.Width - m.Width) / 2);
                            //args.Graphics.DrawImage(i, 260, 80, m.Width - 360, m.Height - 270); //Keychain size
                            //args.Graphics.DrawImage(i, 260, 80, m.Width - 350, m.Height - 265); //Keychain size
                            args.Graphics.DrawImage(i, 260, 80, m.Width - 340, m.Height - 260); //Keychain size
                                                                                                //                        Position    Size   

                            System.Drawing.Image i2 = Image.FromStream(httpPostedFile2.InputStream, true, true);


                            Rectangle m2 = args.MarginBounds;
                            if (i2.Height > i2.Width)
                                i2.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            if ((double)i2.Width / (double)i2.Height > (double)m2.Width / (double)m2.Height) // image is wider
                            {
                                m2.Height = (int)((double)i2.Height / (double)i2.Width * (double)m2.Width);
                            }
                            else
                            {
                                m2.Width = (int)((double)i2.Width / (double)i2.Height * (double)m2.Height);
                            }
                            //Calculating optimal orientation.
                            m2.Y = (int)((((System.Drawing.Printing.PrintDocument)(sndr)).DefaultPageSettings.PaperSize.Height - m2.Height) / 2);
                            m2.X = (int)((((System.Drawing.Printing.PrintDocument)(sndr)).DefaultPageSettings.PaperSize.Width - m2.Width) / 2);
                            //args.Graphics.DrawImage(i2, 260, 310, m2.Width - 360, m2.Height - 270); //landscape
                            //args.Graphics.DrawImage(i2, 260, 310, m2.Width - 350, m2.Height - 265); //landscape
                            args.Graphics.DrawImage(i2, 260, 310, m2.Width - 340, m2.Height - 260); //landscape
                                                                                                    //                        Position    Size
                                                                                                    //args.Graphics.DrawImage(i, m);
                                                                                                    //                         X    Y 
                        };
                    //pd.PrintPage += PrintPage;
                    pd.Print();
                    return Ok(new { message = "Print successful!" });
                }
                else
                {
                    return BadRequest("Please submit only two files!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost]
        [Route("api/others/printMagnet")]
        public IHttpActionResult printMagnet(string printername)
        {
            try
            {
                var httpContext = HttpContext.Current;
                HttpPostedFile httpPostedFile = httpContext.Request.Files[0];

                short printcopy = 1;
                //txtcopies
                PrinterSettings ps = new PrinterSettings();
                PrintDocument recordDoc = new PrintDocument();
                ps.PrinterName = printername;
                recordDoc.PrinterSettings = ps;
                recordDoc.PrinterSettings.Copies = printcopy;

                IEnumerable<PaperSize> paperSizes = ps.PaperSizes.Cast<PaperSize>();
                PaperSize sizeA5 = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A5); // setting paper size to A5 size
                recordDoc.DefaultPageSettings.PaperSize = sizeA5;


                PrintDocument pd = new PrintDocument();
                pd.PrinterSettings.Copies = printcopy;
                pd.PrinterSettings.PrinterName = printername;

                pd.PrintPage += (sndr, args) =>
                {
                    System.Drawing.Image i = Image.FromStream(httpPostedFile.InputStream, true, true);
                    //Adjust the size of the image to the page to print the full image without loosing any part of the image.
                    System.Drawing.Rectangle m = args.MarginBounds;

                    if (i.Height > i.Width)
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
                    //Putting image in center of page.
                    m.Y = (int)((((System.Drawing.Printing.PrintDocument)(sndr)).DefaultPageSettings.PaperSize.Height - m.Height) / 2);
                    m.X = (int)((((System.Drawing.Printing.PrintDocument)(sndr)).DefaultPageSettings.PaperSize.Width - m.Width) / 2);
                    //args.Graphics.DrawImage(i, 260, 80, m.Width - 360, m.Height - 270); //Keychain size
                    //args.Graphics.DrawImage(i, 260, 80, m.Width - 350, m.Height - 265); //Keychain size
                    //args.Graphics.DrawImage(i, 260, 80, m.Width - 340, m.Height - 260); //Keychain size V1.003
                    args.Graphics.DrawImage(i, 204, -5, m.Width - 240, m.Height - 316); //Magnet Size
                                                                                        //                        Position    Size   


                    //args.Graphics.DrawImage(i, m);
                    //                         X    Y 
                };
                //pd.PrintPage += PrintPage;
                pd.Print();
                return Ok(new { message = "Print successful!" });
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

        }
    }
}

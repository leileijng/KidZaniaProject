using Neodynamic.SDK.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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


        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            if (image.Width > image.Height)
                image.RotateFlip(RotateFlipType.Rotate90FlipNone);

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            float imgWidth = image.Width;
            float imgHeight = image.Height;

            float paperRatio = (float)width / (float)height;

            //Logic below maintains Aspect Ratio.
            if (imgWidth / imgHeight > paperRatio) // image is wider
            {
                imgWidth = width;
                imgHeight = width / paperRatio;
            }
            else
            {
                imgHeight = height;
                imgWidth = height * paperRatio;
            }

            //Calculating optimal orientation.
            //pd.DefaultPageSettings.Landscape = m.Width > m.Height;

            //Putting image in center of page.
            int y = (int)((height - imgHeight) / 2);
            int x = (int)((width - imgWidth) / 2);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                
                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, x, y, imgWidth, imgHeight, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }


        public static Bitmap ResizeImage2(Image imgPhoto, int Width, int Height)
        {
            if (imgPhoto.Width > imgPhoto.Height)
                imgPhoto.RotateFlip(RotateFlipType.Rotate90FlipNone);

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            int margin = 15;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((Width -
                              (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((Height -
                              (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width, Height,
                              PixelFormat.Format32bppArgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                             imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.White);
            grPhoto.InterpolationMode =
                    InterpolationMode.HighQualityBicubic;
            grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;
            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX + margin, destY + margin, destWidth - 2 * margin, destHeight - 2 * margin),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            
            return bmPhoto;
        }

        public static Bitmap ResizeImage3(Image imgPhoto, int Width, int Height)
        {
            if (imgPhoto.Width > imgPhoto.Height)
                imgPhoto.RotateFlip(RotateFlipType.Rotate90FlipNone);

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            int margin = 15;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((Width -
                              (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((Height -
                              (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width, Height,
                              PixelFormat.Format32bppArgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                             imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.White);
            grPhoto.InterpolationMode =
                    InterpolationMode.HighQualityBicubic;
            grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;
            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();

            return bmPhoto;
        }


        [HttpPost]
        [Route("api/resizeImg1")]
        public IHttpActionResult ResizeDemo()
        {
            try
            {
                System.Drawing.Image i1 = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath("~/Content/1.jpg"));
                System.Drawing.Image i2 = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath("~/Content/1.jpg"));

                PrinterSettings ps = new PrinterSettings();
                IEnumerable<PaperSize> paperSizes = ps.PaperSizes.Cast<PaperSize>();
                PaperSize sizeA5 = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A5);

                Debug.WriteLine("Width: " + sizeA5.Width);
                Debug.WriteLine("Height: " + sizeA5.Height);

                Bitmap resultImage1 = ResizeImage2(i1, 2450, 1748);
                Bitmap resultImage2 = ResizeImage3(i2, 1748, 2480);

                resultImage1.Save(System.Web.HttpContext.Current.Server.MapPath("~/Content/test1.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
                resultImage2.Save(System.Web.HttpContext.Current.Server.MapPath("~/Content/test3.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
                return Ok();

            }
            catch(Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }


        [HttpPost]
        [Route("api/resizeImg")]
        public IHttpActionResult ResizeDemo2()
        {
            bool withMargin = false;
            try
            {//txtcopies
                
                PrintDocument pd = new PrintDocument();
                PrinterSettings ps = new PrinterSettings();

                
                if (!withMargin)
                {
                    Margins margins = new Margins(0, 0, 0, 0);
                    pd.DefaultPageSettings.Margins = margins;
                    pd.OriginAtMargins = true;
                    IEnumerable<PaperSize> paperSizes = ps.PaperSizes.Cast<PaperSize>();
                    PaperSize sizeA5 = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A5); // setting paper size to A5 size
                    pd.DefaultPageSettings.PaperSize = sizeA5;
                }
                
                string current_photo_path = System.Web.HttpContext.Current.Server.MapPath("~/Content/1.jpg");
                pd.PrintPage += (sndr, args) =>
                {
                    System.Drawing.Image i = System.Drawing.Image.FromFile(current_photo_path);
                    System.Drawing.Rectangle m = args.MarginBounds;
                    if (i.Width > i.Height)
                        i.RotateFlip(RotateFlipType.Rotate90FlipNone);

                    if (withMargin)
                    {
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
                                
                    }
                    else
                    {
                        Rectangle p = args.PageBounds;

                        if ((double)i.Width / (double)i.Height > (double)p.Width / (double)p.Height) // image is wider
                        {
                            p.Height = (int)((double)i.Height / (double)i.Width * (double)p.Width);
                        }
                        else
                        {
                            p.Width = (int)((double)p.Width / (double)p.Height * (double)p.Height);
                        }

                        if ((i.Width == 6000 && i.Height == 4000) || (i.Width == 4000 && i.Height == 6000))
                            args.Graphics.DrawImage(i, 120, -5, m.Width - 70, m.Height - 130);
                        else
                        {
                            //args.Graphics.DrawImage(i, 120, -5, m.Width - 280, m.Height - 300);
                            //args.Graphics.DrawImage(i, 120, -5, m.Width - 260, m.Height - 280);
                            args.Graphics.DrawImage(i, -10, -10, p.Width + 20, p.Height);

                        }

                    }

                };
                pd.PrinterSettings.PrinterName = "Canon iP8700 series (A5P2)";
                
                pd.Print();
                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

    }
}

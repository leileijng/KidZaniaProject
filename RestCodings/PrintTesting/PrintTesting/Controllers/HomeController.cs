using Neodynamic.SDK.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.Management;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PrintTesting.Controllers
{
    public class HomeController : Controller
    {
        public string orientation;
        public double xPosition;
        public double yPosition;
        public double width;
        public double height;

        public ActionResult Index()
        {
            string url = Url.RouteUrl(
            "DefaultApi",
            new { httproute = "", controller = "printing" }
        );
            ViewBag.WCPScript = Neodynamic.SDK.Web.WebClientPrint.CreateScript(Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme), Url.Action("PrintFile", "Home", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);
           
            return View();
        }

        public ActionResult JSPrint()
        {
            return View();
        }

        public void ResizeImage1(Image imgPhoto, int Width, int Height)
        {
            if (imgPhoto.Width > imgPhoto.Height)
            {
                orientation = "L";
            }
            else
            {
                orientation = "P";
            }
                int sourceWidth = imgPhoto.Width;
                int sourceHeight = imgPhoto.Height;
                int destX = 0;
                int destY = 0;

                float nPercent = 0;
                float nPercentW = 0;
                float nPercentH = 0;

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

            xPosition = destX;
            yPosition = destY;
            width = (float)destWidth / (float)96;
            height = (float)destHeight / (float)96;


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

            int margin = 8;

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
                new Rectangle(destX + margin, destY, destWidth - 2 * margin, destHeight - 2 * margin),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();

            return bmPhoto;
        }


        [AllowAnonymous]
        public void PrintFile()
        {

            string filePath = "~/Content/1.jpg";
            //System.Drawing.Image i1 = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath(filePath));
            
            //Bitmap resultImage1 = ResizeImage2(i1, 420, 595);

            //resultImage1.Save(System.Web.HttpContext.Current.Server.MapPath("~/Content/test1.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);

            string fileName = Guid.NewGuid().ToString("N") + "." + "jpg";
           
            if (filePath != null)
            {
                PrintFile file = null;
                file = new PrintFile(System.Web.HttpContext.Current.Server.MapPath("~/Content/test1.jpg"), fileName+ "-PX=1.2-PY=-3-PW=6.75-PH=8.3-PO=P.jpg");
                //file = new PrintFile(System.Web.HttpContext.Current.Server.MapPath("~/Content/test1.jpg"), fileName + "-PX=1.2-PY=-3-PW=6.75-PH=8.3-PO=P.jpg");
                //file = new PrintFile(System.Web.HttpContext.Current.Server.MapPath("~/Content/test1.jpg"), fileName + "-PX=0-PY=0-PW=5.85-PH=8.27-PO=P.jpg");
                ClientPrintJob cpj = new ClientPrintJob();
                cpj.PrintFile = file;
                cpj.ClientPrinter = new InstalledPrinter(System.Web.HttpUtility.UrlDecode("Canon iP8700 series (A5P2)"));
                InstalledPrinter ip = new InstalledPrinter(System.Web.HttpUtility.UrlDecode("Canon iP8700 series (A5P2)"));
                ip.PaperName = "PW=5.85-PH=8.27";



                System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                System.Web.HttpContext.Current.Response.BinaryWrite(cpj.GetContent());
                System.Web.HttpContext.Current.Response.End();
            }
        }
    }
}

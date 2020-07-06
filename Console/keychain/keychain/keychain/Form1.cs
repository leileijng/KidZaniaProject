using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kidzania_station_console_printers
{
    public partial class Form1 : Form
    {
        public static int image_pos = 0;
        public static List<string> hc_copy = new List<string>();
        public static string current_photo_path = "";

        public Form1()
        {
            InitializeComponent();

            //Insert printers into combo box
            foreach (var item in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cmb_printer.Items.Add(item.ToString());
            }
            cmb_printer.SelectedIndex = 0;

        }


        private void txtcopies_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }


        

        private void cmb_printer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string default_printer = cmb_printer.Items[cmb_printer.SelectedIndex].ToString();
            SetPrinters.SetDefaultPrinter(default_printer);
        }

        public static class SetPrinters
        {
            [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool SetDefaultPrinter(string Printer);

            
        }

        private void PrintPage(object o, PrintPageEventArgs args)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(current_photo_path);
            //Point loc = new Point(100, 100);
            //e.Graphics.DrawImage(img, loc);
            //e.Graphics.DrawImage(i, args.MarginBounds);
            Rectangle m = args.MarginBounds;

            if ((double)img.Width / (double)img.Height > (double)m.Width / (double)m.Height) // image is wider
            {
                m.Height = (int)((double)img.Height / (double)img.Width * (double)m.Width);
            }
            else
            {
                m.Width = (int)((double)img.Width / (double)img.Height * (double)m.Height);
            }
            //m.Y = (int)((((System.Drawing.Printing.PrintDocument)(sndr)).DefaultPageSettings.PaperSize.Height - m.Height) / 2);
            //m.X = (int)((((System.Drawing.Printing.PrintDocument)(sndr)).DefaultPageSettings.PaperSize.Width - m.Width) / 2);
            args.Graphics.DrawImage(img, m);
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        

        private void btn_print_Click(object sender, EventArgs e)
        {
            if (hc_copy.Count == 0)
                return;

            PrinterSettings ps = new PrinterSettings();
            PrintDocument recordDoc = new PrintDocument();
            recordDoc.PrinterSettings = ps;

            IEnumerable<PaperSize> paperSizes = ps.PaperSizes.Cast<PaperSize>();
            PaperSize sizeA5 = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A5); // setting paper size to A5 size
            recordDoc.DefaultPageSettings.PaperSize = sizeA5;

            
            PrintDocument pd = new PrintDocument();
            foreach (string hardcopy in hc_copy)
            {
                if (hardcopy.ToLower().StartsWith("c:") || hardcopy.ToLower().StartsWith("d:") || hardcopy.ToLower().StartsWith("e:") || hardcopy.ToLower().StartsWith("f:"))
                    current_photo_path = hardcopy;
                else
                    current_photo_path = @"\\152.10.200.26\Remote\Media\Image" + hardcopy.Replace("/", "\\");
                
                pd.PrintPage += (sndr, args) =>
                {
                    System.Drawing.Image i = System.Drawing.Image.FromFile(current_photo_path);
                    //i = resizeImage(i, new Size(50, 50));
                    //Adjust the size of the image to the page to print the full image without loosing any part of the image.
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
                    //args.Graphics.DrawImage(i, m);
                    //args.Graphics.DrawImage(i, 50, -10, m.Width +50, m.Height +80); //Old settings
                    
                    args.Graphics.DrawImage(i, 120, -5, m.Width - 70, m.Height - 80);
                    //                           X    Y 

                };
                //pd.PrintPage += PrintPage;
                pd.Print();
            }

            //pd.Print();

            hc_copy.Clear();
        }




        private static string get_orderdetails(string code, string custom_date)
        {
            string json = "";

            WebClient client = new WebClient();
            
            string url = "https://photos.kidzania.com.sg/station_request.ashx?key=da6ab2a146df53d76536549fc692e94a&type=order_detail&pid=" + code;
            if (custom_date != "")
                url = "https://photos.kidzania.com.sg/station_request.ashx?key=da6ab2a146df53d76536549fc692e94a&type=order_detail&pid=" + code + "&date=" + custom_date;
            string result = client.DownloadString(url).Trim('[', ']');
            dynamic json_obj = JsonConvert.DeserializeObject(result);

            if (json_obj != null)
            {
                string status = json_obj.status;

                int a5copy_num = 0;
                if (json_obj.a5copy[0] != "")
                    a5copy_num = json_obj.a5copy.Count;

                List<string> hardcopy_list = new List<string>();
                for(int x = 0; x < json_obj.a5copy.Count; x++)
                {
                    hardcopy_list.Add(json_obj.a5copy[x].ToString());
                    hc_copy.Add(json_obj.a5copy[x].ToString());
                }

                string purchase_status = json_obj.purchase_status;

                if (purchase_status == "")
                    purchase_status = "unpaid";

                string total = json_obj.total_price;

                string div_style = "font-size = 20px; border: 1px solid black;";
                string font_bold = "font-weight: bold;";

                string div_style2 = "font-size = 26px; border: 1px solid black;";
                string div_style3 = "font-size = 26px;";

                string status_style = "";
                if (purchase_status == "unpaid")
                    status_style = "color: red;";

                json = "<table style='width:400px; border: 1px solid black; border-collapse: collapse;'>";

                json += "<tr>";
                json += "<td style='" + div_style + font_bold + "'>Item</td><td style='" + div_style + font_bold + "'>Quantity</td>";
                json += "</tr>";

                json += "<tr>";
                json += "<td style='" + div_style + "'>Hardcopy</td><td style='" + div_style + "'>" + a5copy_num + "</td>";
                json += "</tr>";
                
                json += "</table>";
                json += "<div style='" + div_style3 + font_bold + "'>Status: <span style='" + status_style + "'>" + purchase_status + "</span></div>";


            }

            return json;
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (image_pos < hc_copy.Count - 1)
                image_pos++;
            else
                image_pos = 0;

            string image_path = "";
            string toprint = hc_copy[image_pos];
            if (toprint.ToLower().StartsWith("c:") || toprint.ToLower().StartsWith("d:") || toprint.ToLower().StartsWith("e:") || toprint.ToLower().StartsWith("f:"))
            {
                pic_holder.Image = Image.FromFile(toprint);
                image_path = toprint;
            }
            else
            {
                string image_fullpath = @"\\152.10.200.26\Remote\Media\Image" + toprint.Replace("/", "\\");
                pic_holder.Image = Image.FromFile(image_fullpath);
                image_path = image_fullpath;
            }


            System.Drawing.Image img = System.Drawing.Image.FromFile(image_path);
            double image_width = (double)img.Width;
            double image_height = (double)img.Height;
            string profile_name = "unknown";
            if (image_width == 2482 && image_height == 3550)
                profile_name = "Generic Template No RKs-01";
            if (image_width == 3550 && image_height == 2481)
                profile_name = "Generic Template No RKs-02";
            if (image_width == 2480 && image_height == 3508)
                profile_name = "MagazineCovers";
            if (image_width == 2480 && image_height == 1748)
                profile_name = "School Group - Landscape";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (hc_copy.Count == 0)
                return;

            short printcopy = 1;
            //txtcopies
            PrinterSettings ps = new PrinterSettings();
            PrintDocument recordDoc = new PrintDocument();
            recordDoc.PrinterSettings = ps;
            recordDoc.PrinterSettings.Copies = printcopy;

            IEnumerable<PaperSize> paperSizes = ps.PaperSizes.Cast<PaperSize>();
            PaperSize sizeA5 = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A5); // setting paper size to A5 size
            recordDoc.DefaultPageSettings.PaperSize = sizeA5;
            

            PrintDocument pd = new PrintDocument();
            string hardcopy_single = hc_copy[image_pos];
            pd.PrinterSettings.Copies = printcopy;

            if (hardcopy_single.ToLower().StartsWith("c:") || hardcopy_single.ToLower().StartsWith("d:") || hardcopy_single.ToLower().StartsWith("e:") || hardcopy_single.ToLower().StartsWith("f:"))
                current_photo_path = hardcopy_single;
            else
                current_photo_path = @"\\152.10.200.26\Remote\Media\Image" + hardcopy_single.Replace("/", "\\");

            pd.PrintPage += (sndr, args) =>
            {
                System.Drawing.Image i = System.Drawing.Image.FromFile(hc_copy[0]);
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
                args.Graphics.DrawImage(i, 260, 80, m.Width - 340, m.Height - 260); //Keychain size
                //                        Position    Size   

                if (hc_copy.Count > 1)
                {
                    System.Drawing.Image i2 = System.Drawing.Image.FromFile(hc_copy[1]);
                    System.Drawing.Rectangle m2 = args.MarginBounds;
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
                }
                //args.Graphics.DrawImage(i, m);
                //                         X    Y 
            };
            //pd.PrintPage += PrintPage;
            pd.Print();
            
        }

        private void btn_loadimages_Click(object sender, EventArgs e)
        {
            hc_copy.Clear();
            System.IO.Stream myStream;
            OpenFileDialog thisDialog = new OpenFileDialog();
            thisDialog.InitialDirectory = "c:\\";
            thisDialog.Filter = "rcc files (*.rcc)|*.rcc|All files (*.*)|*.*";
            thisDialog.FilterIndex = 2;
            thisDialog.RestoreDirectory = true;
            thisDialog.Multiselect = true;
            thisDialog.Title = "Please Select Source File(s) for Conversion";
            if (thisDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (String file in thisDialog.FileNames)
                {
                    try
                    {
                        if ((myStream = thisDialog.OpenFile()) != null)
                        {
                            using (myStream)
                            {
                                hc_copy.Add(file);
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    }
                }
            }

            if (hc_copy.Count > 0)
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(hc_copy[0]);
                double image_width = (double)img.Width;
                double image_height = (double)img.Height;
                if (image_width > image_height)
                {
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    pic_holder.Image = img;
                }
                else
                {
                    pic_holder.Image = Image.FromFile(hc_copy[0]);
                }

            }
                
            if (hc_copy.Count > 1)
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(hc_copy[1]);
                double image_width = (double)img.Width;
                double image_height = (double)img.Height;
                if (image_width > image_height)
                {
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    pic_holder2.Image = img;
                }
                else
                {
                    pic_holder2.Image = Image.FromFile(hc_copy[1]);
                }



            }






            //string profile_name = "unknown";
            //if (image_width == 2482 && image_height == 3550)
            //    profile_name = "Generic Template No RKs-01";
            //if (image_width == 3550 && image_height == 2481)
            //    profile_name = "Generic Template No RKs-02";
            //if (image_width == 2480 && image_height == 3508)
            //    profile_name = "MagazineCovers";
            //if (image_width == 2480 && image_height == 1748)
            //    profile_name = "School Group - Landscape";



            //txt_photodetails


            //CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            //dialog.InitialDirectory = "C:\\";
            //dialog.IsFolderPicker = true;
            //if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            //{
            //    txt_ecard_path.Text = dialog.FileName.ToString();
            //    //MessageBox.Show("You selected: " + dialog.FileName);
            //}
        }
        //List<System.Drawing.Size> _rectangleData = new List<System.Drawing.Size>;
        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //g.DrawRectangle(Pens.Red, new Rectangle(10, 10, 100, 75));
            Color myRgbColor70 = new Color();
            myRgbColor70 = Color.FromArgb(255, 255, 255);
            SolidBrush Brosh70 = new SolidBrush(myRgbColor70);
            g.FillRectangle(Brosh70, 10, 10, 650, 450);
        }


        public static Bitmap RotateImage(Image image, PointF offset, float angle)
        {
            if (image == null)
                throw new ArgumentNullException("image");

            //create a new empty bitmap to hold rotated image
            Bitmap rotatedBmp = new Bitmap(image.Width, image.Height);
            rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(rotatedBmp);

            //Put the rotation point in the center of the image
            g.TranslateTransform(offset.X, offset.Y);

            //rotate the image
            g.RotateTransform(angle);

            //move the image back
            g.TranslateTransform(-offset.X, -offset.Y);

            //draw passed in image onto graphics object
            g.DrawImage(image, new PointF(0, 0));

            return rotatedBmp;
        }


    }
}

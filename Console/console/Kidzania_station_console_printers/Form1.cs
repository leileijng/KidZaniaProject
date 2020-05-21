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
        private static string token = "da6ab2a146df53d76536549fc692e94a";
        public static int image_pos = 0;
        public static List<string> hc_copy = new List<string>();
        public static string current_photo_path = "";

        public Form1()
        {
            InitializeComponent();
            populate_customercode_list();

            //Insert printers into combo box
            foreach (var item in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cmb_printer.Items.Add(item.ToString());
            }
            cmb_printer.SelectedIndex = 0;

            txt_code.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCode_CheckEnterKeyPress);
            txtcopies.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtcopies_KeyPress);
        }

        private void txtCode_CheckEnterKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                btn_submit_Click(sender, e);
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
                e.Handled = true;
        }

        private void btn_searchcodes_Click(object sender, EventArgs e)
        {
            populate_customercode_list();
        }

        private void populate_customercode_list()
        {
            string ret_val = "";
            string tablerows = "";
            string div_style = "font-size = 20px; border: 1px solid black;";
            string font_bold = "font-weight: bold;";

            WebClient client = new WebClient();
            string url = "https://photos.kidzania.com.sg/station_request.ashx?key=" + token + "&type=customer_list_hardcopy";
            String result = client.DownloadString(url);
            if (result != "")
            {
                List<string> datalist = result.Split('|').ToList();
                foreach (string data in datalist)
                {
                    string code = data.Split(':')[0];
                    string payment_status = data.Split(':')[1];
                    if (payment_status != "complete")
                    {
                        tablerows += "<tr>";
                        tablerows += "<td style='" + div_style + "'>" + code + "</td><td style='" + div_style + "'>" + payment_status + "</td>";
                        tablerows += "</tr>";
                    }
                }
            }
            ret_val = "<table style='width:200px; border: 1px solid black; border-collapse: collapse;'>";
            ret_val += "<tr>";
            ret_val += "<td style='" + div_style + font_bold + "'>Code</td><td style='" + div_style + font_bold + "'>Status</td>";
            ret_val += "</tr>";
            ret_val += tablerows;
            ret_val += "</table>";
            wb_existingcodes.DocumentText = ret_val;
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
            Rectangle m = args.MarginBounds;
            if ((double)img.Width / (double)img.Height > (double)m.Width / (double)m.Height) // image is wider
            {
                m.Height = (int)((double)img.Height / (double)img.Width * (double)m.Width);
            }
            else
            {
                m.Width = (int)((double)img.Width / (double)img.Height * (double)m.Height);
            }
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
                if (char.IsLetter(hardcopy.Substring(0, 1).ToCharArray()[0]) && hardcopy.Substring(1, 1) == ":")
                    current_photo_path = hardcopy;
                else
                    current_photo_path = @"\\152.10.200.26\Remote\Media\Image" + hardcopy.Replace("/", "\\");
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
                    
                    args.Graphics.DrawImage(i, 120, -5, m.Width - 70, m.Height - 80); //Calibration
                };
                pd.Print();
            }

            populate_customercode_list();
            wb_photos.DocumentText = "";
            hc_copy.Clear();
        }

        private void update_hardcopy_status()
        {
            if (txt_code.Text == "")
                return;
            string code = "kidzsg" + txt_code.Text + DateTime.Now.ToString("ddMMyy").ToString();
            WebClient client = new WebClient();
            string url = "https://photos.kidzania.com.sg/station_request.ashx?key=" + token + "&type=update_hardcopy_status&pid=" + code;
            string result = client.DownloadString(url).Trim('[', ']');
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            if (txt_code.Text == "")
                return;

            hc_copy.Clear();
            string detail_output = get_orderdetails(txt_code.Text, txt_date.Text);

            wb_photos.DocumentText = detail_output;
            if (hc_copy.Count > 0)
            {
                string toprint = hc_copy[0];
                string image_fullpath = @"\\152.10.200.26\Remote\Media\Image" + toprint.Replace("/", "\\");
                pic_holder.Image = Image.FromFile(image_fullpath);
            }
        }


        private static string get_orderdetails(string code, string custom_date)
        {
            string json = "";

            WebClient client = new WebClient();
            string url = "https://photos.kidzania.com.sg/station_request.ashx?key=" + token + "&type=order_detail&pid=" + code;
            if (custom_date != "")
                url = "https://photos.kidzania.com.sg/station_request.ashx?key=" + token + "&type=order_detail&pid=" + code + "&date=" + custom_date;
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

            lbl_photo.Text = "Photo #" + (image_pos + 1);
            string image_path = "";
            string toprint = hc_copy[image_pos];
            if (char.IsLetter(toprint.Substring(0,1).ToCharArray()[0]) && toprint.Substring(1,1) == ":")
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
            txt_photodetails.Text = profile_name + Environment.NewLine + " -- width: " + image_width + " -- height: " + image_height;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (hc_copy.Count == 0)
                return;

            short printcopy = 1;
            if (short.Parse(txtcopies.Text) > 1)
            {
                var confirmResult = MessageBox.Show("Are you sure to print multiple copies?",
                                     "Confirm Cancellation??",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    printcopy = short.Parse(txtcopies.Text);
                }
                else
                    return;
            }
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

            if (char.IsLetter(hardcopy_single.Substring(0, 1).ToCharArray()[0]) && hardcopy_single.Substring(1, 1) == ":")
                current_photo_path = hardcopy_single;
            else
                current_photo_path = @"\\152.10.200.26\Remote\Media\Image" + hardcopy_single.Replace("/", "\\");

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
            
            pd.Print();
            
        }

        private void btn_loadimages_Click(object sender, EventArgs e)
        {
            hc_copy.Clear();
            System.IO.Stream myStream;
            OpenFileDialog thisDialog = new OpenFileDialog();
            thisDialog.InitialDirectory = "c:\\";
            thisDialog.InitialDirectory = @"C:\Users\chrischan76\Downloads\~work\";
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

            pic_holder.Image = Image.FromFile(hc_copy[0]);

            //Calibration of photos based on template(dimension of image file)
            System.Drawing.Image img = System.Drawing.Image.FromFile(hc_copy[0]);
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
            if ((image_width == 6000 && image_height == 4000) || (image_width == 4000 && image_height == 6000))
                profile_name = "DSLR";

            txt_photodetails.Text = profile_name + Environment.NewLine + " -- width: " + image_width + " -- height: " + image_height;
        }

    }
}

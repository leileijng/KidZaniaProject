using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QRCoder;
using System.Drawing;

namespace WebForm
{
    public partial class payment_onsite : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["pid"] == null)
                Response.Redirect("http://photos.kidzania.com.sg");

            string pid = Session["pid"].ToString();
            string total_price = "0";
            if (pid != null)
            {
                total_price = get_totalprice(pid).ToString();
            }
            Uri myuri = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
            string pathQuery = myuri.PathAndQuery;
            string hostName = myuri.ToString().Replace(pathQuery, "");
            string domain = Request.Url.Host;
            string amt = total_price; // Request.Form["sa"];
            string transaction_id = pid;

            if (transaction_id != null)
            {

                string usercode = pid.Replace("kidzsg", "").Substring(0, 5);
                string c_path = System.AppDomain.CurrentDomain.BaseDirectory;
                string[] lines = File.ReadAllLines(c_path + "header.txt");
                string header = string.Join(" ", lines);

                //Display processing
                string metaheader = "<head><meta content='width=device-width, initial-scale = 1' name='viewport' /><title><script type='text/javascript' src='Scripts/jquery-3.3.1.js'></script><link rel='stylesheet' href='/Scripts/css/style.css' type='text/css' media='all' /><link rel='stylesheet' href='/Scripts/css/misc.css' type='text/css' media='all' /><link rel ='stylesheet' href='/Scripts/css/jquery-ui.css'/>"
                            + "<script type='text/javascript' src='/Scripts/jquery-3.3.1.min.js'></script><script type='text/javascript' src='/Scripts/bootstrap.min.js'></script>"
                    + "<link href='/Scripts/lib/font-awesome/css/all.min.css' rel='stylesheet'/><link href='/Scripts/lib/twitter-bootstrap/css/bootstrap.min.css' rel='stylesheet'/>"
                    + "<link href='/Scripts/lib/mdb/css/mdb.min.css' rel='stylesheet'/><link href='/Scripts/css/site.css' rel='stylesheet'/><link href='/Scripts/lib/noty/noty.min.css' rel='stylesheet'/>"
                    + "<link rel='stylesheet' href='/Scripts/css/noty_custom.css' /><link rel='stylesheet' href='/Scripts/css/sticky_footer.css'/><link href='/Scripts/lib/bootstrap-table/bootstrap-table.min.css' rel='stylesheet'/>"
                    + "<link href='/Scripts/lib/jqwidgets/styles/jqx.base.css' rel='stylesheet'/><link href='/Scripts/lib/jqwidgets/styles/jqx.flat.css' rel='stylesheet'/><script src='/Scripts/lib/jquery/dist/jquery.js'></script>"
                    + "<script src='/Scripts/lib/twitter-bootstrap/js/bootstrap.min.js'></script><script src='/Scripts/lib/mdb/js/mdb.min.js'></script><script src='/Scripts/lib/jqwidgets/jqx-all.js'></script>"
                    + "<script src='/Scripts/lib/jquery-validation/dist/jquery.validate.js'></script><script src='/Scripts/lib/jquery-validation/dist/additional-methods.js'></script>"
                    + "<script src='/Scripts/lib/bootstrap-table/bootstrap-table.min.js'></script><script src='/Scripts/lib/noty/noty.min.js'></script><script src='/Scripts/lib/moment/moment.min.js'></script><script src='/Scripts/lib/store/store.min.js'></script></head>";

                //Update purchase table in database 
                update_payment_status(pid, "Waiting");
                update_lineitem_status(pid, "Waiting");
                update_itemphoto_status(pid, "Waiting");


                
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(usercode, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                imgBarCode.Height = 150;
                imgBarCode.Width = 150;
                string imgsrc = "";
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] byteImage = ms.ToArray();
                        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                        imgsrc = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    }
                    Response.Write("<html>" + metaheader + "<body>" + header + "<div id='statustext' style='position: relative; top: 165px; margin-top: 160px;width: 100%; height: 200px; text-align:center; margin:0 auto; font-size: 33px;'><div style=\"text -align:center;margin-top: 10px;margin:0 auto;\"><img src='Content/img/process-checkout.png' /></div><div style='display: inline-block;'>Please proceed to the checkout counter with the following code:<br><b>" + usercode + "</b><img src='"+ imgsrc + "'/></div></div>");

                    payment_success.InnerHtml += "<div style='display: inline-block;'>Please proceed to the checkout counter with the following code:<br> <b id='ordercode'>" + usercode + "</b><p><img id='QRcode' src='" + imgsrc + "' width='300'/></p></div>";

                    //PlaceHolder1.Controls.Add(imgBarCode);
                }
                Response.Flush();
            }
        }

        public static decimal get_totalprice(string pid)
        {
            string connstring = @"server=localhost;userid=root;password=12345;database=kidzania";

            MySqlConnection conn = null;
            conn = new MySqlConnection(connstring);
            conn.Open();
            decimal totalCost = 0;
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * from order where pid = '" + pid + "'", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        totalCost = decimal.Parse(reader["total_amount"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Get total price Error: {0}", e.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return totalCost;
        }

        private void update_payment_status(string pid, string status)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = @"server=localhost;userid=root;password=12345;database=kidzania";
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = myConnectionString;
            try
            {
                conn.Open();
                string query = "UPDATE `order` SET status = '" + status + "', `updatedAt`='"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"' where pid = '" + MySqlHelper.EscapeString(pid) + "'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Update order Error: {0}", e.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private void update_lineitem_status(string pid, string status)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = @"server=localhost;userid=root;password=12345;database=kidzania";
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = myConnectionString;
            try
            {
                conn.Open();
                string query = "UPDATE `lineitem` SET status = '" + status + "', `updatedAt`='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where p_id = '" + MySqlHelper.EscapeString(pid) + "'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Update Lineitem Error: {0}", e.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private void update_itemphoto_status(string pid, string status)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = @"server=localhost;userid=root;password=12345;database=kidzania";
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = myConnectionString;
            try
            {
                conn.Open();
                string query = "UPDATE `itemphoto` SET printing_status = '" + status + "', `updated_at`='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where p_id = '" + MySqlHelper.EscapeString(pid) + "'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Update photo item: {0}", e.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}
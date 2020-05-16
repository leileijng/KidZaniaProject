using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                total_price = get_totalprice(pid);
            }
            Uri myuri = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
            string pathQuery = myuri.PathAndQuery;
            string hostName = myuri.ToString().Replace(pathQuery, "");
            string domain = Request.Url.Host;
            string amt = total_price; // Request.Form["sa"];
            string transaction_id = pid;
            if (transaction_id != null)
            {
                string c_path = System.AppDomain.CurrentDomain.BaseDirectory;
                string[] lines = File.ReadAllLines(c_path + "header.txt");
                string header = string.Join(" ", lines);

                //Display processing
                string metaheader = "<head><title></title><link rel='stylesheet'  href='/css/style.css' type='text/css' media='all' /><link rel='stylesheet'  href='/css/misc.css' type='text/css' media='all' /><link rel='stylesheet' href='/css/jquery-ui.css'></head>";

                //Update purchase table in database 
                update_payment_status(pid, "onsite");

                string usercode = pid.Replace("kidzsg", "").Substring(0, 5);

                Response.Write("<html>" + metaheader + "<body>" + header + "<div id='statustext' style='position: relative; top: 165px; margin-top: 160px;width: 100%; height: 200px; text-align:center; margin:0 auto; font-size: 33px;'><div style=\"text -align:center;margin-top: 10px;margin:0 auto;\"><img src='/img/process-checkout.png' /></div><div style='display: inline-block;'>Please proceed to the checkout counter with the following code:<br>" + usercode + "</div></div>");

                Response.Flush();
            }
        }
        public static string get_totalprice(string pid)
        {
            string total_price = "0";
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;pwd=zzaaqq11;database=frphotosg;SslMode=none";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string query = "SELECT * FROM purchase WHERE purchase_id = '" + MySqlHelper.EscapeString(pid) + "';";
                var cmd = new MySqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    total_price = reader["total_price"].ToString();
                }
                conn.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }
            return total_price;
        }

        private void update_payment_status(string pid, string status = "paid")
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=127.0.0.1;uid=root;pwd=zzaaqq11;database=frphotosg;SslMode=none";

            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = myConnectionString;
            conn.Open();
            string query = "UPDATE purchase SET status = '" + status + "', type = '" + status + "' where purchase_id = '" + MySqlHelper.EscapeString(pid) + "'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

        }
    }
}
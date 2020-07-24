using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.Net;
using System.Net.Sockets;

//Clickable DIV solution to change the button to DIV
//https://www.experts-exchange.com/questions/27457900/Clickable-Div-in-ASP-NET.html

//IMage orientation EXIF - https://stackoverflow.com/questions/6222053/problem-reading-jpeg-metadata-orientation

namespace WebForm
{
    public partial class main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string pid = "kidzsg" + RandomString(5) + DateTime.Now.ToString("ddMMyy");
            Session["pid"] = pid;
            Session["onsite"] = true;
            Session["CartItems"] = null;
        }


        protected void PhotoScan(object sender, EventArgs e)
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;
            string ipaddr = ip_addr.Value;

            /*
            if (path.Contains("/onsite/") && !ipaddr.StartsWith("203.116."))
            {
                if (!ipaddr.StartsWith("101.127") && !ipaddr.StartsWith("66.96")) //Chris Development ip address - to be remove
                {
                    Session["onsite"] = false;
                    Response.Redirect("https://photos.kidzania.com.sg/"); //redirect to online page
                }

            }
            else if (!path.Contains("/onsite/"))
                Session["onsite"] = false;

                */
            string dateval = datepicker.Value;

            if (dateval == "")
                dateval = DateTime.Now.ToString("ddMMyyyy").ToString();
            else
            {
                DateTime date = DateTime.ParseExact(dateval, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                dateval = date.ToString("ddMMyyyy").ToString();
            }

            string threshold = Range1.Value;
            Session["DateVal"] = dateval;
            Session["Threshold"] = threshold;
            Response.Redirect("photoscan.aspx");
        }

        protected void PhotoUpload(object sender, EventArgs e)
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;
            string ipaddr = ip_addr.Value;

            /*
            if (path.Contains("/onsite/") && !ipaddr.StartsWith("203.116."))
            {
                if (!ipaddr.StartsWith("101.127")) //Chris Development ip address - to be remove
                {
                    Session["onsite"] = false;
                    Response.Redirect("https://photos.kidzania.com.sg/"); //redirect to online page
                }
            }
            else if (!path.Contains("/onsite/"))
                Session["onsite"] = false;
                */

            string dateval = datepicker.Value;
            if (dateval == "")
                dateval = DateTime.Now.ToString("ddMMyyyy").ToString();
            else
            {
                DateTime date = DateTime.ParseExact(dateval, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                dateval = date.ToString("ddMMyyyy").ToString();
            }

            string threshold = Range1.Value;
            Session["DateVal"] = dateval;
            Session["Threshold"] = threshold;
            Response.Redirect("photoupload.aspx");
        }

        protected void ProfileSearch(object sender, EventArgs e)
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;
            string ipaddr = ip_addr.Value;

            /*
            if (path.Contains("/onsite/") && !ipaddr.StartsWith("203.116."))
            {
                if (!ipaddr.StartsWith("101.127")) //Chris Development ip address - to be remove
                {
                    Session["onsite"] = false;
                    Response.Redirect("https://photos.kidzania.com.sg/"); //redirect to online page
                }
            }
            else if (!path.Contains("/onsite/"))
                Session["onsite"] = false;
                */
            string dateval = datepicker.Value;
            if (dateval == "")
                dateval = DateTime.Now.ToString("ddMMyyyy").ToString();
            else
            {
                DateTime date = DateTime.ParseExact(dateval, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                dateval = date.ToString("ddMMyyyy").ToString();
            }
            

            string threshold = Range1.Value;
            Session["DateVal"] = dateval;
            Session["Threshold"] = threshold;
            Response.Redirect("profile_id_search.aspx");
        }

        public static bool check_pid_exist(string pid)
        {
            bool exist = false;
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=kidzania_sg;pwd=zzaaqq11;database=kidzania_sg_photofr;";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string query = "SELECT count(*) FROM purchase WHERE pid = '" + MySqlHelper.EscapeString(pid) + "';";
                var cmd = new MySqlCommand(query, conn);
                int RecCount = int.Parse(cmd.ExecuteScalar().ToString());
                if (RecCount > 0)
                {
                    exist = true;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }
            return exist;
        }


        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
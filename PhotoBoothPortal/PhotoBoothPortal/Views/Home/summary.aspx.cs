using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PhotoBoothPortal.Views.Test
{
    public partial class summary : System.Web.Mvc.ViewPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["digital_cb"] != null || Request.Form["hardcopy_cb"] != null)
            {
                string[] digitalcopy_list = Request.Form.GetValues("digital_cb");
                string[] hardcopy_list = Request.Form.GetValues("hardcopy_cb");
                string amt = Request.Form["sa"];
                string dc = Request.Form["dc"];  //Digital Copy
                string hc = Request.Form["hc"];  //Hard Copy
                string ec = Request.Form["ec"];  //Establishment Card
                string a5 = Request.Form["a5"];  //A5 Folder
                string mg = Request.Form["mg"];  //Magnet
                string kc = Request.Form["kc"];  //Keychain
                string lr = Request.Form["lr"];  //Leatherette
                string json = Request.Form["js"];

                MySql.Data.MySqlClient.MySqlConnection conn;
                string myConnectionString;

                myConnectionString = "server=127.0.0.1;uid=root;pwd=zzaaqq11;database=frphotosg;SslMode=none";

                try
                {
                    string pid = Session["pid"].ToString();
                    string dcopy = ""; string hcopy = ""; string eccopy = ""; string a5copy = ""; string mgcopy = ""; string kccopy = ""; string lrcopy = "";
                    if (Request.Form.GetValues("digital_cb") != null) dcopy = string.Join("|", Request.Form.GetValues("digital_cb"));
                    if (Request.Form.GetValues("hardcopy_cb") != null) hcopy = string.Join("|", Request.Form.GetValues("hardcopy_cb"));
                    if (Request.Form.GetValues("ECcopy_cb") != null) eccopy = string.Join("|", Request.Form.GetValues("ECcopy_cb"));
                    if (Request.Form.GetValues("A5copy_cb") != null) a5copy = string.Join("|", Request.Form.GetValues("A5copy_cb"));
                    if (Request.Form.GetValues("MGcopy_cb") != null) mgcopy = string.Join("|", Request.Form.GetValues("MGcopy_cb"));
                    if (Request.Form.GetValues("KCcopy_cb") != null) kccopy = string.Join("|", Request.Form.GetValues("KCcopy_cb"));
                    if (Request.Form.GetValues("LRcopy_cb") != null) lrcopy = string.Join("|", Request.Form.GetValues("LRcopy_cb"));


                    List<string> dcopy_list = dcopy.Split('|').ToList();
                    List<string> hcopy_list = hcopy.Split('|').ToList();
                    List<string> eccopy_list = eccopy.Split('|').ToList();
                    List<string> a5copy_list = a5copy.Split('|').ToList();
                    List<string> mgcopy_list = mgcopy.Split('|').ToList();
                    List<string> kccopy_list = kccopy.Split('|').ToList();
                    List<string> lrcopy_list = lrcopy.Split('|').ToList();
                    List<string> json_list = json.Split('|').ToList();

                    string softcopy_json = "";
                    foreach (string dcopydata in dcopy_list)
                    {
                        if (dcopydata != "")
                        {
                            foreach (string jsondata in json_list)
                            {
                                if (jsondata.Contains(dcopydata))
                                    softcopy_json += jsondata + "|";
                            }
                        }
                    }
                    softcopy_json = softcopy_json.TrimEnd('|');

                    string hardcopy_json = "";
                    foreach (string hcopydata in hcopy_list)
                    {
                        if (hcopydata != "")
                        {
                            foreach (string jsondata in json_list)
                            {
                                if (jsondata.Contains(hcopydata))
                                    hardcopy_json += jsondata + "|";
                            }
                        }
                    }
                    hardcopy_json = hardcopy_json.TrimEnd('|');

                    string eccopy_json = "";
                    foreach (string eccopydata in eccopy_list)
                    {
                        if (eccopydata != "")
                        {
                            foreach (string jsondata in json_list)
                            {
                                if (jsondata.Contains(eccopydata))
                                    eccopy_json += jsondata + "|";
                            }
                        }
                    }
                    eccopy_json = eccopy_json.TrimEnd('|');

                    string a5copy_json = "";
                    foreach (string a5copydata in a5copy_list)
                    {
                        if (a5copydata != "")
                        {
                            foreach (string jsondata in json_list)
                            {
                                if (jsondata.Contains(a5copydata))
                                    a5copy_json += jsondata + "|";
                            }
                        }
                    }
                    a5copy_json = a5copy_json.TrimEnd('|');

                    string mgcopy_json = "";
                    foreach (string mgcopydata in mgcopy_list)
                    {
                        if (mgcopydata != "")
                        {
                            foreach (string jsondata in json_list)
                            {
                                if (jsondata.Contains(mgcopydata))
                                    mgcopy_json += jsondata + "|";
                            }
                        }
                    }
                    mgcopy_json = mgcopy_json.TrimEnd('|');

                    string kccopy_json = "";
                    foreach (string kccopydata in kccopy_list)
                    {
                        if (kccopydata != "")
                        {
                            foreach (string jsondata in json_list)
                            {
                                if (jsondata.Contains(kccopydata))
                                    kccopy_json += jsondata + "|";
                            }
                        }
                    }
                    kccopy_json = kccopy_json.TrimEnd('|');

                    string lrcopy_json = "";
                    foreach (string lrcopydata in lrcopy_list)
                    {
                        if (lrcopydata != "")
                        {
                            foreach (string jsondata in json_list)
                            {
                                if (jsondata.Contains(lrcopydata))
                                    lrcopy_json += jsondata + "|";
                            }
                        }
                    }
                    lrcopy_json = lrcopy_json.TrimEnd('|');

                    if (check_pid_exist(pid) == false)
                    {
                        conn = new MySql.Data.MySqlClient.MySqlConnection();
                        conn.ConnectionString = myConnectionString;
                        conn.Open();
                        string query = "INSERT INTO purchase ( purchase_id, hardcopy, softcopy, eccopy, a5copy, mgcopy, kccopy, lrcopy, json, total_price) VALUES('" + MySqlHelper.EscapeString(pid) + "','" + MySqlHelper.EscapeString(hardcopy_json) + "','" + MySqlHelper.EscapeString(softcopy_json) + "','" + MySqlHelper.EscapeString(eccopy_json) + "','" + MySqlHelper.EscapeString(a5copy_json) + "','" + MySqlHelper.EscapeString(mgcopy_json) + "','" + MySqlHelper.EscapeString(kccopy_json) + "','" + MySqlHelper.EscapeString(lrcopy_json) + "','" + MySqlHelper.EscapeString(json) + "', " + amt + ");";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    Console.Write(ex.Message);
                }

                summarydiv.InnerHtml += "Hardcopy: " + hc;
                summarydiv.InnerHtml += "<br>Digital copy: " + dc;
                summarydiv.InnerHtml += "<br>Establishment Card: " + ec;
                summarydiv.InnerHtml += "<br>A5 Folder: " + a5;
                summarydiv.InnerHtml += "<br>Magnet: " + mg;
                summarydiv.InnerHtml += "<br>Keychain: " + kc;
                summarydiv.InnerHtml += "<br>Leatherette: " + lr;
                summarydiv.InnerHtml += "<br>Total amt: $" + amt + " SGD";
                sa.Value = amt;
            }
            else if (Request.QueryString["retry"] != null)
            {
                if (Session["pid"] == null)
                    Response.Redirect("http://photos.kidzania.com.sg");
                string pid = Session["pid"].ToString();
                string hc = "";
                string dc = "";
                string amt = "";
                if (pid == null)
                    Response.Redirect("http://photos.kidzania.com.sg");
                else
                {
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
                            amt = reader["total_price"].ToString();
                            if (reader["hardcopy"].ToString() == "")
                                hc = "0";
                            else
                                hc = reader["hardcopy"].ToString().Split('|').Length.ToString();
                            if (reader["softcopy"].ToString() == "")
                                dc = "0";
                            else
                                dc = reader["softcopy"].ToString().Split('|').Length.ToString();
                        }
                        summarydiv.InnerHtml += "Total hardcopy: " + hc;
                        summarydiv.InnerHtml += "<br>Total digital copy: " + dc;
                        summarydiv.InnerHtml += "<br>Total amt: $" + amt + " SGD";
                        sa.Value = amt;
                        conn.Close();
                    }
                    catch (MySql.Data.MySqlClient.MySqlException ex)
                    {
                        Console.Write(ex.Message);
                    }

                }
            }
            else
            {
                Response.Redirect("http://photos.kidzania.com.sg");
            }
        }

        public static bool check_pid_exist(string pid)
        {
            bool exist = false;
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;pwd=zzaaqq11;database=frphotosg;SslMode=none";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string query = "SELECT count(*) FROM purchase WHERE purchase_id = '" + MySqlHelper.EscapeString(pid) + "';";
                var cmd = new MySqlCommand(query, conn);
                int RecCount = int.Parse(cmd.ExecuteScalar().ToString());
                if (RecCount > 0)
                {
                    exist = true;
                }
                conn.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }

            return exist;
        }

        protected void GoToCheckout(object sender, EventArgs e)
        {
            Response.Redirect("checkout.aspx");
        }
    }
}
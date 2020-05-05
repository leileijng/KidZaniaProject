﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.Collections.Specialized;
using System.Web.Script.Serialization;
using Dropbox.Api;
using System.Threading.Tasks;
using System.IO;
using System.Security.Principal;
using System.Globalization;
using System.Net.Http;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace PhotoBoothPortal.Views.Test
{
    public partial class selection : System.Web.Mvc.ViewPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            bool onsite = false;
            try
            {
                onsite = bool.Parse(Session["onsite"].ToString());
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Response.Redirect("/");
            }

            string profile_id = "";
            Dictionary<string, string> photoprofile = new Dictionary<string, string>();
            string alert_message = "";
            if (Request.Form["pf"] != null)
            {
                string profile = "";
                string pf = Request.Form["pf"];
                if (!pf.Contains("|"))
                {
                    string datval = Session["dateval"].ToString();
                    profile = datval + "|" + pf;
                }
                else
                {
                    profile = Request.Form["pf"].Replace("%7c", "|");
                }

                string pid = Session["pid"].ToString();

                //This is to check if there are any records of the profile in the purchase table.
                //This is to warn the customer if they have already made a purchase already.
                string status = checkprofile(profile, pid);
                if (status == "false")
                    Insert_profile(profile, pid);
                else if (status == "selection")
                {
                    //display alert
                    alert_message = "Profile is in selection stage on another terminal.";
                }
                else if (status == "processing")
                {
                    alert_message = "There is an existing order with this profile.";
                }

                Session["profile"] = profile;
                photoprofile = getphotoselection(profile);
                if (photoprofile.Count == 0)
                    Response.Redirect("/onsite/");
                profile_id = profile.Split('|')[1];
            }


            if (photoprofile.Count > 0)
            {
                if (Session["CapturedImageBase64"] != null)
                {
                    string photobase64 = Session["CapturedImageBase64"].ToString();
                    //profile_photo.InnerHtml = "<img style='height: 150px;' src='" + photopath + "' />";
                    profile_photo.InnerHtml = "<img style='max-height: 300px !important;' src='" + photobase64 + "' />";
                }


                Dictionary<string, string> photomatch = photoprofile; //comment it for debugging.
                ///COMMENT UNTIL HERE FOR DEBUGGING

                string currentApplicationPath = HttpContext.Current.Request.PhysicalApplicationPath;

                Uri myuri = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
                string pathQuery = myuri.PathAndQuery;
                string host = myuri.ToString().Replace(pathQuery, "") + "/";
                host = "/";
                if (photomatch.Count > 0)
                {
                    int x = 1;
                    string photofullpath = "";
                    foreach (var photo in photomatch)
                    {
                        string photounwatermarked = photo.Key;
                        string photowatermarked = photo.Value;
                        string photoid = photounwatermarked.Substring(photowatermarked.LastIndexOf('/') + 1);

                        photowatermarked = photowatermarked.Replace(@"Xeric/files/kidzania/", "");
                        string photowatermarked_filename = photowatermarked.Replace("/", "");
                        if (!File.Exists(currentApplicationPath + "photos//" + photowatermarked_filename)) { }
                        else
                        {
                            File.Delete(currentApplicationPath + "photos//" + photowatermarked_filename);
                        }

                        photo_gallery_ctn.InnerHtml += "<div id='gl_id_" + x + "' class=\"gallery\">";
                        photo_gallery_ctn.InnerHtml += "<img class='modal_window' onclick='show_modal(this);'; id='tn_id_" + x + "' class='thumbnail' src='" + host + "photos/" + photowatermarked_filename + "'>";
                        photo_gallery_ctn.InnerHtml += "<div style='margin-bottom: 15px; font-size: 10px;'>Profile ID:" + profile_id + "</div>";
                        photo_gallery_ctn.InnerHtml += "<div class='item_checkbox_grp'>";
                        photo_gallery_ctn.InnerHtml += "    <div>";
                        photo_gallery_ctn.InnerHtml += "        <input name=\"digital_cb\" id=\"dc_photo" + photoid + "\" type=\"checkbox\" checked=\"checked\" value=\"" + photoid + "\">";
                        photo_gallery_ctn.InnerHtml += "        &nbsp;<label name=\"dc_photo" + photoid + "_lbl\">Digital</label>";
                        photo_gallery_ctn.InnerHtml += "    </div>";
                        if (onsite)
                        {
                            photo_gallery_ctn.InnerHtml += "    <div>";
                            photo_gallery_ctn.InnerHtml += "        <input name=\"A5copy_cb\" id=\"a5_photo" + photoid + "\" type=\"checkbox\" value=\"" + photoid + "\">";
                            photo_gallery_ctn.InnerHtml += "        &nbsp;<label name=\"a5_photo" + photoid + "_lbl\">A5 w/Folder</label>";
                            photo_gallery_ctn.InnerHtml += "    </div>";
                            photo_gallery_ctn.InnerHtml += "    <div>";
                            photo_gallery_ctn.InnerHtml += "        <input name=\"KCcopy_cb\" id=\"kc_photo" + photoid + "\" type=\"checkbox\" value=\"" + photoid + "\">";
                            photo_gallery_ctn.InnerHtml += "        &nbsp;<label name=\"kc_photo" + photoid + "_lbl\">Keychain</label>";
                            photo_gallery_ctn.InnerHtml += "    </div>";
                            photo_gallery_ctn.InnerHtml += "    <div>";
                            photo_gallery_ctn.InnerHtml += "        <input name=\"ECcopy_cb\" id=\"ec_photo" + photoid + "\" type=\"checkbox\" value=\"" + photoid + "\">";
                            photo_gallery_ctn.InnerHtml += "        &nbsp;<label name=\"ec_photo" + photoid + "_lbl\">Establishment Card</label>";
                            photo_gallery_ctn.InnerHtml += "    </div>";
                            photo_gallery_ctn.InnerHtml += "    <div>";
                            photo_gallery_ctn.InnerHtml += "        <input name=\"MGcopy_cb\" id=\"mg_photo" + photoid + "\" type=\"checkbox\" value=\"" + photoid + "\">";
                            photo_gallery_ctn.InnerHtml += "        &nbsp;<label name=\"mg_photo" + photoid + "_lbl\">Magnet</label>";
                            photo_gallery_ctn.InnerHtml += "    </div>";

                        }

                        photo_gallery_ctn.InnerHtml += "</div>";
                        photo_gallery_ctn.InnerHtml += "<div onclick='delete_gl(\"" + x + "\");' style='text-align:center;border-top: solid 1px;margin-top: 10px;cursor: pointer;padding: 5px;background-color: indianred;'><img style='width: auto !important;' src='\\img\\trash_small.png' /></div>";
                        photo_gallery_ctn.InnerHtml += "</div>";

                        x++;
                        photofullpath += photo.Key + "|";
                    }
                    int dc_amt = 20; //Default digital copy price
                    purchase_status.InnerHtml += "<div class='fix_corner'>";
                    purchase_status.InnerHtml += "  <div class='fix_corner_ctn'>";
                    purchase_status.InnerHtml += "      <div class='fix_corner_item' id='digital_copy_amt'>Digital Copy: $" + dc_amt + "</div>";
                    if (onsite)
                    {
                        purchase_status.InnerHtml += "      <div class='fix_corner_item' id='a5_copy_amt'>A5 hardcopy: $0</div>";
                        purchase_status.InnerHtml += "      <div class='fix_corner_item' id='kc_copy_amt'>Keychain: $0</div>";
                        purchase_status.InnerHtml += "      <div class='fix_corner_item' id='ec_copy_amt'>Establishment Card: $0</div>";
                        purchase_status.InnerHtml += "      <div class='fix_corner_item' id='mg_copy_amt'>Magnet: $0</div>";
                        purchase_status.InnerHtml += "      <div class='fix_corner_item' id='lr_copy_amt'>Leatherette: $0</div>";

                    }
                    purchase_status.InnerHtml += "      <div class='fix_corner_item' id='Total_cost'>Total: $" + dc_amt + " SGD</div>";
                    purchase_status.InnerHtml += "      <div class='fix_corner_item'><b>*Bold: purchase with purchase discount</b></div>";
                    if (alert_message != "")
                        purchase_status.InnerHtml += "     <br> <div class='fix_corner_item'><b><span style='color:red;'>ALERT!!! " + alert_message + "</span></b></div>";
                    purchase_status.InnerHtml += "  </div>";
                    purchase_status.InnerHtml += "</div>";

                    sa.Attributes["value"] = dc_amt.ToString();
                    dc.Attributes["value"] = (x - 1).ToString();
                    js.Attributes["value"] = photofullpath.TrimEnd('|');
                }

                photo_gallery_ctn.InnerHtml += "<!-- The Modal -->";
                photo_gallery_ctn.InnerHtml += "<div id=\"myModal\" class=\"modal\">";
                photo_gallery_ctn.InnerHtml += "  <!-- The Close Button -->";
                photo_gallery_ctn.InnerHtml += "<span class=\"close\" onclick=\"document.getElementById('myModal').style.display='none'\">&times;</span>";
                photo_gallery_ctn.InnerHtml += "  <!-- Modal Content (The Image) -->";
                photo_gallery_ctn.InnerHtml += "<img class=\"modal-content\" id=\"img01\">";
                photo_gallery_ctn.InnerHtml += "  <!-- Modal Caption (Image Text) -->";
                photo_gallery_ctn.InnerHtml += "  <div id=\"caption\"></div>";
                photo_gallery_ctn.InnerHtml += "</div>";
                photo_gallery_ctn.InnerHtml += "<script>";
                photo_gallery_ctn.InnerHtml += "  modal_load();";
                photo_gallery_ctn.InnerHtml += "</script>";
                return;
            }
        }

        public class Post_Search_Profile
        {
            public List<string> groups { get; set; }
            public string image64 { get; set; }
            public string approve { get; set; }
            public int threshold { get; set; }
        }

        public class SearchData
        {
            public string profile_id { get; set; } // unique profile id
            public string group { get; set; } // Date in DDMMYYY eg. 28092018
            public string image { get; set; } // Image path – from local path
                                              // if image is at D:/Remote/Image/28092018/01/huiehdfacvneofeah.jpg
                                              // result will be /28092018/01/huiehdfacvneofeah.jpg
            public int count { get; set; } // number of image in profile
        }

        public class NM_ProfileItem
        {
            public string profile_id { get; set; } // unique profile id
            public string group { get; set; } // Date in DDMMYYYY
            public string facetrack { get; set; } // unique face track id
            public string faceimage { get; set; } // image location from file storage path
            public string photoimage { get; set; } // image location from file storage path
            public string previewimage { get; set; } // image location from file storage path
        }

        protected Dictionary<string, string> getphotoselection(string photodata)
        {
            Dictionary<string, string> retval = new Dictionary<string, string>();
            string groupid = photodata.Split('|')[0];
            string profileid = photodata.Split('|')[1];

            string apipath = "http://152.10.200.26:8081/api";
            var pclient = new HttpClient();
            var presponse = pclient.GetAsync(apipath + "/media/" + groupid + "/" + profileid).Result;

            List<NM_ProfileItem> presult = new List<NM_ProfileItem>();
            if (presponse.IsSuccessStatusCode)
            {
                using (HttpContent data = presponse.Content)
                {
                    Task<string> p_result = data.ReadAsStringAsync();
                    string str = p_result.Result;
                    presult = JsonConvert.DeserializeObject<List<NM_ProfileItem>>(str);
                }
            }

            if (presult.Count > 0)
            {
                for (int x = 0; x < presult.Count; x++)
                {
                    string photox = presult[x].photoimage.ToString();
                    string watermarkx = presult[x].previewimage.ToString();
                    if (!retval.ContainsKey(photox))
                        retval.Add(photox, watermarkx);
                }
            }
            return retval;
        }

        protected string checkprofile(string profile, string pid)
        {
            string status = "false";

            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;pwd=zzaaqq11;database=frphotosg;SslMode=none";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string query = "SELECT * FROM profiles WHERE profile = '" + MySqlHelper.EscapeString(profile) + "';";
                var cmd = new MySqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    status = reader["status"].ToString();
                    string db_pid = reader["pid"].ToString();
                    if (db_pid == pid)
                        status = "same_session";
                    //else
                    //    status = status;
                }
                conn.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }
            return status;
        }

        public static void Insert_profile(string profile, string pid)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;pwd=zzaaqq11;database=frphotosg;SslMode=none";
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = myConnectionString;
            conn.Open();
            string query = "INSERT INTO profiles (pid, profile, status) VALUES('" + pid + "','" + profile + "','selection');";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
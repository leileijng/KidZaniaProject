using System;
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
using System.Diagnostics;
using System.Data;
using System.Web.Services;
using WebForm;
using static WebForm.summary;

namespace WebForm
{
    public partial class selection : System.Web.UI.Page
    {
        public static List<InCartItem> incartItems;

        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            bool onsite = false;
            try
            {
                onsite = bool.Parse(Session["onsite"].ToString());
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                //Response.Redirect("/");
            }
            */
            

            string profile_id = "";

            
            Dictionary<string, string> photoprofile = new Dictionary<string, string>();
            string alert_message = "";
            if (Request.Form["pf"] != null)
            {
                string profile = "";
                string pf = Request.Form["pf"];
                //testing purpose
                string photoPath = pf.Replace("photoID", "");
                profile_photo.InnerHtml = "<img style='max-height: 300px !important;' src='/Content/photos/" + photoPath + ".jpg' />";
                
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
                /*
                photoprofile = getphotoselection(profile);
                if (photoprofile.Count == 0)
                    Response.Redirect("/onsite/");
                    */
                profile_id = profile.Split('|')[1];
            }

            if (alert_message != "")
            {
                purchase_status.InnerHtml += "<div class='fix_corner'>";
                purchase_status.InnerHtml += "  <div class='fix_corner_ctn'>";
                purchase_status.InnerHtml += "     <br> <div class='fix_corner_item'><b><span style='color:red;'>ALERT!!! " + alert_message + "</span></b></div>";
                purchase_status.InnerHtml += "  </div>";
                purchase_status.InnerHtml += "</div>";
            }

            /*
                      if (photoprofile.Count > 0)
                      {
                          if (Session["CapturedImageBase64"] != null)
                          {
                              string photobase64 = Session["CapturedImageBase64"].ToString();
                              //profile_photo.InnerHtml = "<img style='height: 150px;' src='" + photopath + "' />";
                              profile_photo.InnerHtml = "<img style='max-height: 300px !important;' src='" + photobase64 + "' />";
                          }
                          Dictionary<string, string> photomatch = photoprofile; //comment it for debugging.
                          ///COMMENT UNTIL HERE FOR DEBUGGING*/
            ///

            if (Session["CartItems"] != null)
            {
                incartItems = (List<InCartItem>)Session["CartItems"];
                string pid = Session["pid"].ToString();
                deletePreviousOrder(pid);
                Debug.WriteLine("Deleted!");
            }

            Dictionary<string, string> photomatch = new Dictionary<string, string>();
            photomatch.Add("photoID1", "1.jpg");
            photomatch.Add("photoID2", "2.jpg");
            photomatch.Add("photoID3", "3.jpg");
            photomatch.Add("photoID4", "4.jpg");
            photomatch.Add("photoID5", "5.jpg");
            photomatch.Add("photoID6", "6.jpg");
            photomatch.Add("photoID7", "7.jpg");
            photomatch.Add("photoID8", "8.jpg");
            photomatch.Add("photoID9", "9.jpg");
            photomatch.Add("photoID10", "10.jpg");
            photomatch.Add("photoID11", "11.jpg");
            photomatch.Add("photoID12", "12.jpg");
            //photomatch.Add("photoID13", "13.jpg");

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
                    string photoid = photounwatermarked;

                    photowatermarked = photowatermarked.Replace(@"Xeric/files/kidzania/", "");
                    string photowatermarked_filename = photowatermarked.Replace("/", "");

                    if (!File.Exists(currentApplicationPath + "photos//" + photowatermarked_filename)) { }
                    else
                    {
                        File.Delete(currentApplicationPath + "photos//" + photowatermarked_filename);
                    }

                    photo_gallery_ctn.InnerHtml += "<div id='gl_id_" + x + "' class=\"gallery\">";

                    photo_gallery_ctn.InnerHtml += "<img class=\"modal_window\" id=\"tn_id_" + x + "\" class=\"thumbnail\" src=\"/Content/photos/" + photowatermarked_filename + "\"/>";

                    //photo_gallery_ctn.InnerHtml += "<img class='modal_window' onclick='show_modal(" + photoid + ");' id='tn_id_" + x + "' class='thumbnail' src='/Content/photos/" + photowatermarked_filename + "'>";

                    photo_gallery_ctn.InnerHtml += "<div style='margin-bottom: 15px; font-size: 10px;'>Profile ID:" + photoid + "</div>";
                    photo_gallery_ctn.InnerHtml += "    <div>";

                    
                    List<Product> products = GetProductsFromDB();
                    List<Product> photoProducts = new List<Product>();
                    foreach (Product p in products) {
                        if (p.PhotoProduct) {
                            photoProducts.Add(p);
                        }
                    }

                    //First For loop to extract the list
                    for (int i = 0; i < photoProducts.Count; i++)
                    {

                        photo_gallery_ctn.InnerHtml += "    <div style='text-align:left;margin-left:30px;'>";
                        photo_gallery_ctn.InnerHtml += "    <input class='form-check-input filled-in itemsChk' onclick='checkChange(\"/Content/Photos/" + photowatermarked_filename + "\",\"" + photoid + "\",\"" + photoProducts[i].ProductId + "\")' type='checkbox' id='item" + photoid + photoProducts[i].ProductId + "'" + " name='product' value='" + photoProducts[i].ProductId + "'/>";
                        photo_gallery_ctn.InnerHtml += "    &nbsp;<label style='height:20px;' name='" + photoProducts[i].ProductName + photoid + "'>" + photoProducts[i].ProductName + "</label>";
                        photo_gallery_ctn.InnerHtml += "    </div>";
                    }
                    /*
                   //photo_gallery_ctn.InnerHtml += "    </div>";

                   //photo_gallery_ctn.InnerHtml += "    <div>";
                   //photo_gallery_ctn.InnerHtml += "        <input name=\"digital_cb\" id=\"dc_photo" + photoid + "\" type=\"checkbox\" checked=\"checked\" value=\"" + photoid + "\">";
                   //photo_gallery_ctn.InnerHtml += "        &nbsp;<label name=\"dc_photo" + photoid + "_lbl\">Digital</label>";
                   //photo_gallery_ctn.InnerHtml += "    </div>";

                   //photo_gallery_ctn.InnerHtml += "<!-- The Modal -->";
                   //photo_gallery_ctn.InnerHtml += "<div id='myModala" + photoid + "' class=\"modal\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"exampleModalLabel\" aria-hidden=\"true\">";
                   //photo_gallery_ctn.InnerHtml += "<div class=\"modal-dialog\" role=\"document\">";

                   //photo_gallery_ctn.InnerHtml += "  <!-- The Close Button -->";
                   //photo_gallery_ctn.InnerHtml += "<span class=\"close\" onclick=\"document.getElementById('myModala" + photoid + "').style.display='none'\">&times;</span>";
                   //photo_gallery_ctn.InnerHtml += "  <!-- Modal Content (The Image) -->";
                   //photo_gallery_ctn.InnerHtml += "<img class=\"modal-content\" id=\"img01\" src='/Content/photos/" + photowatermarked_filename + "'> ";
                   //photo_gallery_ctn.InnerHtml += "  <!-- Modal Caption (Image Text) -->";
                   //photo_gallery_ctn.InnerHtml += "        <div class='modal-body'>";
                   */

                   //photo_gallery_ctn.InnerHtml += "</div>";
                   //photo_gallery_ctn.InnerHtml += "</div>";

                   //photo_gallery_ctn.InnerHtml += "<script>";
                   //photo_gallery_ctn.InnerHtml += "  modal_load(" + photoid + "); ";
                   //photo_gallery_ctn.InnerHtml += "</script>";


                   photo_gallery_ctn.InnerHtml += "</div>";
                            photo_gallery_ctn.InnerHtml += "<div onclick='delete_gl(\"" + x + "\");' style='text-align:center;border-top: solid 1px;margin-top: 10px;cursor: pointer;padding: 5px;background-color: indianred;'><img style='width: auto !important;' src='\\Content\\img\\trash_small.png' /></div>";
                            photo_gallery_ctn.InnerHtml += "</div>";

                            x++;
                            photofullpath += photo.Key + "|";


                    

            }
                /*
                int dc_amt = 20; //Default digital copy price

                purchase_status.InnerHtml += "<div class='fix_corner'>";
                purchase_status.InnerHtml += "  <div class='fix_corner_ctn'>";
                purchase_status.InnerHtml += "      <div class='fix_corner_item' id='digital_copy_amt'>Digital Copy: $" + dc_amt + "</div>";

                if (true)
                {
                    purchase_status.InnerHtml += "      <div class='fix_corner_item' id='a5_copy_amt'>A5 hardcopy: $0</div>";
                    purchase_status.InnerHtml += "      <div class='fix_corner_item' id='kc_copy_amt'>Keychain: $0</div>";
                    purchase_status.InnerHtml += "      <div class='fix_corner_item' id='ec_copy_amt'>Establishment Card: $0</div>";
                    purchase_status.InnerHtml += "      <div class='fix_corner_item' id='mg_copy_amt'>Magnet: $0</div>";
                    purchase_status.InnerHtml += "      <div class='fix_corner_item' id='lr_copy_amt'>Leatherette: $0</div>";

                }
                purchase_status.InnerHtml += "      <div class='fix_corner_item' id='Total_cost'>Total: $" + dc_amt + " SGD</div>";
                purchase_status.InnerHtml += "      <div class='fix_corner_item'><b>*Bold: purchase with purchase discount</b></div>";


                if (alert_message != ""){
                purchase_status.InnerHtml += "  </div>";
                    purchase_status.InnerHtml += "</div>";
                    sa.Attributes["value"] = dc_amt.ToString();
                    dc.Attributes["value"] = (x - 1).ToString();
                    js.Attributes["value"] = photofullpath.TrimEnd('|');
                }



                return;
            }*/
            


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
            //myConnectionString = "server=127.0.0.1;uid=root;pwd=zzaaqq11;database=frphotosg;SslMode=none";

            myConnectionString = @"server=localhost;userid=root;password=12345;database=kidzania";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string query = "SELECT * FROM profile WHERE profile = '" + MySqlHelper.EscapeString(profile) + "';";
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

        public static void deletePreviousOrder(string pid)
        {
            //delete itemphoto
            try {
                MySql.Data.MySqlClient.MySqlConnection conn;
                string myConnectionString;
                //myConnectionString = "server=127.0.0.1;uid=root;pwd=zzaaqq11;database=frphotosg;SslMode=none";
                myConnectionString = @"server=localhost;userid=root;password=12345;database=kidzania";

                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                string query = "DELETE FROM `kidzania`.`itemphoto` WHERE `p_id`='"+ pid +"';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Delete item photo failed");
                Debug.WriteLine(ex.Message.ToString());
            }

            //delete lineitem
            try
            {
                MySql.Data.MySqlClient.MySqlConnection conn;
                string myConnectionString;
                //myConnectionString = "server=127.0.0.1;uid=root;pwd=zzaaqq11;database=frphotosg;SslMode=none";
                myConnectionString = @"server=localhost;userid=root;password=12345;database=kidzania";

                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                string query = "DELETE FROM `kidzania`.`lineitem` WHERE `p_id`='" + pid + "';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Delete line item failed");
                Debug.WriteLine(ex.Message.ToString());
            }
            try
            {
                MySql.Data.MySqlClient.MySqlConnection conn;
                string myConnectionString;
                //myConnectionString = "server=127.0.0.1;uid=root;pwd=zzaaqq11;database=frphotosg;SslMode=none";
                myConnectionString = @"server=localhost;userid=root;password=12345;database=kidzania";

                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                string query = "DELETE FROM `kidzania`.`order` WHERE `pid`='" + pid + "';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Delete order failed");
                Debug.WriteLine(ex.Message.ToString());
            }
        }

        public static void Insert_profile(string profile, string pid)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            //myConnectionString = "server=127.0.0.1;uid=root;pwd=zzaaqq11;database=frphotosg;SslMode=none";
            myConnectionString = @"server=localhost;userid=root;password=12345;database=kidzania";
            
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = myConnectionString;
            conn.Open();
            string query = "INSERT INTO profile (pid, profile, status) VALUES('" + pid + "','" + profile + "','selection');";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        

        public static List<Product> GetProductsFromDB()
        {
            string connstring = @"server=localhost;userid=root;password=12345;database=kidzania";
            List<Product> products = new List<Product>();

            MySqlConnection conn = null;
            conn = new MySqlConnection(connstring);
            conn.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * from product", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Setting the product list
                        products.Add(new Product()
                        {
                            ProductId = reader["product_id"].ToString(),
                            ProductName = reader["name"].ToString(),
                            ProductImagePath = reader["image"].ToString(),
                            ProductDescription = reader["description"].ToString(),
                            ProductQuantityConstraint = reader["quantity_constraint"].ToString(),
                            OrginalPrice = Decimal.Parse(reader["original_price"].ToString()),
                            OriginalGST = Decimal.Parse(reader["original_GST"].ToString()),
                            ProductVisibility = bool.Parse(reader["visibility"].ToString()),
                            PhotoProduct = bool.Parse(reader["photo_product"].ToString()),
                            UpdatedBy = reader["updated_by"].ToString(),
                            UpdatedAt = DateTime.Parse(reader["updated_at"].ToString()).ToString("yyyy-MM-dd HH:mm:ss")
                        });
                        if (!reader["pwp_price"].ToString().Equals(""))
                        {
                            products[products.Count - 1].PwpPrice = Decimal.Parse(reader["pwp_price"].ToString());
                            products[products.Count - 1].PwpGST = Decimal.Parse(reader["pwp_GST"].ToString());
                        }
                        else
                        {
                            products[products.Count - 1].PwpPrice = 0;
                            products[products.Count - 1].PwpGST = 0;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return products;
        }


        //Return All Product in the datbase excluding showphoto=0
        [WebMethod]
        public static string GetProducts()
        {
            List<Product> allProducts = GetProductsFromDB();
            return new JavaScriptSerializer().Serialize(allProducts);
        }


        [WebMethod]
        public static string GetProductbyId(string id)
        {
            List<Product> allProducts = GetProductsFromDB();
            Product foundProduct = null;
            foreach (Product p in allProducts) {
                if (p.ProductId.Equals(id)) {
                    foundProduct = p;
                }
            }
            return new JavaScriptSerializer().Serialize(foundProduct);
        }
        
        
        [WebMethod]
        public static string CalculateTotalCost(string cartItems)
        {
            List<Product> allProducts = GetProductsFromDB();
            CartItem[] items = new JavaScriptSerializer().Deserialize<CartItem[]>(cartItems);

            List<CartItemWithCost> allCosts = new List<CartItemWithCost>();
            if (items.Length > 0)
            {
                string basicItem = items[0].productId;
                decimal basicItemPrice = 9999;
                //find the item applies basic price
                foreach (CartItem item in items)
                {
                    foreach (Product p in allProducts)
                    {
                        if (item.productId == p.ProductId)
                        {
                            if (p.OrginalPrice <= basicItemPrice)
                            {
                                basicItemPrice = p.OrginalPrice;
                                basicItem = p.ProductId;
                            }
                        }
                    }
                }

                CartItem baseItem = new CartItem();
                foreach (CartItem item in items)
                {
                    if (item.productId == basicItem)
                    {
                        baseItem = item;
                    }
                }

                //calculate for the basic item
                foreach (Product p in allProducts)
                {
                    if (baseItem.productId == p.ProductId)
                    {
                        CartItemWithCost originalItem = new CartItemWithCost();
                        originalItem.ProductId = baseItem.productId;
                        originalItem.Cost = p.PwpPrice * (baseItem.quantity - 1) + p.OrginalPrice;
                        allCosts.Add(originalItem);
                    }
                }

                //if the cart contains more than 1 item 
                //pwp products
                foreach (CartItem item in items)
                {
                    if (item.productId != baseItem.productId)
                    {
                        foreach (Product p in allProducts)
                        {
                            if (item.productId == p.ProductId)
                            {
                                CartItemWithCost pwpItem = new CartItemWithCost();
                                pwpItem.ProductId = item.productId;
                                pwpItem.Cost = p.PwpPrice * item.quantity;
                                allCosts.Add(pwpItem);
                            }
                        }
                    }
                }


                //calculate total cost
                decimal totalCost = 0;
                foreach (CartItemWithCost c in allCosts)
                {
                    totalCost += c.Cost;
                }
                CartItemWithCost totalItem = new CartItemWithCost();
                totalItem.ProductId = "total";
                totalItem.Cost = totalCost;
                allCosts.Add(totalItem);

                return new JavaScriptSerializer().Serialize(allCosts);
            }
            else
            {
                return "No item in the cart";
            }
            
        }


        [WebMethod]
        public static string GetCartItems()
        {
            if (incartItems != null)
            {
                return new JavaScriptSerializer().Serialize(incartItems);
            }
            else
            {
                return null;
            }
        }
        

        public class CartItem
        {
            public string productId { get; set; }
            public int quantity { get; set; }
        }


        public class CartItemWithCost
        {
            public string ProductId { get; set; }
            public decimal Cost { get; set; }
        }
    }
}
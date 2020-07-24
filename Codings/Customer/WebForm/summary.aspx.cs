using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm
{
    public partial class summary : System.Web.UI.Page
    {
       

        public static List<InCartItem> cartItems;

        protected void Page_Load(object sender, EventArgs e)
        {

            /*
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
			*/


            if (Request.Form["summarytotalcost"] != null)
            {
                //Create an order insert 
                /*
                if (Session["pid"] == null)
                    Response.Redirect("http://photos.kidzania.com.sg");

                string pid = Session["pid"].ToString();
                */

                //TESTING
                /*
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                var stringChars = new char[8];
                var random = new Random();
                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);
                string today = DateTime.Now.ToString("MMddyyyy");
                string pid = "kidzsg" + finalString + today;
                */

                string pid = Session["pid"].ToString();

                //TESTING
                string orderId = pid.Replace("kidzsg", "").Substring(0, 5);

                Order newOrder = new Order();
                newOrder.Order_Code = orderId;
                Debug.WriteLine("ordercode: " + newOrder.Order_Code);
                newOrder.p_id = pid;
                newOrder.status = "Unpaid";
                newOrder.total_amount = Decimal.Parse(Request.Form["summarytotalcost"]);
                newOrder.Updated_At = DateTime.Now;

                //prevent duplicated primary key
                if (check_pid_exist(pid) == true)
                {
                    Debug.WriteLine("There is an order already!");
                }
                else
                {
                    string profileId = Session["profile"].ToString();
                    Update_profile(profileId, pid);
                    insertOrder(newOrder);


                    //Create List of Line items
                    List<Product> allproducts = GetProductsFromDB();

                    List<LineItem> lineitems = new List<LineItem>();
                    List<ItemPhoto> itemphotos = new List<ItemPhoto>();

                    cartItems = new List<InCartItem>();
                    foreach (Product p in allproducts)
                    {
                        if (p.PhotoProduct)
                        {
                            if (Request.Form.GetValues("summary" + p.ProductId) != null)
                            {
                                LineItem newLineItem = new LineItem();
                                newLineItem.LineItem_Id = orderId + "_" + p.ProductId;
                                newLineItem.Pid = pid;
                                newLineItem.Product_Id = p.ProductId;
                                newLineItem.Photos = string.Join("|", Request.Form.GetValues("summary" + p.ProductId));
                                newLineItem.Quantity = Request.Form.GetValues("summary" + p.ProductId).Length;
                                newLineItem.LineItem_Amount = Decimal.Parse(Request.Form["summary" + p.ProductId + "cost"]);
                                newLineItem.Status = "Unpaid";
                                newLineItem.Updated_At = DateTime.Now;
                                lineitems.Add(newLineItem);
                                insertLineItem(newLineItem);

                                //create list of item photos
                                string[] photo_array = Request.Form.GetValues("summary" + p.ProductId);
                                string photoElements = "";
                                foreach (string photo in photo_array)
                                {
                                    ItemPhoto itemPhoto = new ItemPhoto();
                                    itemPhoto.ItemPhoto_Id = orderId + "_" + p.ProductId + "_" + photo;
                                    itemPhoto.LineItem_Id = orderId + "_" + p.ProductId;
                                    itemPhoto.Pid = pid;
                                    itemPhoto.Photo = photo;
                                    DateTime dt = DateTime.Now;
                                    itemPhoto.Updated_At = dt;
                                    itemphotos.Add(itemPhoto);

                                    InCartItem ci = new InCartItem();
                                    string[] photoID1 = photo.Split('/');
                                    string[] photoID2 = photoID1[photoID1.Length - 1].Split('.');
                                    string phID = photoID2[0];
                                    ci.photoId = "photoID" + phID;
                                    ci.productId = p.ProductId;
                                    ci.photoSource = photo;
                                    cartItems.Add(ci);
                                    //generate photoelements
                                    photoElements += "<div class='column'><img src='" + photo + "' style='width:60px'></div>";

                                    insertItemPhoto(itemPhoto);
                                }

                                //add html
                                summarydiv.InnerHtml += "<tr><th>" + p.ProductName + " </th><th><div class='row'> " + photoElements + "</div></th><th>" + Request.Form.GetValues("summary" + p.ProductId).Length + " </th><th>SGD " + Request.Form["summary" + p.ProductId + "cost"] + " </th></tr>";

                            }
                        }
                    }

                    foreach (Product p in allproducts)
                    {
                        if (!p.PhotoProduct)
                        {
                            string element = "summary" + p.ProductId;
                            string costelement = "summary" + p.ProductId + "cost";
                            if (Request.Form[element] != null)
                            {
                                LineItem newLineItem = new LineItem();
                                newLineItem.LineItem_Id = orderId + "_" + p.ProductId;
                                newLineItem.Pid = pid;
                                newLineItem.Product_Id = p.ProductId;
                                newLineItem.Photos = "";
                                newLineItem.Quantity = int.Parse(Request.Form[element]);
                                newLineItem.LineItem_Amount = Decimal.Parse(Request.Form[costelement]);
                                newLineItem.Status = "Unpaid";
                                lineitems.Add(newLineItem);
                                insertLineItem(newLineItem);

                                InCartItem ci = new InCartItem();
                                ci.photoId = "photoProduct";
                                ci.productId = p.ProductId;
                                ci.photoSource = Request.Form[element];
                                cartItems.Add(ci);
                                //summarydiv.InnerHtml += p.ProductName + ": " + lineitems[lineitems.Count - 1].LineItem_Amount + "<br>";

                                if (newLineItem.Product_Id == "dc")
                                {
                                    email_ctn.InnerHtml += "<span style='color: red;'> <b>Email required: </b></span ><input type='text' id='useremail' size ='50' runat ='server' /><span style='color: red; margin-left: 5px;' id ='email_status' ></span><div id='email_validator' runat='server' ></div> <p style='display:none' id='pidSession'>" + pid + "</p> ";
                                }
                                summarydiv.InnerHtml += "<tr><th>" + p.ProductName + " </th><th> - </th><th>" + Request.Form[element] + " </th><th>SGD " + Request.Form[costelement] + " </th></tr>";
                            }
                        }
                    }
                    Session["CartItems"] = cartItems;
                    summarydiv.InnerHtml += "<tr><th colspan='4' style='text-align:right'>Total Cost: <b>SGD " + Request.Form["summarytotalcost"] + "</b></th></tr>";
                }
            }



            //query string
            else if (Request.QueryString["retry"] != null)
            {
                if (Session["pid"] == null)
                    Response.Redirect("http://photos.kidzania.com.sg");
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

        public class InCartItem
        {
            public string productId { get; set; }
            public string photoId { get; set; }
            public string photoSource { get; set; }
        }

        public class Order
        {
            public string Order_Code;
            public string p_id;
            public decimal total_amount;
            public string status;
            public DateTime Updated_At { get; set; }
        }

        public class LineItem
        {
            public string LineItem_Id { get; set; }
            public string Pid { get; set; }
            public string Product_Id { get; set; }
            public string Photos { get; set; }
            public int Quantity { get; set; }
            public decimal LineItem_Amount { get; set; }
            public string Status { get; set; }
            public DateTime Updated_At { get; set; }
        }

        public class ItemPhoto
        {
            public string ItemPhoto_Id { get; set; }
            public string Pid { get; set; }
            public string LineItem_Id { get; set; }
            public string Photo { get; set; }
            public string Assigned_PrinterId { get; set; }
            public string Printing_Status { get; set; }
            public DateTime Updated_At { get; set; }
        }

        public static void insertOrder(Order order)
        {
            string connstring = @"server=localhost;userid=root;password=12345;database=kidzania";
            MySqlConnection conn = null;
            conn = new MySqlConnection(connstring);
            conn.Open();
            try
            {
                string query = "insert into `order` ( pid, order_id, total_amount, status, updatedAt) VALUES('" + MySqlHelper.EscapeString(order.p_id) + "','" + MySqlHelper.EscapeString(order.Order_Code) + "','" + MySqlHelper.EscapeString(order.total_amount.ToString()) + "','" + MySqlHelper.EscapeString(order.status) + "','" + MySqlHelper.EscapeString(order.Updated_At.ToString("yyyy-MM-dd HH:mm:ss")) + "');";
                
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Insert Order Record failed");
                Debug.WriteLine(ex.Message.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static void insertLineItem(LineItem lineitem)
        {
            string connstring = @"server=localhost;userid=root;password=12345;database=kidzania";
            MySqlConnection conn = null;
            conn = new MySqlConnection(connstring);
            conn.Open();
            try
            {
                string query = "insert into `lineitem` (lineitem_id, p_id, product_id, photos, item_amount, quantity, status, updatedAt) VALUES('" + MySqlHelper.EscapeString(lineitem.LineItem_Id) + "','" + MySqlHelper.EscapeString(lineitem.Pid) + "','" + MySqlHelper.EscapeString(lineitem.Product_Id) + "','" + MySqlHelper.EscapeString(lineitem.Photos) + "','" + MySqlHelper.EscapeString(lineitem.LineItem_Amount.ToString()) + "','" + MySqlHelper.EscapeString(lineitem.Quantity.ToString()) + "','" + MySqlHelper.EscapeString(lineitem.Status) + "','" + MySqlHelper.EscapeString(lineitem.Updated_At.ToString("yyyy-MM-dd HH:mm:ss")) + "');";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Insert Lineitem Record failed");
                Debug.WriteLine(ex.Message.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static void insertItemPhoto(ItemPhoto itemphoto)
        {
            string connstring = @"server=localhost;userid=root;password=12345;database=kidzania";
            MySqlConnection conn = null;
            conn = new MySqlConnection(connstring);
            conn.Open();
            try
            {
                string query = "insert into `itemphoto` (itemphoto_id, photo, p_id, lineitem_id, updated_at) VALUES('" + MySqlHelper.EscapeString(itemphoto.ItemPhoto_Id) + "','" + MySqlHelper.EscapeString(itemphoto.Photo) + "','" + MySqlHelper.EscapeString(itemphoto.Pid) + "','" + MySqlHelper.EscapeString(itemphoto.LineItem_Id) + "','" + MySqlHelper.EscapeString(itemphoto.Updated_At.ToString("yyyy-MM-dd HH:mm:ss")) + "');";
              
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Insert itemphoto Record failed");
                Debug.WriteLine(ex.Message.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        //Return All Product in the datbase excluding showphoto=0
        [WebMethod]
        public static void insertEmail(string email, string pid)
        {
            string connstring = @"server=localhost;userid=root;password=12345;database=kidzania";
            MySqlConnection conn = null;
            conn = new MySqlConnection(connstring);
            conn.Open();
            try
            {
                string query = "insert into `email` (pid, email) VALUES('" + MySqlHelper.EscapeString(pid) + "','" + MySqlHelper.EscapeString(email) + "');";
               
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Insert email Record failed");
                Debug.WriteLine(ex.Message.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
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

        public static void Update_profile(string profile, string pid)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            //myConnectionString = "server=127.0.0.1;uid=root;pwd=zzaaqq11;database=frphotosg;SslMode=none";
            myConnectionString = @"server=localhost;userid=root;password=12345;database=kidzania";

            conn = new MySql.Data.MySqlClient.MySqlConnection();
            conn.ConnectionString = myConnectionString;
            conn.Open(); 
            //string query = "UPDATE `profile` SET status = 'processing' where profile = '" + MySqlHelper.EscapeString(profile) + "'";
            string query = "INSERT INTO profile (pid, profile, status) VALUES('" + pid + "','" + profile + "','processing');";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }


        public static bool check_pid_exist(string pid)
        {
            bool exist = false;
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = @"server=localhost;userid=root;password=12345;database=kidzania";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                string query = "SELECT count(*) FROM order WHERE pid = '" + MySqlHelper.EscapeString(pid) + "';";
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
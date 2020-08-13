using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm
{
    public partial class profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (Session["CapturedImageBase64"] != null && Session["CapturedImageBase64"] != "")
            {
                string photobase64 = Session["CapturedImageBase64"].ToString();
                profile_photo.InnerHtml = "<img style='max-height: 300px !important;' src='" + photobase64 + "' />";
                Dictionary<string, string> photoprofile = getphotoprofile(photobase64); */


                Dictionary<string, string> photoprofile = new Dictionary<string, string>();
                photoprofile.Add("photoID1", "1.jpg");
                photoprofile.Add("photoID2", "2.jpg");
                photoprofile.Add("photoID3", "12.jpg");
                

               string currentApplicationPath = HttpContext.Current.Request.PhysicalApplicationPath;

                Uri myuri = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
                string pathQuery = myuri.PathAndQuery;
                string host = myuri.ToString().Replace(pathQuery, "") + "/";
                host = "/";
                if (photoprofile.Count > 0)
                {
                    photo_gallery_ctn.InnerHtml += "<div style='text-align:center;'>Please select a profile: </div>";
                    int x = 1;
                    string photofullpath = "";
                    foreach (var photo in photoprofile)
                    {
                        //string photodata = photo.Key;
                        //string profile_id = photodata.Split('|')[1];
                        //string photoprofileimage = photo.Value;

                    //testing purpose
                        string photodata = photo.Key;
                        string photoprofileimage = photo.Value;
                        string profile_id = photodata;

                        string photoid = photoprofileimage.Substring(photoprofileimage.LastIndexOf('/') + 1);
                        photoid = photoid.Substring(0, photoid.IndexOf('.'));

                        photoprofileimage = photoprofileimage.Replace(@"Xeric/files/kidzania/", "");
                        string photowatermarked_filename = photoprofileimage.Substring(photoprofileimage.LastIndexOf('/') + 1);
                        string local_photoimage = photoprofileimage.Replace("/", "");

                        using (WindowsIdentity.GetCurrent().Impersonate())
                        {
                            if (!File.Exists(currentApplicationPath + "photos//" + local_photoimage)) { } //"profiles//"
                        else
                            {
                                File.Delete(currentApplicationPath + "photos//" + local_photoimage);
                            }
                        }

                        photo_gallery_ctn.InnerHtml += "<div class=\"gallery\">";
                    //ON CLICK will submit form to selection.aspx
                        photo_gallery_ctn.InnerHtml += "<button onclick='$(\"#pf\").val(\"" + photodata + "\");' type='submit' value='" + photodata + "'><img class=\"thumbnail\" src=\"/Content/photos/" + local_photoimage + "\"></button>";
                        //photo_gallery_ctn.InnerHtml += "<button onclick='$(\"#pf\").val(\"" + photodata + "\");' type='submit' value='" + photodata + "'><img class='thumbnail' src='" + host + "profiles/" + local_photoimage + "'></button>";
                        photo_gallery_ctn.InnerHtml += "<div>ID: " + profile_id + "</div>";
                        photo_gallery_ctn.InnerHtml += "</div>";
                        x++;
                        photofullpath += photo.Key + "|";
                    }

                    int dc_amt = 0;
                    if ((x - 1) > 0 && (x - 1) < 15) dc_amt = 5;
                    if ((x - 1) >= 15 && (x - 1) < 30) dc_amt = 10;
                    if ((x - 1) >= 30 && (x - 1) < 45) dc_amt = 15;
                    if ((x - 1) >= 45 && (x - 1) < 60) dc_amt = 20;
                    if ((x - 1) > 60) dc_amt = 25;

                    sa.Attributes["value"] = dc_amt.ToString();
                    dc.Attributes["value"] = (x - 1).ToString();
                    js.Attributes["value"] = photofullpath.TrimEnd('|');
                }
                else
                {
                    //display no match
                    photo_gallery_ctn.InnerHtml += "No profile found for this face.";
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
            //}
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

        protected Dictionary<string, string> getphotoprofile(string base64image)
        {
            Dictionary<string, string> retval = new Dictionary<string, string>();

            //data:image/jpeg;base64,
            string base64imagewithoutheader = base64image.Replace("data:image/jpeg;base64,", "");

            string datval = Session["dateval"].ToString();
            string temp_startdate = datval + " 00:00:00";
            string temp_enddate = datval + " 23:59:00";


            //NEW CODE TO GET PROFILE
            List<string> mydate = new List<string>();
            string profile_date = Session["DateVal"].ToString();
            mydate.Add(profile_date);

            Post_Search_Profile sProfile = new Post_Search_Profile();
            sProfile.approve = "0"; // “0” – search all; “1” – search only approve
            sProfile.groups = mydate;  // date in DDMMYYYY
            sProfile.image64 = base64imagewithoutheader; // image in base64
            sProfile.threshold = 60; // search threshold value – default 60

            string apipath = "http://152.10.200.26:8081/api";
            var hclient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(sProfile),
            Encoding.UTF8, "application/json");
            var response = hclient.PostAsync(apipath + "/media/search/", content).Result;

            List<SearchData> mResult = new List<SearchData>();
            if (response.IsSuccessStatusCode)
            {
                using (HttpContent data = response.Content)
                {
                    Task<string> tresult = data.ReadAsStringAsync();
                    string str = tresult.Result;
                    mResult = JsonConvert.DeserializeObject<List<SearchData>>(str);
                }
            }

            if (mResult.Count > 0)
            {
                for (int x = 0; x < mResult.Count; x++)
                {
                    string photodata = mResult[x].group.ToString() + "|" + mResult[x].profile_id.ToString();
                    string photox = mResult[x].image.ToString();
                    retval.Add(photodata, photox);
                }
                return retval;
            }

            return retval;
        }
    }
}
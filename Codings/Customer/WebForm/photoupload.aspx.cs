using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm
{
    public partial class photoupload : System.Web.UI.Page
    {
        public class Result_Detect_Face
        {
            public string image { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            btn_reset.Visible = false;
            if (Request.Form["pf"] != null)
            {
                string profile = Request.Form["pf"].Replace("%7c", "|");
                if (profile != "default")
                {
                    Session["CapturedImageBase64"] = "data:image/jpeg;base64," + profile;
                    //Response.Redirect("profile.aspx");
                }
                if (profile.Equals("/Content/photos/PU1.jpg") || profile.Equals("/Content/photos/PU2.jpg") || profile.Equals("/Content/photos/PU3.jpg") || profile.Equals("/Content/photos/PU4.jpg"))
                {
                    Session["CapturedImageBase64"] = "data:image/jpeg;base64," + profile;
                    Response.Redirect("profile.aspx");
                }
            }

            if (IsPostBack && FileUpload1.PostedFile != null && FileUpload1.PostedFile.FileName.Length > 0)
            {
                photo_profile_ctn.InnerHtml = "<div>Select a face:</div>";
                List<string> testingFacesPhotos = new List<string>();
                testingFacesPhotos.Add("/Content/photos/PU1.jpg");
                testingFacesPhotos.Add("/Content/photos/PU2.jpg");
                testingFacesPhotos.Add("/Content/photos/PU3.jpg");
                testingFacesPhotos.Add("/Content/photos/PU4.jpg");
                for (int x = 0; x < testingFacesPhotos.Count; x++)
                {
                    string Base64Face = testingFacesPhotos[x];
                    photo_profile_ctn.InnerHtml += "<div class=\"gallery_profile\">";
                    //ON CLICK will submit form to profile.aspx
                    photo_profile_ctn.InnerHtml += "<button onclick='$(\"#pf\").val(\"" + Base64Face + "\");' type='submit' value='" + x + "'><img class='thumbnail' src='" + Base64Face + "'></button>";
                    photo_profile_ctn.InnerHtml += "</div>";
                }
                FileUpload1.Visible = false;
                btn_reset.Visible = true;

                //Response.Redirect("profile.aspx");
                /*
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                Byte[] b = new byte[FileUpload1.PostedFile.ContentLength];
                FileUpload1.PostedFile.InputStream.Read(b, 0, b.Length);

                
                string apipath = "http://152.10.200.26:8081/api"; //path to VM2

                Result_Detect_Face dFace = new Result_Detect_Face();
                dFace.image = Convert.ToBase64String(b);

                //This will send the photo as base64 to the API to do face recognition.
                //API will detect if photo has human face, if human face detected, it will crop to include face portion.
                //Else it will return unsuccessful if no human face is found in the photo
                String Base64Face = "";
                var pclient = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(dFace), Encoding.UTF8, "application/json");
                var response = pclient.PostAsync(apipath + "/media/", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent data = response.Content)
                    {
                        Task<string> result = data.ReadAsStringAsync();
                        string jsondata = result.Result;
                        var mResult = JsonConvert.DeserializeObject<List<Result_Detect_Face>>(jsondata);
                        photo_profile_ctn.InnerHtml = "<div>Select a face:</div>";
                        for (int x = 0; x < mResult.Count; x++)
                        {
                            Base64Face = mResult[x].image;
                            photo_profile_ctn.InnerHtml += "<div class=\"gallery_profile\">";
                            //ON CLICK will submit form to profile.aspx
                            photo_profile_ctn.InnerHtml += "<button onclick='$(\"#pf\").val(\"" + Base64Face + "\");' type='submit' value='" + x + "'><img class='thumbnail' src='data:image/jpeg;base64," + Base64Face + "'></button>";
                            photo_profile_ctn.InnerHtml += "</div>";
                        }
                        FileUpload1.Visible = false;
                        btn_reset.Visible = true;
                    }
                }
                else
                {
                    photo_profile_ctn.InnerHtml = "<div>Face not detected. Please upload another photo:</div>";
                }*/
            }

        }

        public int GetOriginalLengthInBytes(string base64string)
        {
            if (string.IsNullOrEmpty(base64string)) { return 0; }

            var characterCount = base64string.Length;
            var paddingCount = base64string.Substring(characterCount - 2, 2)
                                           .Count(c => c == '=');
            return (3 * (characterCount / 4)) - paddingCount;
        }

        protected void reset(object sender, EventArgs e)
        {
            photo_profile_ctn.InnerHtml = "";
            FileUpload1.Visible = true;
        }

        protected void GoToSelection(object sender, EventArgs e)
        {
            Response.Redirect("profile.aspx");
        }

        protected void Upload(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                Byte[] b = new byte[FileUpload1.PostedFile.ContentLength];

                using (System.Drawing.Image myImage = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream))
                {
                    int height = myImage.Height;
                    int width = myImage.Width;
                }
                FileUpload1.PostedFile.InputStream.Read(b, 0, b.Length);
                string base64String = System.Convert.ToBase64String(b, 0, b.Length);
                Session["CapturedImageBase64"] = "data:image/jpeg;base64," + base64String;
                btnProceed.Style.Add("display", "block");
            }
        }


        private static byte[] ConvertHexToBytes(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

        [WebMethod(EnableSession = true)]
        public static string GetCapturedImage()
        {
            string url = HttpContext.Current.Session["CapturedImage"].ToString();
            HttpContext.Current.Session["CapturedImage"] = null;
            return url;
        }
    }
}

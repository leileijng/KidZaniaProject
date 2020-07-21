using System;
using System.Collections.Generic;
using KidZaniaPhotoPrintingAdminPortal.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Web;
using System.IO;
using System.Web.Hosting;

namespace KidZaniaPhotoPrintingAdminPortal.APIs
{
    public class PausedController : ApiController
    {
        private Database database = new Database();

        [HttpGet]
        [Route("api/paused")]
        public IHttpActionResult getPausedCopys()
        {
            try
            {
                var prod = database.itemphotoes.Where(i => i.lineitem.status.Equals("Paused") || i.lineitem.status.Equals("Cancelled")).OrderBy(n => n.updated_at).OrderBy(m => m.lineitem_id).Select(x => new
                {
                    order_id = x.lineitem.order.order_id,
                    lineItem_id = x.lineitem_id,
                    photo_id = x.itemphoto_id,
                    photo_qty = x.lineitem.quantity,
                    photo_path = x.photo,
                    photo_status = x.printing_status,
                    assigned_printer = x.printer.name,
                    lineitem_status = x.lineitem.status,
                    updated_time = x.updated_at,
                    lineItem_name = x.lineitem.product.name
                }
                ).ToList();
                return Ok(prod);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPut]
        [Route("api/paused/{lineItemId}")]
        public IHttpActionResult updateLineItemStatus(string lineItemId, [FromBody]JObject data)
        {
            try
            {
                var lineitem = database.lineitems.SingleOrDefault(x => x.lineitem_id == lineItemId);
                lineitem.status = data["status"].ToString();

                var photos = database.itemphotoes.Where(i => i.lineitem_id == lineItemId).ToList();

                for (int i = 0; i < photos.Count; i++)
                {
                    string photoId = photos[i].itemphoto_id;
                    var photoItem = database.itemphotoes.SingleOrDefault(m => m.itemphoto_id == photoId);
                    photoItem.printing_status = data["status"].ToString();
                }

                database.SaveChanges();
                return Ok(new { message = "Line Item status has been updated to " + data["status"] });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

        [Route("api/paused/deletePhoto")]
        [HttpDelete]
        public IHttpActionResult Delete([FromBody]JObject data)
        {
            string photoId = data["photoId"].ToString();
            try
            {
                itemphoto foundPhoto = database.itemphotoes.SingleOrDefault(p => p.itemphoto_id.Equals(photoId));

                if (foundPhoto != null)
                {
                    lineitem foundLineItem = foundPhoto.lineitem;
                    if(foundLineItem.quantity > 1)
                    {
                        database.itemphotoes.Remove(foundPhoto);
                        //int qty = foundLineItem.quantity - 1;
                        foundLineItem.quantity--;
                    }
                    else
                    {
                        foundPhoto.printing_status = "Cancelled";
                        foundLineItem.status = "Cancelled";
                        
                    }
                    database.SaveChanges();
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, "Photo is not found!");
                }
                return Ok(new { message = "Photo deleted!" });
            }
            catch (Exception exceptionObject)
            {
                return BadRequest("Unable to delete product because " + exceptionObject.Message.ToString());
            }
        }

        [Route("api/paused/UploadPhoto/{imgName}")]
        [HttpPost]
        public IHttpActionResult UploadImgFile(string imgName)
        {
            var httpContext = HttpContext.Current;
            try
            {
                // Check for any uploaded file  
                if (httpContext.Request.Files.Count > 0)
                {
                    //Loop through uploaded files  
                    for (int i = 0; i < httpContext.Request.Files.Count; i++)
                    {
                        HttpPostedFile httpPostedFile = httpContext.Request.Files[i];
                        if (httpPostedFile != null)
                        {
                            // Construct file save path  
                            var ext = Path.GetExtension(httpPostedFile.FileName).ToLower();
                            var fileSavePath = Path.Combine(HostingEnvironment.MapPath("~/Content/Photos/"), imgName + ext);
                            if (!File.Exists(fileSavePath))
                            {
                                // Save the uploaded file  
                                httpPostedFile.SaveAs(fileSavePath);
                            }
                        }
                    }
                }
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut]
        [Route("api/paused/updateImgPath")]
        public IHttpActionResult updateImgPath([FromBody]JObject data)
        {
            try
            {
                string photoid = data["photoId"].ToString();
                var photoItem = database.itemphotoes.SingleOrDefault(x => x.itemphoto_id == photoid);
                
                photoItem.photo = "/Content/Photos/" + data["imgName"].ToString();
                string[] ids = photoid.Split('_');
                database.SaveChanges();
                return Ok(new { message = "Line Item status has been updated to " + data["status"] });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

    }
}

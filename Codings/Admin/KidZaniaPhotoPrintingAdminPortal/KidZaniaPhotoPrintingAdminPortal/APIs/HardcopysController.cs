using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using KidZaniaPhotoPrintingAdminPortal.Models;
using Newtonsoft.Json.Linq;

namespace KidZaniaPhotoPrintingAdminPortal.APIs
{
    public class HardcopysController : ApiController
    {
        private Database database = new Database();


        [HttpGet]
        [Route("api/hardcopys")]
        public IHttpActionResult getHardCopys()
        {
            try
            {
                var prod = database.itemphotoes.Where(i => i.lineitem.product_id.Equals("a5")).OrderBy(n => n.updated_at).OrderBy(m=>m.lineitem_id).Select(x => new
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
        [Route("api/hardcopys")]
        public IHttpActionResult updatePhotoStatus([FromBody]JObject data)
        {
            try
            {
                string photoId = data["photoId"].ToString();
                var itemPhoto = database.itemphotoes.SingleOrDefault(x => x.itemphoto_id == photoId);
                var status = data["status"].ToString();
                itemPhoto.printing_status = status;
                itemPhoto.updated_at = DateTime.Now;

                var lineItem = itemPhoto.lineitem;
                if(status == "Printing")
                {
                    lineItem.status = status;
                }
                else if (status == "Completed")
                {
                    var itemPhotos = database.itemphotoes.Where(x => x.lineitem_id == lineItem.lineitem_id);
                    bool allCompleted = true;
                    foreach(var photo in itemPhotos)
                    {
                        if(photo.printing_status != "Completed")
                        {
                            allCompleted = false;
                        }
                    }
                    if (allCompleted)
                    {
                        lineItem.status = "Completed";
                    }
                    else
                    {
                        lineItem.status = "Printing";
                    }
                }
                
                database.SaveChanges();
                return Ok(new { message = "Photo status has been updated to " + status });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }


        [HttpPut]
        [Route("api/hardcopys/{lineItemId}")]
        public IHttpActionResult updateLineItemStatus(string lineItemId, [FromBody]JObject data)
        {
            try
            {
                var lineitem = database.lineitems.SingleOrDefault(x => x.lineitem_id == lineItemId);
                lineitem.status = data["status"].ToString();

                var photos = database.itemphotoes.Where(i => i.lineitem_id == lineItemId).ToList();

                for (int i=0; i<photos.Count; i++)
                {
                    string photoId = photos[i].itemphoto_id;
                    var photoItem = database.itemphotoes.SingleOrDefault(m => m.itemphoto_id == photoId);
                    photoItem.printing_status = data["status"].ToString();
                }

                database.SaveChanges();
                return Ok(new { message = "Photo status has been updated to " + data["status"] });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

    }
}
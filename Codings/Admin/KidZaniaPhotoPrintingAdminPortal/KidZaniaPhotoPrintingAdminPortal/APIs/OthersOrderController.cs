using KidZaniaPhotoPrintingAdminPortal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace KidZaniaPhotoPrintingAdminPortal.APIs
{
    public class OthersOrderController : ApiController
    {
        private Database database = new Database();

        [Route("api/others")]
        [HttpGet]
        public IHttpActionResult getOrder()
        {
            try
            {
                var order = database.orders.Select(x => new
                {
                    order_id = x.order_id,
                    p_id = x.pid,
                    product = database.lineitems.Select(y => new
                    {
                        p_id = y.p_id,
                        product_id = y.product_id,
                        status = y.status,
                        lineitem_id = y.lineitem_id
                    }).Where(y => y.p_id == x.pid && (y.product_id == "ec" || y.product_id == "kc" || y.product_id == "mg")).ToList(),
                    status = x.status
                }
                ).Where(x => x.status == "paid" && x.product.Count != 0).ToList();
                return Ok(order);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [Route("api/others/{id}")]
        [HttpGet]
        public IHttpActionResult getOrderById(string id)
        {
            try
            {
                var order = database.orders.Select(x => new
                {
                    order_id = x.order_id,
                    p_id = x.pid,
                    product = database.lineitems.Select(y => new
                    {
                        p_id = y.p_id,
                        product_id = y.product_id,
                        quantity = y.quantity,
                        productname = y.product.name,
                        lineitem_id = y.lineitem_id,
                        photos = y.photos,
                        status = y.status
                    }).Where(y => y.p_id == x.pid && (y.product_id == "ec" || y.product_id == "kc" || y.product_id == "mg")).ToList(),
                    status = x.status
                }
                ).SingleOrDefault(x => x.status == "paid" && x.product.Count != 0 && x.order_id == id);
                return Ok(order);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [Route("api/others/{id}")]
        [HttpPut]
        public IHttpActionResult changeStatus(string id, string status)
        {
            try
            {
                var lineitems = database.lineitems.SingleOrDefault(y => y.lineitem_id == id);
                lineitems.status = status;
                var itemphotoes = database.itemphotoes.Where(y => y.lineitem_id == id).ToList();
                foreach(var itemphoto in itemphotoes)
                {
                    itemphoto.printing_status = status;
                }
                database.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [Route("api/others/downloadImage/{order_id}")]
        [HttpPut]
        public IHttpActionResult UploadFile(string order_id, string photos, string path)
        {
            
            try
            {
                if (photos.Contains('|'))
                {
                    string[] photo = photos.Split('|');
                    foreach(string onephoto in photo)
                    {
                        string[] filename = onephoto.Split('/');
                        string newfilename = filename[filename.Length - 1];
                        if (File.Exists(Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename)))
                        {
                            File.Delete(Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename));
                        }
                        if (File.Exists(HostingEnvironment.MapPath(onephoto)))
                        {
                            File.Copy(HostingEnvironment.MapPath(onephoto), Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename));
                        }
                    }
                }
                else
                {
                    if (File.Exists(HostingEnvironment.MapPath(photos)))
                    {
                        string[] filename = photos.Split('/');
                        string newfilename = filename[filename.Length - 1];
                        if (File.Exists(Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename)))
                        {
                            File.Delete(Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename));
                        }
                        File.Copy(HostingEnvironment.MapPath(photos), Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename));
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok();
        }

        [Route("api/others/moveImage/{order_id}")]
        [HttpPut]
        public IHttpActionResult moveFile(string order_id, string photos, string product_id)
        {
            try
            {
                string path = "";
                string newpath = "";

                if(product_id == "mg")
                {
                    path = "~/Content/OrderPhotos/Magnet/";
                    newpath = "~/Content/OrderPhotos/Magnet/Completed_Magnet/";
                }
                else if (product_id == "kc")
                {
                    path = "~/Content/OrderPhotos/Keychain/";
                    newpath = "~/Content/OrderPhotos/Keychain/Completed_Keychain/";
                }

                if (photos.Contains('|'))
                {
                    string[] photo = photos.Split('|');
                    foreach (string onephoto in photo)
                    {
                        string[] filename = onephoto.Split('/');
                        string newfilename = filename[filename.Length - 1];
                        if (File.Exists(HostingEnvironment.MapPath(onephoto)))
                        {
                            if(File.Exists(Path.Combine(HostingEnvironment.MapPath(newpath), order_id + "_" + newfilename)))
                            {
                                File.Delete(Path.Combine(HostingEnvironment.MapPath(newpath), order_id + "_" + newfilename));
                            }
                            File.Copy(Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename), Path.Combine(HostingEnvironment.MapPath(newpath), order_id + "_" + newfilename));
                            File.Delete(Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename));
                        }
                    }
                }
                else
                {
                    if (File.Exists(HostingEnvironment.MapPath(photos)))
                    {
                        string[] filename = photos.Split('/');
                        string newfilename = filename[filename.Length - 1];
                        if (File.Exists(Path.Combine(HostingEnvironment.MapPath(newpath), order_id + "_" + newfilename)))
                        {
                            File.Delete(Path.Combine(HostingEnvironment.MapPath(newpath), order_id + "_" + newfilename));
                        }
                        File.Copy(Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename), Path.Combine(HostingEnvironment.MapPath(newpath), order_id + "_" + newfilename));
                        File.Delete(Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename));
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok();
        }

        [Route("api/others/removeImage/{order_id}")]
        [HttpPut]
        public IHttpActionResult removeFile(string order_id, string photos, string product_id)
        {
        http://localhost:50704/API/others/moveImage/RQYKE?photos=/Content/Photos/1.jpg&product_id=mg
            try
            {
                string path = "";
                string newpath = "";

                if (product_id == "mg")
                {
                    path = "~/Content/OrderPhotos/Magnet/";
                    newpath = "~/Content/OrderPhotos/Magnet/Completed_Magnet/";
                }
                else if (product_id == "kc")
                {
                    path = "~/Content/OrderPhotos/Keychain/";
                    newpath = "~/Content/OrderPhotos/Keychain/Completed_Keychain/";
                }

                if (photos.Contains('|'))
                {
                    string[] photo = photos.Split('|');
                    foreach (string onephoto in photo)
                    {
                        string[] filename = onephoto.Split('/');
                        string newfilename = filename[filename.Length - 1];
                        if (File.Exists(HostingEnvironment.MapPath(onephoto)))
                        {
                            if (File.Exists(Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename)))
                            {
                                File.Delete(Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename));
                            }
                            File.Copy(Path.Combine(HostingEnvironment.MapPath(newpath), order_id + "_" + newfilename), Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename));
                            File.Delete(Path.Combine(HostingEnvironment.MapPath(newpath), order_id + "_" + newfilename));
                        }
                    }
                }
                else
                {
                    if (File.Exists(HostingEnvironment.MapPath(photos)))
                    {
                        string[] filename = photos.Split('/');
                        string newfilename = filename[filename.Length - 1];
                        if (File.Exists(Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename)))
                        {
                            File.Delete(Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename));
                        }
                        File.Copy(Path.Combine(HostingEnvironment.MapPath(newpath), order_id + "_" + newfilename), Path.Combine(HostingEnvironment.MapPath(path), order_id + "_" + newfilename));
                        File.Delete(Path.Combine(HostingEnvironment.MapPath(newpath), order_id + "_" + newfilename));
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return Ok();
        }
        //[Route("api/others/completeOrder/{id}")]
        //[HttpPut]
        //public IHttpActionResult completeOrder(string id, string status)
        //{
        //    try
        //    {
        //        var lineitems = database.lineitems.SingleOrDefault(y => y.lineitem_id == id);
        //        lineitems.status = status;
        //        var itemphotoes = database.itemphotoes.Where(y => y.lineitem_id == id).ToList();
        //        foreach (var itemphoto in itemphotoes)
        //        {
        //            itemphoto.printing_status = status;
        //        }
        //        database.SaveChanges();
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.ToString());
        //    }
        //}
    }
}

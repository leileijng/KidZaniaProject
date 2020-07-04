using KidZaniaPhotoPrintingAdminPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
                        status = y.status
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
                        //itemphotoes = y.itemphotoes.Select(z => new
                        //{
                        //    lineitem_id = z.lineitem_id,
                        //}).Where(z => z.lineitem_id == y.lineitem_id).ToList(),
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

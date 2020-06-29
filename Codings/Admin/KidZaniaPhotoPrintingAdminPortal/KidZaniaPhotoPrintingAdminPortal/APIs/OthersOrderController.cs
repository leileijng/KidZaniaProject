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
                        product_id = y.product_id
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
                var order = database.lineitems.Select(x => new
                {
                    order_id = x.order.order_id,
                    product_id = x.product_id,
                    status = x.status,
                    item_amount = x.item_amount,
                    p_id = x.p_id,
                    photos = x.photos
                }
                ).Where(x => x.order_id == id).ToList();
                return Ok(order);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}

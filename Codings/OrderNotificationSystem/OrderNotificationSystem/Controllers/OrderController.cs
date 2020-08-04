using OrderNotificationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrderNotificationSystem.Controllers
{
    public class OrderController : ApiController
    {
        private Database database = new Database();

        [HttpGet]
        [Route("api/order/ReadyOrders")]
        public IHttpActionResult getReadyOrders()
        {
            try
            {
                var orders = database.orders.Select(x => new
                {
                    orderid = x.order_id,
                    status = x.status
                }).Where(x => x.status == "Ready").ToList();
                return Ok(orders);
            }
            catch (Exception e)
            {
                return BadRequest("Fail to retrieve ready orders!");
            }
        }

        [HttpGet]
        [Route("api/order/WaitingOrders")]
        public IHttpActionResult getWaitingOrders()
        {
            try
            {
                var orders = database.orders.Select(x => new
                {
                    orderid = x.order_id,
                    status = x.status
                }).Where(x => x.status == "Waiting").ToList();
                return Ok(orders);
            }
            catch (Exception e)
            {
                return BadRequest("Fail to retrieve waiting orders!");
            }
        }
    }
}

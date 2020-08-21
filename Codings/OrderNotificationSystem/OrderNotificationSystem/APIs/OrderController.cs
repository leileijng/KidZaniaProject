using OrderNotificationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrderNotificationSystem.APIs
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
                }).Where(x => x.status == "Preparing" || x.status == "Waiting").ToList();
                return Ok(orders);
            }
            catch (Exception e)
            {
                return BadRequest("Fail to retrieve printing orders!");
            }
        }

        [HttpPut]
        [Route("api/order/CompleteOrder")]
        public IHttpActionResult CompleteOrder(string orderId)
        {
            try
            {
                var order = database.orders.SingleOrDefault(x => x.order_id == orderId);
                order.status = "Completed";
                database.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("Fail to update order status!");
            }
        }
    }
}

using KidZaniaPhotoPrintingAdminPortal.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KidZaniaPhotoPrintingAdminPortal.APIs
{
    public class DashboardController : ApiController
    {
        private Database database = new Database();

        [HttpGet]
        [Route("api/dashboard/retrieveRealTimeData")]
        public IHttpActionResult retrieveRealTimeData()
        {
            DateTime today = DateTime.Now;
            DateTime yesterday = today.AddDays(-1);
            List<order> todayOrders = database.orders.Where(i => i.updatedAt.Day == today.Day && i.updatedAt.Month == today.Month && i.updatedAt.Year == today.Year).ToList();
            List<order> yesterdayOrders = database.orders.Where(i => i.updatedAt.Day == yesterday.Day && i.updatedAt.Month == yesterday.Month && i.updatedAt.Year == yesterday.Year).ToList();
            
            //1
            int todayOrderNumber = todayOrders.Count;
            int yesterdayOrderNumber = yesterdayOrders.Count;
            Debug.WriteLine("yesterday order is .." + yesterdayOrderNumber);
            Debug.WriteLine("today order is .." + todayOrderNumber);

            //2
            decimal todayRevenueNumber = 0;
            decimal yesterdayRevenueNumber = 0;

            foreach(order o in todayOrders)
            {
                todayRevenueNumber += o.total_amount;
            }
            foreach (order yo in yesterdayOrders)
            {
                yesterdayRevenueNumber += yo.total_amount;
            }



            //3
            int todayPhotoNumber = 0;
            int yesterdayPhotoNumber = 0;

            foreach (order o in todayOrders)
            {
                List<lineitem> photoLineItems = o.lineitems.Where(i => i.product.photo_product == true).ToList();
                foreach(lineitem pl in photoLineItems)
                {
                    todayPhotoNumber += pl.quantity;
                }
            }
            foreach (order o in yesterdayOrders)
            {
                List<lineitem> photoLineItems = o.lineitems.Where(i => i.product.photo_product == true).ToList();
                foreach (lineitem pl in photoLineItems)
                {
                    yesterdayPhotoNumber += pl.quantity;
                }
            }

            //4
            int waitingOrders = 0;
            foreach (order o in todayOrders)
            { 
                if(o.status == "Waiting" || o.status == "Printing")
                {
                    waitingOrders++;
                }
            }


            decimal rateOrder = 1;
            decimal rateRevenue = 1;
            decimal ratePhoto = 1;
            if (yesterdayOrderNumber != 0 && yesterdayPhotoNumber!=0)
            {
                rateOrder = Math.Round(((decimal)todayOrderNumber - (decimal)yesterdayOrderNumber) / (decimal)yesterdayOrderNumber, 2);
                rateRevenue = Math.Round(((decimal)todayRevenueNumber - (decimal)yesterdayRevenueNumber) / (decimal)yesterdayRevenueNumber, 2);
            }
            if (yesterdayPhotoNumber != 0)
            {
                ratePhoto = Math.Round(((decimal)todayPhotoNumber - (decimal)yesterdayPhotoNumber) / (decimal)yesterdayPhotoNumber, 2);
            }
            return Ok(new {
                todayOrderNumber,
                rateOrder,
                todayRevenueNumber,
                rateRevenue,
                todayPhotoNumber,
                ratePhoto,
                waitingOrders
            });
        }

        [HttpGet]
        [Route("api/dashboard/retrivePieChartData")]
        public IHttpActionResult retrivePieChartData()
        {
            DateTime today = DateTime.Now;
            List<lineitem> todayLineItems = database.lineitems.Where(i => i.updatedAt.Day == today.Day && i.updatedAt.Month == today.Month && i.updatedAt.Year == today.Year && i.status != "Unpaid").ToList();
            List<product> products = database.products.Where(i => i.visibility == true).ToList();
            List<SalesProduct> salesPro = new List<SalesProduct>();
            foreach (product p in products)
            {
                SalesProduct s = new SalesProduct();
                s.product_name = p.name;
                s.amount = 0;
                salesPro.Add(s);
            }

            foreach (lineitem l in todayLineItems)
            {
                for(int i = 0; i < salesPro.Count; i++)
                {
                    if (l.product.name.Equals(salesPro[i].product_name))
                    {
                        salesPro[i].amount += l.item_amount;
                    }
                }
            }
            return Ok(salesPro);
        }

        public class SalesProduct
        {
            public string product_name { get; set; }
            public decimal amount { get; set; }
        }
    }
}

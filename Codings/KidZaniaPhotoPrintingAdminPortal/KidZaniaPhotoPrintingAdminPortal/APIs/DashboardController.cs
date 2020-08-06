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

        [HttpGet]
        [Route("api/dashboard/retriveLineChartData")]
        public IHttpActionResult retriveLineChartData()
        {
            DateTime today = DateTime.Now;
            List<order> todayOrders = database.orders.Where(i => i.updatedAt.Day == today.Day && i.updatedAt.Month == today.Month && i.updatedAt.Year == today.Year).OrderBy(i => i.updatedAt).ToList();
            
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 0, 0);
            //DateTime end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
            DateTime end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 0, 0);



            if (DateTime.Compare(start, end) > 0)
            {
                //return yesterday's chart
                return BadRequest();
            }
            else
            {
                List<SalesFequency> salesFequencies = new List<SalesFequency>();
                for (int i = 0; i <= end.Hour - start.Hour; i++)
                {
                    SalesFequency sf = new SalesFequency();
                    sf.TimeSpan = (start.Hour + i).ToString() + ":00 - " + (start.Hour + i + 1).ToString() + ":00";
                    sf.numberOfOrders = 0;
                    foreach (order o in todayOrders)
                    {
                        if (o.updatedAt.Hour == start.Hour + i)
                        {
                            sf.numberOfOrders++;
                        }
                    }
                    salesFequencies.Add(sf);
                }

                return Ok(salesFequencies);
            }
        }
        public class SalesFequency
        {
            public string TimeSpan { get; set; }
            public int numberOfOrders { get; set; }
        }
    }
}

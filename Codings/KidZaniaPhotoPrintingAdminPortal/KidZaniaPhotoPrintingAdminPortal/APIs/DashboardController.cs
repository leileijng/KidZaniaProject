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


            decimal rateOrder = -1;
            decimal rateRevenue = -1;
            decimal ratePhoto = -1;
            if (yesterdayOrderNumber != 0 && yesterdayPhotoNumber!=0)
            {
                rateOrder = Math.Round(((decimal)todayOrderNumber - (decimal)yesterdayOrderNumber) / (decimal)yesterdayOrderNumber, 4);
                rateRevenue = Math.Round(((decimal)todayRevenueNumber - (decimal)yesterdayRevenueNumber) / (decimal)yesterdayRevenueNumber, 4);
            }
            if (yesterdayPhotoNumber != 0)
            {
                ratePhoto = Math.Round(((decimal)todayPhotoNumber - (decimal)yesterdayPhotoNumber) / (decimal)yesterdayPhotoNumber, 4);
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

        [HttpGet]
        [Route("api/dashboard/retriveYesterdayPieChartData")]
        public IHttpActionResult retriveYesterdayPieChartData()
        {
            DateTime yesterday = DateTime.Now.AddDays(-1);
            List<lineitem> todayLineItems = database.lineitems.Where(i => i.updatedAt.Day == yesterday.Day && i.updatedAt.Month == yesterday.Month && i.updatedAt.Year == yesterday.Year && i.status != "Unpaid").ToList();
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
                for (int i = 0; i < salesPro.Count; i++)
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
            DateTime yesterday = today.AddDays(-1);
            List<order> todayOrders = database.orders.Where(i => i.updatedAt.Day == today.Day && i.updatedAt.Month == today.Month && i.updatedAt.Year == today.Year).OrderBy(i => i.updatedAt).ToList();
            List<order> yesterdayOrders = database.orders.Where(i => i.updatedAt.Day == yesterday.Day && i.updatedAt.Month == yesterday.Month && i.updatedAt.Year == yesterday.Year).OrderBy(i => i.updatedAt).ToList();
            
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 0, 0);
            DateTime end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
            end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0);

            if (end.Hour > 18)
            {
                end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0);
            }

            bool isTodayOperating = true;
            if (DateTime.Compare(start, end) > 0)
            {
                isTodayOperating = false;
                end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0);
            }
                List<SalesFequency> salesFequencies = new List<SalesFequency>();
                for (int i = 0; i <= end.Hour - start.Hour; i++)
                {
                    SalesFequency sf = new SalesFequency();
                    sf.TimeSpan = (start.Hour + i).ToString() + ":00-" + (start.Hour + i + 1).ToString() + ":00";

                    sf.yesterdayNumberOfOrders = 0;
                    foreach (order yo in yesterdayOrders)
                    {
                        if (yo.updatedAt.Hour == start.Hour + i)
                        {
                            sf.yesterdayNumberOfOrders++;
                        }
                    }
                    if (isTodayOperating && todayOrders.Count > 0)
                    {
                        sf.todayNumberOfOrders = 0;
                        foreach (order o in todayOrders)
                        {
                            if (o.updatedAt.Hour == start.Hour + i)
                            {
                                sf.todayNumberOfOrders++;
                            }
                        }
                    }
                else
                {
                    sf.todayNumberOfOrders = -1;
                }
                    salesFequencies.Add(sf);


            }
            return Ok(salesFequencies);
        }
        public class SalesFequency
        {
            public string TimeSpan { get; set; }
            public int todayNumberOfOrders { get; set; }
            public int yesterdayNumberOfOrders { get; set; }
        }


        [HttpGet]
        [Route("api/dashboard/retriveNewlyUpdatedOrders")]
        public IHttpActionResult retriveNewlyUpdatedOrders()
        {
            List<order> selectedOrders = database.orders.OrderByDescending(i => i.updatedAt).ToList();
            if(selectedOrders == null || selectedOrders.Count == 0)
            {
                return Ok("No Order has been found");
            }
            else if(selectedOrders.Count > 10) {
                selectedOrders.RemoveRange(10, selectedOrders.Count - 10);
            }
            
            List<OrderDetails> orders = new List<OrderDetails>();
            foreach(order o in selectedOrders)
            {
                OrderDetails od = new OrderDetails();
                od.OrderCode = o.order_id;
                od.Products = "";
                od.NumberOfPhotos = 0;
                foreach (lineitem li in o.lineitems)
                {
                    od.Products += li.product.product_id + ", ";
                    if (li.product.photo_product)
                    {
                        od.NumberOfPhotos += li.quantity;
                    }
                }
                od.Products = od.Products.TrimEnd(' ');
                od.Products = od.Products.TrimEnd(',');
                od.OrderStatus = o.status;
                od.TotalAmount = o.total_amount;
                od.UpdatedTime = o.updatedAt.ToString("MM/dd/yyyy HH:mm:ss");
                orders.Add(od);
            }
            return Ok(orders);
        }

        public class OrderDetails
        {
            public string OrderCode { get; set; }
            public string Products { get; set; }
            public int NumberOfPhotos { get; set; }
            public string OrderStatus { get; set; }
            public decimal TotalAmount { get; set; }
            public string UpdatedTime { get; set; }
        }
    }
}

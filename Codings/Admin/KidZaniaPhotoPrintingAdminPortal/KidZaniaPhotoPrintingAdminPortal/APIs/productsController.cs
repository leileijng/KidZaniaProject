using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KidZaniaPhotoPrintingAdminPortal.Models;

namespace KidZaniaPhotoPrintingAdminPortal.APIs
{
    public class ProductsController : ApiController
    {
        private Database database = new Database();

        [HttpGet]
        public IHttpActionResult getProducts()
        {
            var prod = database.products.Select(x => new
            {
                product_id = x.product_id,
                name = x.name,
                image = x.image,
                original_price = x.original_price,
                original_GST = x.original_GST,
                photo_product = x.photo_product,
                visibility = x.visibility,
                quantity_constraint = x.quantity_constraint,
                description = x.description,
                pwp_price = x.pwp_price,
                pwp_GST = x.pwp_GST,
                updated_by = x.staff.name,
                updated_at = x.updated_at
            }
            ).ToList();
            

            //if (rateListQueryResults.Count() != 0)
            //{
            //    foreach (var oneRate in rateListQueryResults)
            //    {
            //        rateList.Add(new
            //        {
            //            customerAccount = oneRate.CustomerAccount.AccountName,
            //            accountRateId = oneRate.AccountRateId,
            //            rate = oneRate.RatePerHour,
            //            effectiveStartDate = oneRate.EffectiveStartDate,
            //            effectiveEndDate = oneRate.EffectiveEndDate,
            //        });
            //    }
            //}
            //else
            //{
            //    var customerListQueryResults = Database.CustomerAccounts.First(rate => rate.CustomerAccountId == id);
            //    rateList.Add(new
            //    {
            //        customerAccount = customerListQueryResults.AccountName,
            //        rate = "undefined"
            //    });
            //}
            return Ok(prod);
        }
    }
}

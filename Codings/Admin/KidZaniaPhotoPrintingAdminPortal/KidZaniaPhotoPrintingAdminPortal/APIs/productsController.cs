using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KidZaniaPhotoPrintingAdminPortal.Models;
using Newtonsoft.Json.Linq;

namespace KidZaniaPhotoPrintingAdminPortal.APIs
{
    public class ProductsController : ApiController
    {
        private Database database = new Database();

        [HttpGet]
        public IHttpActionResult getProducts()
        {
            if (database.products != null)
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
                return Ok(prod);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("api/products/{id}")]
        [HttpGet]
        public IHttpActionResult getProductsbyId(string id)
        {
            if (database.products != null)
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
                ).FirstOrDefault(x => x.product_id == id);
                return Ok(prod);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("api/products/{id}")]
        [HttpPut]
        public IHttpActionResult editProduct(string id, [FromBody]JObject data)
        {
            var prod = database.products.Single(x => x.product_id == id);
            //var name = data[0];
            var name = data["name"].ToString();
            var image = data["image"].ToString();
            var original_price = Decimal.Parse(data["original_price"].ToString());
            var pwp_price = Decimal.Parse(data["pwp_price"].ToString());
            var description = data["description"].ToString();
            var quantity_constraint = data["quantity_constraint"].ToString();
            var visibility = bool.Parse(data["visibility"].ToString());
            var photo_product = bool.Parse(data["photo_product"].ToString());
            prod.name = name;
            prod.image = image;
            prod.original_price = original_price;
            prod.pwp_price = pwp_price;
            prod.description = description;
            if (quantity_constraint == "--None--")
            {
                prod.quantity_constraint = null;
            }
            else
            {
                prod.quantity_constraint = quantity_constraint;
            }
            prod.visibility = visibility;
            prod.photo_product = photo_product;
            database.SaveChanges();
            return Ok(new { message = name + " details was successfully updated!" });
        }
        [Route("api/products/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                product foundProd = database.products.SingleOrDefault(p => p.product_id == id);

                if (foundProd != null)
                {
                    database.products.Remove(foundProd);
                    database.SaveChanges();
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, "Product not found.");
                }
                return Ok(new { message = "Product deleted" });
            }
            catch (Exception exceptionObject)
            {
                return BadRequest("Unable to delete product");
            }
        }
    }
}

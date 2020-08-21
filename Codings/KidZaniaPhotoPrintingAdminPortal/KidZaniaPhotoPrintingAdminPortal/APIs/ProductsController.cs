using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
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
            try
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
            catch (Exception e)
            {
                return BadRequest(e.InnerException.InnerException.Message);
            }
        }

        [Route("api/products/{id}")]
        [HttpGet]
        public IHttpActionResult getProductsbyId(string id)
        {
            try
            {
                var prod = database.products.Select(x => new
                {
                    product_id = x.product_id,
                    name = x.name,
                    image = x.image,
                    original_price = x.original_price,
                    original_GST = x.original_GST,
                    pwp_GST = x.pwp_GST,
                    photo_product = x.photo_product,
                    visibility = x.visibility,
                    quantity_constraint = x.quantity_constraint,
                    description = x.description,
                    pwp_price = x.pwp_price,
                    updated_by = x.staff.name,
                    updated_at = x.updated_at
                }
                ).FirstOrDefault(x => x.product_id == id);
                return Ok(prod);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.InnerException.Message);
            }
        }

        [Route("api/products/{id}")]
        [HttpPut]
        public IHttpActionResult editProduct(string id, [FromBody]JObject data)
        {
            try
            {
                var prod = database.products.Single(x => x.product_id == id);
                var quantity_constraint = data["quantity_constraint"].ToString();
                var gst = Decimal.Parse(data["gst"].ToString());
                if (data["pwp_price"].ToString() != "")
                {
                    var pwp_price = Decimal.Parse(data["pwp_price"].ToString());
                    prod.pwp_price = pwp_price;
                    prod.pwp_GST = pwp_price * gst / 100;
                }
                else
                {
                    prod.pwp_price = null;
                    prod.pwp_GST = null;
                }
                prod.name = data["name"].ToString();
                prod.original_price = Decimal.Parse(data["original_price"].ToString());
                prod.description = data["description"].ToString();
                prod.original_GST = Decimal.Parse(data["original_price"].ToString()) * gst / 100;
                prod.updated_at = DateTime.Now;
                prod.updated_by = data["staffid"].ToString();
                if (quantity_constraint == "--None--")
                {
                    prod.quantity_constraint = null;
                }
                else
                {
                    prod.quantity_constraint = quantity_constraint;
                }
                prod.visibility = bool.Parse(data["visibility"].ToString());
                prod.photo_product = bool.Parse(data["photo_product"].ToString());
                database.SaveChanges();
                return Ok(new { message = data["name"].ToString() + " details was successfully updated!" });
            }
            catch (Exception e)
            {
                return BadRequest("Item details failed to update! " + e.InnerException.InnerException.Message);
            }
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
                    return Content(HttpStatusCode.NotFound, "Product not found!");
                }
                return Ok(new { message = "Product deleted!" });
            }
            catch (Exception e)
            {
                return BadRequest("Unable to delete product! " + e.InnerException.InnerException.Message);
            }
        }

        [Route("api/products/editGST")]
        [HttpPut]
        public IHttpActionResult editGST(decimal gst)
        {
            try
            {
                List<product> prod = database.products.ToList();
                foreach (product oneprod in prod)
                {
                    oneprod.original_GST = oneprod.original_price * gst / 100;
                    if (oneprod.pwp_price != null)
                    {
                        oneprod.pwp_GST = oneprod.pwp_price * gst / 100;
                    }
                }
                database.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok();
        }

        [Route("api/products/{id}")]
        [HttpPost]
        public IHttpActionResult createProduct(string id, [FromBody]JObject data)
        {
            try
            {
                product prod = new product();
                var quantity_constraint = data["quantity_constraint"].ToString();
                var gst = Decimal.Parse(data["gst"].ToString());
                if (data["pwp_price"].ToString() != "")
                {
                    var pwp_price = Decimal.Parse(data["pwp_price"].ToString());
                    prod.pwp_price = pwp_price;
                    prod.pwp_GST = pwp_price * gst / 100;
                }
                prod.name = data["name"].ToString();
                prod.image = "/Content/ProductPhoto/" + id + data["image"].ToString().Substring(data["image"].ToString().IndexOf('.'));
                prod.original_price = Decimal.Parse(data["original_price"].ToString());
                prod.description = data["description"].ToString();
                prod.original_GST = Decimal.Parse(data["original_price"].ToString()) * gst / 100;
                prod.updated_at = DateTime.Now;
                prod.product_id = id;
                if (quantity_constraint == "--None--")
                {
                    prod.quantity_constraint = null;
                }
                else
                {
                    prod.quantity_constraint = quantity_constraint;
                }
                prod.visibility = bool.Parse(data["visibility"].ToString()); 
                prod.photo_product = bool.Parse(data["photo_product"].ToString());
                prod.updated_by = data["staffid"].ToString() ;
                database.products.Add(prod);
                database.SaveChanges();
                return Ok(new { message = data["name"].ToString() + " record has been created!" });
            }
            catch (Exception e)
            {
                return BadRequest("Unable to create product!" + e.InnerException.InnerException.Message);
            }
        }

        [Route("api/products/UploadFile/{id}")]
        [HttpPost]
        public IHttpActionResult UploadFile(string id)
        {
            var httpContext = HttpContext.Current;
            try
            {
                // Check for any uploaded file  
                if (httpContext.Request.Files.Count > 0)
                {
                    //Loop through uploaded files  
                    for (int i = 0; i < httpContext.Request.Files.Count; i++)
                    {
                        HttpPostedFile httpPostedFile = httpContext.Request.Files[i];
                        if (httpPostedFile != null)
                        {
                            // Construct file save path  
                            var ext = Path.GetExtension(httpPostedFile.FileName);
                            var fileSavePath = Path.Combine(HostingEnvironment.MapPath("~/Content/ProductPhoto/"), id + ext);
                            if (File.Exists(fileSavePath))
                            {
                                File.Delete(fileSavePath);
                            }
                            // Save the uploaded file  
                            httpPostedFile.SaveAs(fileSavePath);
                        }
                    }
                }
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }

        [Route("api/products/removefile")]
        [HttpDelete]
        public IHttpActionResult deleteFile(string image)
        {
            try
            {
                var fileSavePath = Path.Combine(HostingEnvironment.MapPath("~/Content/ProductPhoto/"), image);

                if (File.Exists(fileSavePath))
                {
                    File.Delete(fileSavePath);
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}

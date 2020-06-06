using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
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
                    photo_product = x.photo_product,
                    visibility = x.visibility,
                    quantity_constraint = x.quantity_constraint,
                    description = x.description,
                    pwp_price = x.pwp_price,
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
            var gst = data["gst"].ToString();
            var intgst = int.Parse(gst.Substring(0, 1));
            prod.name = name;
            prod.image = image;
            prod.original_price = original_price;
            prod.pwp_price = pwp_price;
            prod.description = description;
            prod.original_GST = original_price * intgst / 100;
            prod.pwp_GST = pwp_price * intgst / 100;
            prod.updated_at = DateTime.Now;
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
        [HttpPost]
        [Route("api/products/UploadFile")]
        public Task<HttpResponseMessage> Post()
        {
            List<string> savedFilePath = new List<string>();
            // Check if the request contains multipart/form-data
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            //Get the path of folder where we want to upload all files.
            string rootPath = HttpContext.Current.Server.MapPath("~/Content/ProductPhoto");
            var provider = new MultipartFileStreamProvider(rootPath);
            // Read the form data.
            //If any error(Cancelled or any fault) occurred during file read , return internal server error
            var task = Request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<HttpResponseMessage>(t =>
                {
                    if (t.IsCanceled || t.IsFaulted)
                    {
                        Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                    }
                    foreach (MultipartFileData dataitem in provider.FileData)
                    {
                        try
                        {
                            //Replace / from file name
                            string name = dataitem.Headers.ContentDisposition.FileName.Replace("\"", "");
                            //Create New file name using GUID to prevent duplicate file name
                            string newFileName = Guid.NewGuid() + Path.GetExtension(name);
                            //Move file from current location to target folder.
                            File.Move(dataitem.LocalFileName, Path.Combine(rootPath, newFileName));


                        }
                        catch (Exception ex)
                        {
                            string message = ex.Message;
                        }
                    }

                    return Request.CreateResponse(HttpStatusCode.Created, savedFilePath);
                });
            return task;
        }
    }
}

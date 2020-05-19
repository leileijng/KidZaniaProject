using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CustomerPortal.Models;

namespace CustomerPortal.APIs
{
    public class ProductController : ApiController
    {
        DBModels Database = new DBModels();
        // GET api/<controller>
        public IQueryable<product> Get()
        {
            return Database.products;
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var prod = Database.products
                .SingleOrDefault(x => x.pro_id == id);
            if (prod == null)
            {
                string message = "Unable to retrieve product";
                return BadRequest(message);
            }
            else
            {
                var response = new
                {
                    pId = prod.pro_id,
                    pName = prod.pro_name,
                    pPrice = prod.pro_price,
                    pGst = prod.pro_gst,
                    pVisibility = prod.pro_visibility,
                    pImage = prod.pro_image,
                    pDesc = prod.pro_description,
                };
                //end of creation of the anonymous type response object
                return Ok(response);
            }
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]product product)
        {
            try
            {
                Database.products.Add(product);
                Database.SaveChanges();//Telling the database model to save the changes
                return Ok(new { message = "New product record created" });

            }
            catch (Exception exceptionObject)
            {
                if (exceptionObject.InnerException.Message
                .Contains("LessonType_LessonTypeName_UniqueConstraint") == true)
                {

                    //Return a bad http request message to the client
                    return BadRequest("Unable to add product");
                }//end of if block
                return BadRequest("Unable to add product");
            }
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, [FromBody]product product)
        {
            try
            {
                product foundProd = Database.products.SingleOrDefault(p => p.pro_id == id);

                if (foundProd != null)
                {
                    foundProd.pro_name = product.pro_name;
                    foundProd.pro_price = product.pro_price;
                    foundProd.pro_gst = product.pro_gst;
                    foundProd.pro_image = product.pro_image;
                    foundProd.pro_visibility = product.pro_visibility;
                    foundProd.pro_description = product.pro_description;

                    Database.SaveChanges();
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, "Product not found.");
                }
                return Ok(new { message = "Product updated" });
            }
            catch (Exception exceptionObject)
            {
                if (exceptionObject.InnerException.Message
                .Contains("LessonType_LessonTypeName_UniqueConstraint") == true)
                {

                    //Return a bad http request message to the client
                    return BadRequest("Unable to update product");
                }//end of if block
                return BadRequest("Unable to update product");
            }
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                product foundProd = Database.products.SingleOrDefault(p => p.pro_id == id);

                if (foundProd != null)
                {
                    Database.products.Remove(foundProd);
                    Database.SaveChanges();
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, "Product not found.");
                }
                return Ok(new { message = "Product deleted" });
            }
            catch (Exception exceptionObject)
            {
                if (exceptionObject.InnerException.Message
                .Contains("LessonType_LessonTypeName_UniqueConstraint") == true)
                {

                    //Return a bad http request message to the client
                    return BadRequest("Unable to delete product");
                }//end of if block
                return BadRequest("Unable to delete product");
            }
        }
    }
}
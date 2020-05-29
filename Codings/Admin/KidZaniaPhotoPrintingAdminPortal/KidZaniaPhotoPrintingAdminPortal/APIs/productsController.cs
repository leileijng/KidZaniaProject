using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using KidZaniaPhotoPrintingAdminPortal.Models;
using Database = KidZaniaPhotoPrintingAdminPortal.Models.Database;

namespace KidZaniaPhotoPrintingAdminPortal.APIs
{
    public class productsController : ApiController
    {
        private Database db = new Database();

        // GET: api/products
        public IQueryable<product> Getproducts()
        {
            return db.products;
        }

        // GET: api/products/5
        [ResponseType(typeof(product))]
        public IHttpActionResult Getproduct(string id)
        {
            product product = db.products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putproduct(string id, product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.product_id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!productExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/products
        [ResponseType(typeof(product))]
        public IHttpActionResult Postproduct(product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.products.Add(product);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (productExists(product.product_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = product.product_id }, product);
        }

        // DELETE: api/products/5
        [ResponseType(typeof(product))]
        public IHttpActionResult Deleteproduct(string id)
        {
            product product = db.products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool productExists(string id)
        {
            return db.products.Count(e => e.product_id == id) > 0;
        }
    }
}
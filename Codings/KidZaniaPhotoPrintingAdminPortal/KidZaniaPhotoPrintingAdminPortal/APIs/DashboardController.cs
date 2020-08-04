using KidZaniaPhotoPrintingAdminPortal.Models;
using System;
using System.Collections.Generic;
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
        [Route("api/dashboard/retrieveData")]
        public IHttpActionResult retrieveData()
        {
            return Ok();
        }
    }
}

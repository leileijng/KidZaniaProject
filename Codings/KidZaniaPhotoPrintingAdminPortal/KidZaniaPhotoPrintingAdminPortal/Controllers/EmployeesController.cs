using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KidZaniaPhotoPrintingAdminPortal.Controllers
{
    public class EmployeesController : ApiController
    {
        [Authorize]
        public IEnumerable<user> Get()
        {
            using (employeeEntities entities = new employeeEntities())
            {
                return entities.users.ToList();
            }

        }
    }
}

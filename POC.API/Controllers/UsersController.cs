using POC.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POC.API.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/users
        public IEnumerable<User> Get()
        {
            return UsersDB.GetUsers();
        }

        // GET api/users/?email=sujan@gmail.com
        public IHttpActionResult Get(string email)
        {
            User user = UsersDB.GetUserByEmail(email);
            if (user == null)
                return NotFound();
            else
                return Json(user);
        }
    }
}

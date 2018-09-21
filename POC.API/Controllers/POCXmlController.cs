using POC.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POC.API.Controllers
{
    public class POCXmlController : ApiController
    {
        // GET api/values
        public IEnumerable<POCXml> Get()
        {
            return POCConXmlDB.GetAll();
        }

        public POCXml Get(int id)
        {
            return POCConXmlDB.Get(1);
        }
    }
}

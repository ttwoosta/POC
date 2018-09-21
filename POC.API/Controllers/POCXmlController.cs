using POC.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace POC.API.Controllers
{
    public class POCXmlController : ApiController
    {
        // GET api/pocxml
        public IEnumerable<POCXml> Get()
        {
            return POCConXmlDB.GetAll();
        }

        // GET api/pocxml/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Json(POCConXmlDB.Get(id));
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message == "Sequence contains no elements")
                    return NotFound();

                throw ex;
            }
        }

        // POST api/pocxml
        public void Post([FromBody]string value)
        {
        }

        // PUT api/pocxml/5
        public void Put(int id, [FromBody]string value)
        {
            
        }
    }
}

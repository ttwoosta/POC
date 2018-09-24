using POC.API.Models;
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
            return POCLinqDB.Get();
        }

        // GET api/pocxml/5
        public IHttpActionResult Get(int id)
        {
            if (POCLinqDB.Count(id) == 0)
                return NotFound();
            else
                return Json(POCLinqDB.Get(id));
        }

        // POST api/pocxml
        public IHttpActionResult Post([FromBody]ConXmlModel model)
        {
            if (ModelState.IsValid)
            {
                POCXml xml = new POCXml()
                {
                    XmlTypeId = model.XmlTypeId,
                    XmlFile = model.XMLFile
                };
                POCLinqDB.Insert(xml);
                return Ok();
            }
            else
            {
                return BadRequest("Data is invalid");
            }
        }

        // PUT api/pocxml/5
        public IHttpActionResult Put(int id, [FromBody]ConXmlModel model)
        {
            if (POCLinqDB.Count(id) == 0)
                return BadRequest(
                    string.Format("Conversion Xml entry for id {0} doest not exist", id));

            if (ModelState.IsValid)
            {
                POCXml xml = new POCXml()
                {
                    Id = id,
                    XmlTypeId = model.XmlTypeId,
                    XmlFile = model.XMLFile
                };
                POCLinqDB.Update(xml);
                return Ok();
            }
            else
            {
                return BadRequest("Data is invalid");
            }
        }
    }
}

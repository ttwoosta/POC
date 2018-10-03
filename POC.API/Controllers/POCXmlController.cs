using POC.API.Models;
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
        // GET api/pocxml
        public IEnumerable<POCXml> Get()
        {
            return POCXmlDB.Get();
        }

        // GET api/pocxml/5
        public IHttpActionResult Get(int id)
        {
            if (POCXmlDB.Count(id) == 0)
                return NotFound();
            else
                return Json(POCXmlDB.Get(id));
        }

        // POST api/pocxml
        public IHttpActionResult Post([FromBody]ConXmlModel model)
        {
            if (ModelState.IsValid)
            {
                POCXml xml = new POCXml()
                {
                    Xml_TypeId = model.Xml_TypeId,
                    XmlFile = model.XmlFile,
                };
                POCXmlDB.Insert(xml);
                return Ok();
            }
            else
            {
                return BadRequest("Data is invalid");
            }
        }
        
        // PUT api/pocxml/5
        public IHttpActionResult Put([FromUri] Int32 id, [FromBody]ConXmlModel model)
        {
            if (POCXmlDB.Count(id) == 0)
                return BadRequest(
                    string.Format("Conversion Xml entry for id {0} doest not exist", id));

            if (ModelState.IsValid)
            {
                POCXml xml = new POCXml()
                {
                    Xml_Id = id,
                    Xml_TypeId = model.Xml_TypeId,
                    XmlFile = model.XmlFile
                };
                POCXmlDB.Update(xml);
                return Ok();
            }
            else
            {
                return BadRequest("Data is invalid");
            }
        }

      
    }
}

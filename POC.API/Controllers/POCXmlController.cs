using POC.API.Models;
using POC.DataModel;
using POC.DataModel.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace POC.API.Controllers
{
    public class POCXmlController : ApiController
    {
        private IXmlRepository _xmlRepo;

        public POCXmlController(IXmlRepository xmlRepository)
        {
            _xmlRepo = xmlRepository;
        }

        // GET api/pocxml
        public IEnumerable<POCXml> Get()
        {
            return _xmlRepo.xmls.OrderBy(x => x.Xml_Id);
        }

        // GET api/pocxml/5
        public IHttpActionResult Get(int id)
        {
            if (_xmlRepo.xmls.Count(x => x.Xml_Id == id) == 0)
                return NotFound();
            else
                return Json(_xmlRepo.xmls.Where(x => x.Xml_Id == id).First());
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

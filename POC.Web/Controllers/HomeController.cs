using POC_Web.Services;
using POC_Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace POC_Web.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        [HttpGet]
        public ActionResult EditXmlFileForm(int id)
        {
            HttpResponseMessage resp = GlobalVariables.WebApiClient.GetAsync("api/pocxml/"+id.ToString()).Result;
            ConversionXmlViewModel model = resp.Content.ReadAsAsync<ConversionXmlViewModel>().Result;
            return PartialView("_EditXmlFileForm", model);
        }
        
        public JsonResult XmlFileTree(int id)
        {
            XmlSerialization xs = new XmlSerialization();
            HttpResponseMessage resp = GlobalVariables.WebApiClient.GetAsync("api/pocxml/" + id.ToString()).Result;
            ConversionXmlViewModel model = resp.Content.ReadAsAsync<ConversionXmlViewModel>().Result;
             var xmlModel = xs.ProcessXml(model.XmlFile);

            List<XmlModel> list = new List<XmlModel>();
            foreach (var item in xmlModel)
            {
                XmlModel xmodel = new XmlModel();
                xmodel.NodeId = item.NodeId;
                xmodel.ParentId = item.ParentId;
                xmodel.NodeName = item.NodeName;
                xmodel.NodeValue = item.NodeValue;
                StringBuilder attrName = new StringBuilder();
                StringBuilder attrValue = new StringBuilder();
                for (int i = 0; i < item.Attributes.Count; i++)
                {
                    attrName.Append(item.Attributes[i].Name.ToString() + ";");
                    attrValue.Append(item.Attributes[i].Value.ToString() + ";");

                }
                xmodel.AttributeName = attrName.ToString();
                xmodel.AttributeValue = attrValue.ToString();
                list.Add(xmodel);
            }

            return Json(list, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult UpdateXMLFile(ConversionXmlViewModel xmlfile)
        {

            XmlSerialization xs = new XmlSerialization();
            XDocument xdoc = xs.SerializeObjectintoXml(xmlfile);
            HttpResponseMessage resp = GlobalVariables.WebApiClient.PutAsJsonAsync("api/pocxml/" + xmlfile.Xml_id.ToString(),xmlfile).Result;
            if(resp.IsSuccessStatusCode)
            {
                return Json("success");
            }
            else
            {
                return Json("fail");
            }
           

        }





    }



}
using POC_Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POC_Web.ViewModel
{
    public class ConversionXmlViewModel
    {
        public int Xml_id { get; set; }
        public int Xml_TypeId { get; set; }
        public string XmlFile { get; set; }

        public List<XmlModel> XmlModel { get; set; }
    }
}
using POC_Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace POC_Web.ViewModel
{
    public class ConversionXmlViewModel
    {
        public int Xml_id { get; set; }
        public int Xml_TypeId { get; set; }
        public string XmlFile { get; set; }
        public List<XmlModel> XmlModel { get; set; }

        public IEnumerable<Element> XmlElements { get; set; }
    }

    public class ElementCollection
    {

        public IEnumerable<Element> Element { get; set; }
    }
    public class Element
    {
        public string Name { get; set; }
        public bool hasText { get; set; }
    }

}
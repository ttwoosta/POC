using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace POC_Web.Services
{
    public class XmlModel
    {
        public string NodeName { get; set; }
        public int NodeId { get; set; }
        public int ParentId { get; set; }
        public string NodeValue { get; set; }

        public IList<XAttribute> Attributes { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
    }
}
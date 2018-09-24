using POC_Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace POC_Web.Services
{
    public class XmlSerialization
    {
        public List<XmlModel> ProcessXml(string xmlString)
        {
            XmlDocument doc = new XmlDocument();
            var xml =
                    XElement.Parse(xmlString);

            var elementsAndIndex =
                xml
                .DescendantsAndSelf()
                .Select((node, index) => new { index = index + 1, node })
                .ToList();

            List<XmlModel> elementsWithIndexAndParentIndex =
                elementsAndIndex
                .Select(
                    elementAndIndex =>
                        new
                        {
                            Element = elementAndIndex.node,
                            Index = elementAndIndex.index,
                            ParentElement = elementsAndIndex.SingleOrDefault(parent => parent.node == elementAndIndex.node.Parent)
                        })
                .Select(
                    elementAndIndexAndParent =>
                        new XmlModel
                        {
                            NodeName = elementAndIndexAndParent.Element.Name.LocalName,
                            NodeId = elementAndIndexAndParent.Index,
                            ParentId = elementAndIndexAndParent.ParentElement == null ? 0 : elementAndIndexAndParent.ParentElement.index,
                            NodeValue = elementAndIndexAndParent.Element.HasElements == true ? null : elementAndIndexAndParent.Element.Value,
                            Attributes = elementAndIndexAndParent.Element.Attributes().ToList()
                        })
                .ToList();
            List<XmlModel> list = new List<XmlModel>();
            foreach (var item in elementsWithIndexAndParentIndex)
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

            return list;
        }


        public XDocument SerializeObjectintoXml(ConversionXmlViewModel xmlFile)
        {
            var childList = xmlFile.XmlModel.Where(s => s.ParentId == 1).ToList();
            XDocument xdoc = new XDocument();
            XElement xe = new XElement(xmlFile.XmlModel[0].NodeName);
            GetAttributes(xmlFile.XmlModel[0], xe);
            for (int i = 0; i < childList.Count(); i++)
            {
                XElement newchild = new XElement(childList[i].NodeName, childList[i].NodeValue);
                GetAttributes(childList[i], newchild);
                AddChildRecursive(newchild, childList[i].NodeId, xmlFile);
                xe.Add(newchild);

            }
            xdoc.Add(xe);
            return xdoc;
        }

        public void AddChildRecursive(XElement element, int nodeId, ConversionXmlViewModel xmlFile)
        {
            var node = xmlFile.XmlModel;
            var childList = xmlFile.XmlModel.Where(s => s.ParentId == nodeId).ToList();
            foreach (var item in childList)
            {
                XElement newchild = new XElement(item.NodeName, item.NodeValue);
                GetAttributes(item, newchild);
                element.Add(newchild);
                AddChildRecursive(newchild, item.NodeId, xmlFile);
            }
        }

        public void GetAttributes(XmlModel model, XElement xe)
        {
            if (model.AttributeName != null && model.AttributeValue != null)
            {
                string[] AttrName = model.AttributeName.Split(';');
                string[] AttrValue = model.AttributeName.Split(';');
                int count = AttrName.Count() > AttrValue.Count() ? AttrName.Count() : AttrValue.Count();
                for (int i = 0; i < count; i++)
                {
                    if (AttrName[i] != "" && AttrValue[i] != "")
                    {
                        xe.SetAttributeValue(AttrName[i], AttrValue[i]);
                    }
                }
            }

        }


    }
}
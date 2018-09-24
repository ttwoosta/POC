
using System.Data.Linq.Mapping;
using System.Xml;
using System.Xml.Linq;

namespace POC.DataModel
{
    [Table(Name ="dbo.ConversionXml")]
    public class POCXml
    {
        [Column(Name = "Xml_Id", IsPrimaryKey = true, IsDbGenerated =true)]
        public int Id { get; set; }

        [Column(Name ="Xml_TypeId", UpdateCheck =UpdateCheck.WhenChanged)]
        public int XmlTypeId { get; set; }

        [Column(Name ="XmlFile", DbType = "NVarChar(MAX)", UpdateCheck = UpdateCheck.WhenChanged)]
        public string XmlFile { get; set; }

    }
}

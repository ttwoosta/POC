
using System.Data.Linq.Mapping;

namespace POC.DataModel
{
    [Table(Name ="dbo.ConversionXml")]
    public class POCXml
    {
        [Column(Name = "Xml_Id", IsPrimaryKey = true, IsDbGenerated =true)]
        public int Id { get; set; }

        [Column(Name ="Xml_TypeId")]
        public int XmlTypeId { get; set; }

        [Column(Name ="XmlFile")]
        public string XmlFile { get; set; }

    }
}

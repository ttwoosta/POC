
using System.Data.Linq.Mapping;
using System.Xml;
using System.Xml.Linq;

namespace POC.DataModel
{
    [Table(Name ="dbo.ConversionXml")]
    public class POCXml
    {
        [Column(IsPrimaryKey = true, IsDbGenerated =true)]
        public int Xml_Id { get; set; }

        [Column(UpdateCheck=UpdateCheck.WhenChanged)]
        public int Xml_TypeId { get; set; }

        [Column(UpdateCheck=UpdateCheck.WhenChanged)]
        public string XmlFile { get; set; }

    }
}

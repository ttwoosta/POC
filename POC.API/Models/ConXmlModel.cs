using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POC.API.Models
{
    public class ConXmlModel
    {

        [Required]
        [Display(Name = "XML Type Id")]
        public int XmlTypeId { get; set; }

        [Required]
        [DataType(DataType.Html)]
        [Display(Name = "XML File")]
        public string XMLFile { get; set; }

    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace XmlValidation.Models
{
    public class IndexViewModel
    {
        public string XmlSchema { get; set; }
        [Required]
        public string XmlDocument { get; set; }
        public bool XmlValidated { get; set; }
        public IList<string> SchemaErrors { get; set; } = new List<string>();
        public IList<string> XmlErrors { get; set; } = new List<string>();
        public bool SchemaValid => SchemaErrors.Count() == 0;
        public bool XmlValid => XmlErrors.Count() == 0;
    }
}

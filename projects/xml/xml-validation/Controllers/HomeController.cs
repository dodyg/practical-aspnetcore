using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Microsoft.AspNetCore.Mvc;
using XmlValidation.Models;

namespace XmlValidation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel
            {

                XmlSchema =
                @"<xsd:schema xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
	                <xsd:element name='Root'>
		                <xsd:complexType>
			                <xsd:sequence>
				                <xsd:element name='Child1' minOccurs='1' maxOccurs='1' />
				                <xsd:element name='Child2' minOccurs='1' maxOccurs='1' />
			                </xsd:sequence>
		                </xsd:complexType>
	                </xsd:element>
                </xsd:schema>",
                XmlDocument =
                @"<Root>
                    <Child1>content1</Child1>
                    <Child2>content2</Child2>
                  </Root>",
                XmlValidated = false
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Index([Bind("XmlSchema, XmlDocument")]IndexViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                viewModel.SchemaErrors = ValidateSchema(viewModel.XmlSchema);
                viewModel.XmlErrors = ValidateDocument(viewModel.XmlSchema, viewModel.XmlDocument, viewModel.SchemaValid);
                viewModel.XmlValidated = true;
            }
            return View(viewModel);
        }

        private IList<string> ValidateDocument(string xmlSchema, string xmlDoc, bool schemaValid)
        {
            IList<string> errors = new List<string>();
            try
            {
                
                var xDocument = XDocument.Parse(xmlDoc);
                if (!string.IsNullOrEmpty(xmlSchema) && schemaValid)
                {
                    var schemaSet = new XmlSchemaSet();
                    schemaSet.Add("",XmlReader.Create(new StringReader(xmlSchema)));
                    xDocument.Validate(schemaSet, (o, e) =>
                    {
                        errors.Add(e.Message);
                    });
                }
            }
            catch (Exception e)
            {
                errors.Add(e.Message);
            }
            return errors;
        }

        private IList<string> ValidateSchema(string xmlSchema)
        {
            IList<string> errors = new List<string>();
            if (!string.IsNullOrEmpty(xmlSchema))
            {
                try
                {
                    _ = XDocument.Parse(xmlSchema);
                }
                catch (Exception ex)
                {
                    errors.Add(ex.Message);
                }
            }
            return errors;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

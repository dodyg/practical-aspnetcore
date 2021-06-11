using System;
using System.Xml.Linq;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CustomEncryptor.Services
{
    public class CustomXmlEncryptor : IXmlEncryptor
    {
        private readonly ILogger _logger;

        public CustomXmlEncryptor(IServiceProvider services)
        {
            _logger = services.GetRequiredService<ILoggerFactory>().CreateLogger<CustomXmlEncryptor>();
        }

        public EncryptedXmlInfo Encrypt(XElement plaintextElement)
        {
            if (plaintextElement == null)
            {
                throw new ArgumentNullException(nameof(plaintextElement));
            }

            _logger.LogInformation("Not encrypting key");

            var newElement = new XElement("unencryptedKey",
                new XComment(" This key is not encrypted. "),
                new XElement(plaintextElement));
            var encryptedTextElement = new EncryptedXmlInfo(newElement, typeof(CustomXmlDecryptor));

            return encryptedTextElement;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;

namespace OutputFormatter
{
    public class RssOutputFormatter : TextOutputFormatter
    {
        public RssOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/rss+xml"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type)
        {
            if (typeof(SyndicationItem).IsAssignableFrom(type) || typeof(IEnumerable<SyndicationItem>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }

            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            IServiceProvider serviceProvider = context.HttpContext.RequestServices;
            var response = context.HttpContext.Response;

            var sw = new StringWriterWithEncoding(selectedEncoding);

            var list = new List<SyndicationItem>();

            if (context.Object is SyndicationItem)
            {
                list.Add(context.Object as SyndicationItem);
            }
            else if (context.Object is IEnumerable<SyndicationItem>)
            {
                list.AddRange(context.Object as IEnumerable<SyndicationItem>);
            }

            using (XmlWriter xmlWriter = XmlWriter.Create(sw, new XmlWriterSettings() { Async = true, Indent = true }))
            {
                var writer = new RssFeedWriter(xmlWriter);

                // Create item
                foreach (var i in list)
                    await writer.Write(i);

                xmlWriter.Flush();
            }

            await response.WriteAsync(sw.ToString(), selectedEncoding);
        }
    }

    class StringWriterWithEncoding : StringWriter
    {
        private readonly Encoding _encoding;

        public StringWriterWithEncoding(Encoding encoding)
        {
            this._encoding = encoding;
        }

        public override Encoding Encoding
        {
            get { return _encoding; }
        }
    }
}
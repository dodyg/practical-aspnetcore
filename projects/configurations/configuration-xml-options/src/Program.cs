using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IO;
using Microsoft.AspNetCore;

namespace Configuration.Xml.Options 
{
    public class XmlOptions
    {
        public XmlOptionsWebPages WebPages { get; set; }
        public class XmlOptionsWebPages
        {
            public string Value { get; set;}
        }

        public XmlOptionsConfig Config { get; set; }
        
        public class XmlOptionsConfig
        {
            public string Password { get; set; }
            public string Username { get; set; }

            public string Server { get; set; }

            public int Port { get; set; }
        }

        public string GoogleMap { get; set; }

        public XmlOptionsApp App { get; set; }

        public class XmlOptionsApp
        {
            public string Password { get; set; }

            public string User { get; set; }

            public XmlOptionsAppPriorities Priorities {get;set;}

            public class XmlOptionsAppPriorities
            {
                public int Task { get; set; }

                public int Limit { get; set; }
            }

            public XmlOptionsPrivacy Privacy {get; set;}

            public class XmlOptionsPrivacy
            {
                public XmlOptionsPrivacyKeys Individual { get; set;}

                public string Organization { get; set; }

                public class XmlOptionsPrivacyKeys
                {
                    public string SharedKey { get; set; }

                    public string PublicKey { get; set; }
                }
            }
        }
    }


    public class Startup
    {
        IConfigurationRoot _config;

        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
            //This is the most basic configuration you can have
            var builder = new ConfigurationBuilder();
            builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddXmlFile("settings.xml");

            _config = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<XmlOptions>(_config);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            app.Run(async (context) =>
            {
                var options = context.RequestServices.GetService<IOptions<XmlOptions>>().Value;
                
                var res = context.Response;

                await res.WriteAsync($"webpages:value : {options.WebPages.Value}\n");
                await res.WriteAsync($"config.password : {options.Config.Password}\n");
                await res.WriteAsync($"config.username : {options.Config.Username}\n");
                await res.WriteAsync($"config.server : {options.Config.Server}\n");
                await res.WriteAsync($"config.port : {options.Config.Port}\n");
                await res.WriteAsync($"config.googleMap : {options.GoogleMap}\n");
                await res.WriteAsync($"app:password : {options.App.Password} \n");
                await res.WriteAsync($"app:user : {options.App.User} \n");
                await res.WriteAsync($"app:priorities:task : {options.App.Priorities.Task} \n");
                await res.WriteAsync($"app:priorities:limit : {options.App.Priorities.Limit} \n");
                await res.WriteAsync($"app:privacy:individual:sharedKey : {options.App.Privacy.Individual.SharedKey}\n");
                await res.WriteAsync($"app:privacy:individual:publicKey : {options.App.Privacy.Individual.PublicKey}\n");
                await res.WriteAsync($"app:privacy:organization : {options.App.Privacy.Organization}\n");
            });
        }
    }
    
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseEnvironment("Development");
    }
}
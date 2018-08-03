using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore;

namespace FileProviderPhysical
{
    public class CustomDirectoryContents : IDirectoryContents
    {
        readonly IEnumerable<IFileInfo> _entries;

        public CustomDirectoryContents(IEnumerable<IFileInfo> files)
        {
            _entries = files;
        }

        public bool Exists => true;
        public IEnumerator<IFileInfo> GetEnumerator() => _entries.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _entries.GetEnumerator();
    }

    public class AlwaysTheSameFile : IFileInfo
    {
        public bool Exists => true;
        public long Length { get; }
        public string PhysicalPath => null;
        public string Name { get; }
        public DateTimeOffset LastModified { get; }
        public bool IsDirectory => false;
        public Stream CreateReadStream()
        {
            var text = @"
            Dhritarashtra said: O Sanjaya, after my sons and the sons of Pandu assembled in the place of pilgrimage at Kurukshetra, desiring to fight, what did they do?
            ";

            return new MemoryStream(Encoding.UTF8.GetBytes(text));
        }
    }

    //https://docs.microsoft.com/en-us/aspnet/core/api/microsoft.extensions.fileproviders.ifileinfo#Microsoft_Extensions_FileProviders_IFileInfo
    public class CustomDirectory : IFileInfo
    {
        public bool Exists => true;
        public long Length => -1; // read the offical doc        
        public string PhysicalPath => null; // read the offical doc
        public string Name { get; }
        public DateTimeOffset LastModified { get; }
        public bool IsDirectory => true;

        public Stream CreateReadStream()
        {
            throw new InvalidOperationException("Create Stream is not applicable for directory");
        }
    }

    public class CustomFileProvider : IFileProvider
    {
        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            var list = new List<AlwaysTheSameFile>
            {
                new AlwaysTheSameFile()    
            };

            var contents = new CustomDirectoryContents(list);
            return contents;
        }

        public IFileInfo GetFileInfo(string subpath) => new AlwaysTheSameFile();
        public IChangeToken Watch(string filter) => NullChangeToken.Singleton;
    }

    public class Startup
    {
        IHostingEnvironment _env;

        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _env.ContentRootFileProvider = new CustomFileProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            app.Run(async context =>
            {
                context.Response.Headers.Add("content-type", "text/html");

                using (var stream = _env.ContentRootFileProvider.GetFileInfo("").CreateReadStream())
                {
                    var reader = new StreamReader(stream);
                    await context.Response.WriteAsync(reader.ReadToEnd());
                }
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
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

    public class CustomFileInfo : IFileInfo
    {
        public bool Exists => true;

        public long Length { get; }

        public string PhysicalPath => null;

        public string Name { get; }

        public DateTimeOffset LastModified { get; }

        public bool IsDirectory => false;

        public Stream CreateReadStream()
        {
            throw new NotImplementedException();
        }
    }

    public class CustomDirectoryInfo : IFileInfo
    {
        public bool Exists => true;

        public long Length => -1;

        public string PhysicalPath => null;

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
            var list = new List<CustomFileInfo>();
            var contents = new CustomDirectoryContents(list);
            return contents;
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            return null;
        }

        public IChangeToken Watch(string filter)
        {
            return NullChangeToken.Singleton;
        }
    }

    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are two services available at constructor
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //This is the only service available at ConfigureServices
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            app.Run(async context =>
            {
                context.Response.Headers.Add("content-type", "text/html");

                await context.Response.WriteAsync("WIP");
            });
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
              .UseKestrel()
              .UseContentRoot(Directory.GetCurrentDirectory()) //If you remove this, ContentRootFileProvider will return something different. Try it out.
              .UseStartup<Startup>()
              .Build();

            host.Run();
        }
    }
}
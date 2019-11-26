using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using System.IO;
using Microsoft.AspNetCore;

namespace StartupBasic 
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are two services available at constructor
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are the three default services available at Configure
            var routerBuilder = new RouteBuilder(app);
       
            routerBuilder.MapGet("", async context =>
            {
               context.Response.Headers.Add("content-type", "text/html");

               var body = $@"
               <h1>Upload File</h1>
               <form action=""Upload"" method=""post"" enctype=""multipart/form-data"">
                    <input type=""text"" name=""name"" placeholder=""Name"" /><br/><br/>
                    <input type=""password"" name=""password"" placeholder=""Password"" /><br/><br/>
                    <textarea name=""description"" placeholder=""Description""></textarea><br/><br/>
                    <select name=""gender"">
                        <option value=""M"">Male</option>
                        <option value=""F"">Female</option>
                    </select><br/><br/>
                    <select name=""hobbies"" multiple=""multiple"">
                        <option>Writing Novel</option>
                        <option>Watching Old Movies</option>
                        <option>Swimming</option>
                    </select><br/><br/>
                    <input type=""checkbox"" name=""married"" value=""Yes""> Married? <br/><br/>
                    <input type=""checkbox"" name=""adventurous"" value=""Yes""> Adventurous? <br/><br/>
                    <input type=""hidden"" name=""secretHiddenValue"" value=""We are secretive"" />
                    <input type=""submit"" value=""Upload"" />
               </form>
               ";

               await context.Response.WriteAsync(body); 
            });

            routerBuilder.MapPost("Upload", async context =>
            {
                context.Response.Headers.Add("content-type", "text/html");

                if (context.Request.HasFormContentType)
                {
                    var form = await context.Request.ReadFormAsync();

                    foreach(var v in form.Keys)
                    {
                        await context.Response.WriteAsync($"{v} = {form[v]} <br/>");
                    }
                   
                }
                await context.Response.WriteAsync("");
            });

            app.UseRouter(routerBuilder.Build());
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16*1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
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
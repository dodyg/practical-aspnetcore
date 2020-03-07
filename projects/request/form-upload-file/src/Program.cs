using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.IO;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("", async context =>
                {
                    context.Response.Headers.Add("content-type", "text/html");

                    var body = $@"
                <h1>Upload File</h1>
                <form action=""Upload"" method=""post"" enctype=""multipart/form-data"">
                        <input type=""file"" name=""file"" />
                        <input type=""submit"" value=""Upload"" />
                </form>
                ";

                    await context.Response.WriteAsync(body);
                });

                endpoints.MapPost("Upload", async context =>
                {
                    if (context.Request.HasFormContentType)
                    {
                        var form = await context.Request.ReadFormAsync();

                        foreach (var f in form.Files)
                        {
                            using (var body = f.OpenReadStream())
                            {
                                var fileName = Path.Combine(env.ContentRootPath, f.FileName);
                                File.WriteAllBytes(fileName, ReadFully(body));
                                await context.Response.WriteAsync($"Uploaded file written to {fileName}");
                            }
                        }
                    }
                    await context.Response.WriteAsync("");
                });
            });
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using var ms = new MemoryStream();
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }
            return ms.ToArray();
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                );
    }
}
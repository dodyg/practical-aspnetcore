using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using System.IO;
using Microsoft.AspNetCore;
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

                endpoints.MapPost("Upload", async context =>
                {
                    context.Response.Headers.Add("content-type", "text/html");

                    if (context.Request.HasFormContentType)
                    {
                        var form = await context.Request.ReadFormAsync();

                        foreach (var v in form.Keys)
                        {
                            await context.Response.WriteAsync($"{v} = {form[v]} <br/>");
                        }

                    }
                    await context.Response.WriteAsync("");
                });
            });
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
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


}
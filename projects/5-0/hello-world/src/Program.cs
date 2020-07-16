using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

RequestDelegate write = ctx => ctx.Response.WriteAsync("Hello world C# 9");
Host.CreateDefaultBuilder().ConfigureWebHostDefaults(wb => wb.Configure(app => app.Run(write))).Build().Run();



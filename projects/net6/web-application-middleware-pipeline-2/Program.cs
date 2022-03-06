using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

var app = WebApplication.Create();

IEndpointRouteBuilder endpoints = app;
IApplicationBuilder pipeline = endpoints.CreateApplicationBuilder();
pipeline.Use((context, next) =>
{
    context.Items["message"] = "Hello World";
    return next(context);
})
.Run(async context =>
{
    await context.Response.WriteAsync("<html><body>" + 
            context.Items["message"] + 
            @"<br/><a href=""/about-us"">about us</a>" + 
            "</body></html>");
});

IApplicationBuilder pipeline2 = endpoints.CreateApplicationBuilder();
pipeline2.Use((context, next) =>
{
    context.Items["message"] = "About Us";
    return next(context);
})
.Run(async context =>
{
    await context.Response.WriteAsync(context.Items["message"].ToString());
});

app.Map("/", pipeline.Build());
app.Map("/about-us", pipeline2.Build());

app.Run();
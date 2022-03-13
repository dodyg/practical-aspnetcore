using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

var app = WebApplication.Create();

app.UseRouter(MapPipeline(app));
app.UseRouter(MapPipeline2(app));

IApplicationBuilder x = app;

app.Run();

IRouter MapPipeline(IApplicationBuilder applicationBuilder)
{
    var builder = new RouteBuilder(applicationBuilder);

    builder.MapMiddlewareGet("/", appBuilder => 
    {
        appBuilder.Use((context, next ) => 
        {
            context.Items["message"] = "hello world from items";
            return next(context);
        });

        appBuilder.Run(async context =>
        {
            await context.Response.WriteAsync("<html><body>" + 
                context.Items["message"] + 
                @"<br/><a href=""/about-us"">about us</a>" + 
                "</body></html>");
        });
    });

    return builder.Build();
}

IRouter MapPipeline2(IApplicationBuilder applicationBuilder)
{
    var builder = new RouteBuilder(applicationBuilder);

    builder.MapMiddlewareGet("/about-us", appBuilder => 
    {
        appBuilder.Run(async context =>
        {
            await context.Response.WriteAsync("About us");
        });
    });

    return builder.Build();
}
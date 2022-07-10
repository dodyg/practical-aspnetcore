using Microsoft.AspNetCore.Mvc.ActionConstraints;
using RouteSpy;
using static RouteSpy.RouteDebugger;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<RouteDebuggerMiddleware>();
builder.Services.AddMvc();

var app = builder.Build();

app.UseRouteDebugger();

app.MapGet("/", () => "Hello World!");

app.Run();



public static class RouteDebugger
{
    public static IApplicationBuilder UseRouteDebugger(this WebApplication app)
    {
        app.UseMiddleware<RouteDebuggerMiddleware>();
        return app;
    }

    public class RouteDebuggerMiddleware
    {
        private RequestDelegate Next { get; }

        public RouteDebuggerMiddleware(RequestDelegate next)
        {
            Next = next;
        }

        public async Task Invoke(HttpContext context, IActionDescriptorCollectionProvider )
        {
            // set header info
            context.Response.OnStarting(static state =>
            {
                if (state is HttpContext httpContext && httpContext.GetEndpoint() is { } endpoint)
                {
                    var info = GetEndpointInformation(endpoint, httpContext.GetRouteData());

                    var response = httpContext.Response;
                    var results = info.Select(kv => $"{kv.Key}:{kv.Value}; ");
                    response.Headers.Add("X-ASPNETCORE-ROUTE", string.Join("", results));
                }

                return Task.CompletedTask;
            }, context);

            await Next(context);
        }

        private static Dictionary<string, string?> GetEndpointInformation(Endpoint endpoint, RouteData? routeData)
        {
            var elements = new Dictionary<string, string?>
            {
                { "Name", endpoint?.DisplayName }
            };

            if (endpoint is RouteEndpoint route)
            {
                elements.Add("Pattern", route.RoutePattern.RawText);

                foreach (var metadata in route.Metadata)
                {
                    if (metadata is RouteNameMetadata name)
                    {
                        elements["Name"] = name.RouteName;
                    }

                    if (metadata is HttpMethodActionConstraint methods)
                    {
                        elements["Methods"] = string.Join(",", methods.HttpMethods);
                    }
                }
            }

            if (routeData is { } && routeData.Values.Any())
            {
                elements["RouteData"] =
                    string.Join(",", routeData.Values.Select(x => $"{x.Key}={x.Value}"));
            }

            return elements;
        }
    }
}
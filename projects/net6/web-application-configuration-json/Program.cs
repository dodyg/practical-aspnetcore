using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

var app = WebApplication.Create();

// WARNING: THIS IS A TERRIBLE IDEA IN REAL LIFE. DO not expose your configuration file over the wire this way!.
app.MapGet("/", (HttpContext context) =>
{
    var env = new List<ConfigInfo>();
    env.AddRange(app.Configuration.AsEnumerable().Select(x => new ConfigInfo(x.Key, x.Value)));
    return env;
});

await app.RunAsync();

record ConfigInfo(string Key, string Value);
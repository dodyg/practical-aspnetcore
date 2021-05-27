using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

var app = WebApplication.Create();
app.MapGet("/", async (HttpContext context) =>
{
    var env = new List<ConfigInfo>();
    env.AddRange(app.Configuration.AsEnumerable().Select(x => new ConfigInfo(x.Key, x.Value)));

    return env;
});

await app.RunAsync();

record ConfigInfo(string Key, string Value);
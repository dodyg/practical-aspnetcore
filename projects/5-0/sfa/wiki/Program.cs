using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Scriban;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<Wiki>();
var app = builder.Build();

app.MapGet("/", async context =>
{
  await context.Response.WriteAsync(BuildPage(title:"Welcome To Irtysh Wiki"));
});

app.MapGet("/{pageName}", async context =>
{
  var wiki = context.RequestServices.GetService<Wiki>();
  var pageName = context.Request.RouteValues["pageName"] as string ?? "";

  var (isFound, page) = wiki.LoadPage(pageName);

  if (isFound)
  {
    await context.Response.WriteAsync($"Found {page!.Name}");
  }
  else {
    await context.Response.WriteAsync(BuildPage(pageName));
  }
});

string BuildPage(string title)
{
  var head = Template.Parse("<title>{{ title }}</title>").Render(new { title });

  var body = Template.Parse("<h1>{{ page_name }}</h1>")
    .Render(new 
    { 
      PageName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title.Replace('-', ' ')) 
    });
    
  var page = @"
    <html>
      <head>
        {{ head }}
      </head>
      <body>
        {{ body }}
      </body>
    </html>
  ";

  var template = Template.Parse(page);
  return template.Render(new { head, body });
}

await app.RunAsync();

class Wiki 
{
  const string PageCollectionName = "Pages";

  readonly IWebHostEnvironment _env;
  
  public Wiki(IWebHostEnvironment env)
  {
      _env = env;
  }

  string GetDbPath() => Path.Combine(_env!.ContentRootPath, "wiki.db");

  public (bool isFound, Page? page) LoadPage(string path)
  {
    using var db = new LiteDatabase(GetDbPath());

    var coll = db.GetCollection<Page>(PageCollectionName);

    var p = coll.Query()
            .Where(x => x.Name.Equals(path, StringComparison.CurrentCultureIgnoreCase))
            .FirstOrDefault();

    if (p == null)
      return (false, null);

    return (true, p);
  }
}

public record Page 
{
  public string Name { get; set; }

  public string Content { get; set; }

  public DateTimeOffset LastModified { get; set; }
} 


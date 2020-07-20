using System;
using System.Globalization;
using System.IO;
using LiteDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Scriban;
using Microsoft.Extensions.DependencyInjection;
using HtmlBuilders;
using Microsoft.Extensions.Primitives;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Antiforgery;
using Markdig;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<Wiki>();
builder.Services.AddAntiforgery();
builder.Services.AddMemoryCache();

var app = builder.Build();

DateTimeOffset Timestamp() => DateTimeOffset.UtcNow;

app.MapGet("/", async context =>
{
  await context.Response.WriteAsync(BuildPage(title: "Welcome To Irtysh Wiki").ToString());
});

app.MapGet("/edit", async context =>
{
  var wiki = context.RequestServices.GetService<Wiki>()!;
  var antiForgery = context.RequestServices.GetService<IAntiforgery>()!;

  var pageName = context.Request.Query["pageName"];

  var (isFound, page) = wiki.LoadPage(pageName);
  if (!isFound)
  {
    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
    return;
  }

  await context.Response.WriteAsync(BuildPage(pageName,
    atHead: () => MarkdownEditorHead(),
    atBody: () =>
      new[]
      {
          BuildForm(new PageInput(page!.Id, pageName, page.Content), path: $"{pageName}", antiForgery: antiForgery.GetAndStoreTokens(context))
      },
    atSidePanel: () => AllPages(wiki),
    atFoot: () => MarkdownEditorFoot()).ToString());
});

string RenderMarkdown(string str)  => Markdown.ToHtml(str, new MarkdownPipelineBuilder().UseAdvancedExtensions().Build());

app.MapGet("/{pageName}", async context =>
{
  var wiki = context.RequestServices.GetService<Wiki>()!;
  var antiForgery = context.RequestServices.GetService<IAntiforgery>()!;

  var pageName = context.Request.RouteValues["pageName"] as string ?? "";

  var (isFound, page) = wiki.LoadPage(pageName);

  if (isFound)
  {
    await context.Response.WriteAsync(BuildPage(pageName, atBody: () =>
      new[]
      {
        RenderMarkdown(page!.Content),
        HtmlTags.A.Href($"/edit?pageName={pageName}").Append("Edit").ToHtmlString()
      },
      atSidePanel: () => AllPages(wiki)
    ).ToString());
  }
  else
  {
    await context.Response.WriteAsync(BuildPage(pageName,
    atHead: () => MarkdownEditorHead(),
    atBody: () =>
      new[]
      {
        BuildForm(new PageInput(null, pageName, string.Empty), path: pageName, antiForgery: antiForgery.GetAndStoreTokens(context))
      },
    atSidePanel: () => AllPages(wiki),
    atFoot: () => MarkdownEditorFoot()).ToString());
  }
});

app.MapPost("/{pageName}", async context =>
{
  var antiForgery = context.RequestServices.GetService<IAntiforgery>()!;
  await antiForgery.ValidateRequestAsync(context);

  var id = context.Request.Form["Id"];
  var name = context.Request.Form["Name"];
  var content = context.Request.Form["Content"];

  var properName = name.ToString().Replace(' ', '-').ToLower();

  var page = new Page
  {
    Name = name,
    Content = content,
    LastModified = Timestamp()
  };

  if (!StringValues.IsNullOrEmpty(id))
    page.Id = Convert.ToInt32(id);

  var wiki = context.RequestServices.GetService<Wiki>()!;
  var (isOK, p, ex) = wiki.SavePage(page);

  if (!isOK)
    Console.WriteLine($"Error {ex?.Message}");

  var pageName = context.Request.RouteValues["pageName"] as string ?? "";
  context.Response.Redirect($"/{p.Name}");
});

await app.RunAsync();

// End of the web part

IEnumerable<string> MarkdownEditorHead() => new[]
{
  @"<link rel=""stylesheet"" href=""https://unpkg.com/easymde/dist/easymde.min.css"">",
  @"<script src=""https://unpkg.com/easymde/dist/easymde.min.js""></script>"
};

IEnumerable<string> MarkdownEditorFoot() => new[]
{
  @"<script>
    var easyMDE = new EasyMDE({
      insertTexts: {
        link: [""["", ""]()""]
      }
    });
    </script>"
};

IEnumerable<string> AllPages(Wiki wiki) => new[]
{ 
  "<ul>",
  string.Join("", 
    wiki.ListAllPages().OrderBy(x => x.Name)
      .Select(x => HtmlTags.Li.Append(HtmlTags.A.Href(x.Name).Append(x.Name)).ToHtmlString()
    )
  ),
  "</ul>"
};

string BuildForm(PageInput input, string path, AntiforgeryTokenSet antiForgery)
{
  var antiForgeryField = HtmlTags.Input.Hidden.Name(antiForgery.FormFieldName).Value(antiForgery.RequestToken);

  var nameField = HtmlTags.Div.Class("field")
    .Append(HtmlTags.Label.Class("label").Append(nameof(input.Name)))
    .Append(HtmlTags.Div.Class("control")
      .Append(HtmlTags.Input.Text.Class("input").Name("Name").Value(input.Name))
    );

  var contentField = HtmlTags.Div.Class("field")
    .Append(HtmlTags.Label.Class("label").Append(nameof(input.Content)))
    .Append(HtmlTags.Div.Class("control")
      .Append(HtmlTags.Textarea.Name("Content").Class("textarea").Append(input.Content))
    );

  var submit = HtmlTags.Button.Class("button").Append("Submit");

  var form = HtmlTags.Form
             .Attribute("method", "post")
             .Attribute("action", $"/{path}")
               .Append(antiForgeryField)
               .Append(nameField)
               .Append(contentField);

  if (input.Id.HasValue)
  {
    HtmlTag id = HtmlTags.Input.Hidden.Name("Id").Value(input.Id.ToString());
    form = form.Append(id);
  }

  form = form.Append(submit);

  return form.ToHtmlString();
}

string KebabToNormalCase(string txt) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txt.Replace('-', ' '));

HtmlString BuildPage(string title, Func<IEnumerable<string>>? atHead = null, Func<IEnumerable<string>>? atBody = null, Func<IEnumerable<string>>? atSidePanel = null, Func<IEnumerable<string>>? atFoot = null)
{
  var head = Template.Parse(@"
    <meta charset=""utf-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
    <title>{{ title }}</title>
    <link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/bulma@0.9.0/css/bulma.min.css"">
    {{ header }}
  ").Render(new { title = title, header = string.Join("\r", atHead?.Invoke() ?? new[] { "" }) });

  var body = Template.Parse(@"
    {{ if at_side_panel != """" }}
    <div class=""columns"">
      <div class=""column is-four-fifths"">
        <div class=""container is-fluid"">
          <h1 class=""title is-1"">{{ page_name }}</h1>
          {{ content }}
        </div>
      </div>
      <div class=""column"">
        {{ at_side_panel }}
      </div>
    </div>
    {{ else }}
    <div class=""container is-fluid"">
      <h1 class=""title is-1"">{{ page_name }}</h1>
      {{ content }}
    </div>
    {{ end }}    
    {{ at_foot }}
    ")
    .Render(new
    {
      PageName = KebabToNormalCase(title),
      Content = string.Join("\r", atBody?.Invoke() ?? new[] { "" }),
      AtSidePanel = string.Join("\r", atSidePanel?.Invoke() ?? new[] { "" }),
      AtFoot = string.Join("\r", atFoot?.Invoke() ?? new[] { "" })
    });

  var page = @"
    <!DOCTYPE html>
      <head>
        {{ head }}
      </head>
      <body>
        {{ body }}
      </body>
    </html>
  ";

  var template = Template.Parse(page);
  return new HtmlString(template.Render(new { head, body }));
}

class Wiki
{
  const string PageCollectionName = "Pages";
  const string AllPagesKey = "AllPages";
  const double CacheAllPagesForMinutes = 30;

  readonly IWebHostEnvironment _env;
  readonly IMemoryCache _cache;

  public Wiki(IWebHostEnvironment env, IMemoryCache cache)
  {
    _env = env!;
    _cache = cache;
  }

  string GetDbPath() => Path.Combine(_env.ContentRootPath, "wiki.db");

  public List<Page> ListAllPages()
  {
    var pages = _cache.Get(AllPagesKey) as List<Page>;

    if (pages is object)
      return pages;

    using var db = new LiteDatabase(GetDbPath());
    var coll = db.GetCollection<Page>(PageCollectionName);
    var items = coll.Query().ToList();

    _cache.Set(items, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(CacheAllPagesForMinutes)));
    return items;
  }

  public (bool isFound, Page? page) LoadPage(string path)
  {
    using var db = new LiteDatabase(GetDbPath());
    var coll = db.GetCollection<Page>(PageCollectionName);
    coll.EnsureIndex(x => x.Name);

    var p = coll.Query()
            .Where(x => x.Name.Equals(path, StringComparison.OrdinalIgnoreCase))
            .FirstOrDefault();

    if (p is not object)
      return (false, null);

    return (true, p);
  }

  public (bool isOK, Page? page, Exception? ex) SavePage(Page page)
  {
    using var db = new LiteDatabase(GetDbPath());
    var coll = db.GetCollection<Page>(PageCollectionName);
    coll.EnsureIndex(x => x.Name);

    if (page.Id == default(int))
      coll.Insert(page);
    else
      coll.Update(page);

    return (true, page, null);
  }
}

public record Page
{
  public int Id { get; set; }

  public string Name { get; set; } = string.Empty;

  public string Content { get; set; } = string.Empty;

  public DateTimeOffset LastModified { get; set; }
}

public record PageInput(int? Id, string Name, string Content);
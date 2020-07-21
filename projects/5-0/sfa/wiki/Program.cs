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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ganss.XSS;
using FluentValidation;
using FluentValidation.AspNetCore;

const string DisplayDateFormat = "MMMM dd, yyyy";
const string HomePageName = "home-page";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<Wiki>();
builder.Services.AddAntiforgery();
builder.Services.AddMemoryCache();

var app = builder.Build();

DateTimeOffset Timestamp() => DateTimeOffset.UtcNow;

app.MapGet("/", async context =>
{
  var wiki = context.RequestServices.GetService<Wiki>()!;
  Page? page = wiki.GetPage(HomePageName);

  if (page is not object)
  {
    context.Response.Redirect($"/{HomePageName}");
    return;
  }

  await context.Response.WriteAsync(BuildPage(HomePageName, atBody: () =>
      new[]
      {
        RenderMarkdown(page!.Content),
        HtmlTags.A.Href($"/edit?pageName={HomePageName}").Append("Edit").ToHtmlString()
      },
      atSidePanel: () => AllPages(wiki)
    ).ToString());
});

app.MapGet("/edit", async context =>
{
  var wiki = context.RequestServices.GetService<Wiki>()!;
  var antiForgery = context.RequestServices.GetService<IAntiforgery>()!;

  var pageName = context.Request.Query["pageName"];

  Page? page = wiki.GetPage(pageName);
  if (page is not object)
  {
    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
    return;
  }

  await context.Response.WriteAsync(BuildEditorPage(pageName,
    atBody: () =>
      new[]
      {
          BuildForm(new PageInput(page!.Id, pageName, page.Content, null), path: $"{pageName}", antiForgery: antiForgery.GetAndStoreTokens(context))
      },
    atSidePanel: () => AllPages(wiki)).ToString());
});

app.MapGet("/{pageName}", async context =>
{
  var wiki = context.RequestServices.GetService<Wiki>()!;
  var antiForgery = context.RequestServices.GetService<IAntiforgery>()!;

  var pageName = context.Request.RouteValues["pageName"] as string ?? "";

  Page? page = wiki.GetPage(pageName);

  if (page is object)
  {
    await context.Response.WriteAsync(BuildPage(pageName, atBody: () =>
      new[]
      {
        RenderMarkdown(page!.Content),
        HtmlTags.Div.Class("last-modified").Append("Last modified: " + page!.LastModified.ToString(DisplayDateFormat)).ToHtmlString(),
        HtmlTags.A.Href($"/edit?pageName={pageName}").Append("Edit").ToHtmlString()
      },
      atSidePanel: () => AllPages(wiki)
    ).ToString());
  }
  else
  {
    await context.Response.WriteAsync(BuildEditorPage(pageName,
    atBody: () =>
      new[]
      {
        BuildForm(new PageInput(null, pageName, string.Empty, null), path: pageName, antiForgery: antiForgery.GetAndStoreTokens(context))
      },
    atSidePanel: () => AllPages(wiki)).ToString());
  }
});

app.MapPost("/{pageName}", async context =>
{
  var pageName = context.Request.RouteValues["pageName"] as string ?? "";
  var wiki = context.RequestServices.GetService<Wiki>()!;
  var antiForgery = context.RequestServices.GetService<IAntiforgery>()!;
  await antiForgery.ValidateRequestAsync(context);

  PageInput input = PageInput.From(context.Request.Form);

  var modelState = new ModelStateDictionary();
  var validator = new PageInputValidator(pageName, HomePageName);
  validator.Validate(input).AddToModelState(modelState, null);

  if (!modelState.IsValid)
  {
    await context.Response.WriteAsync(BuildEditorPage(pageName,
      atBody: () =>
        new[]
        {
            BuildForm(input, path: $"{pageName}", antiForgery: antiForgery.GetAndStoreTokens(context), modelState)
        },
      atSidePanel: () => AllPages(wiki)).ToString());
    return;
  }

  var sanitizer = new HtmlSanitizer();
  var properName = input.Name.ToString().Trim().Replace(' ', '-').ToLower();
  var page = new Page
  {
    Id = input.Id ?? default(int),
    Name = sanitizer.Sanitize(properName),
    Content = sanitizer.Sanitize(input.Content),
    LastModified = Timestamp()
  };

  var (isOK, p, ex) = wiki.SavePage(page);
  context.Response.Redirect($"/{p!.Name}");
});

await app.RunAsync();

// End of the web part

string RenderMarkdown(string str)  => Markdown.ToHtml(str, new MarkdownPipelineBuilder().UseAdvancedExtensions().Build());

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

string BuildForm(PageInput input, string path, AntiforgeryTokenSet antiForgery, ModelStateDictionary? modelState = null)
{
  bool IsFieldOK(string key) => modelState!.ContainsKey(key) && modelState[key].ValidationState == ModelValidationState.Invalid;

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

  var attachmentField = HtmlTags.Div.Class("field")
    .Append(HtmlTags.Label.Class("label").Append(nameof(input.Attachment)))
    .Append(HtmlTags.Div.Class("control")
      .Append(HtmlTags.Input.File.Name("Attachment"))
    );


  if (modelState is object && !modelState.IsValid)
  {
    if (IsFieldOK("Name"))
    {
      foreach(var er in modelState["Name"].Errors)
      {
        nameField = nameField.Append(HtmlTags.P.Class("help is-danger").Append(er.ErrorMessage));
      }
    }

    if (IsFieldOK("Content"))
    {
      foreach(var er in modelState["Content"].Errors)
      {
        contentField = contentField.Append(HtmlTags.P.Class("help is-danger").Append(er.ErrorMessage));
      }
    }
  }

  var submit = HtmlTags.Button.Class("button").Append("Submit");

  var form = HtmlTags.Form
             .Attribute("method", "post")
             .Attribute("enctype", "multipart/form-data")
             .Attribute("action", $"/{path}")
               .Append(antiForgeryField)
               .Append(nameField)
               .Append(contentField)
               .Append(attachmentField);

  if (input.Id.HasValue)
  {
    HtmlTag id = HtmlTags.Input.Hidden.Name("Id").Value(input.Id.ToString());
    form = form.Append(id);
  }

  form = form.Append(submit);

  return form.ToHtmlString();
}

string KebabToNormalCase(string txt) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txt.Replace('-', ' '));

HtmlString BuildEditorPage(string title, Func<IEnumerable<string>> atBody, Func<IEnumerable<string>>? atSidePanel = null) =>
   BuildPage(
    title,
    atHead: () => MarkdownEditorHead(),
    atBody: atBody,
    atSidePanel: atSidePanel,
    atFoot: () => MarkdownEditorFoot()
    );

HtmlString BuildPage(string title, Func<IEnumerable<string>>? atHead = null, Func<IEnumerable<string>>? atBody = null, Func<IEnumerable<string>>? atSidePanel = null, Func<IEnumerable<string>>? atFoot = null)
{
  var head = Template.Parse(@"
    <meta charset=""utf-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
    <title>{{ title }}</title>
    <link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/bulma@0.9.0/css/bulma.min.css"">
    {{ header }}
    <style>
      .last-modified { font-size: small; }
    </style>
  ").Render(new { title = title, header = string.Join("\r", atHead?.Invoke() ?? new[] { "" }) });

  var body = Template.Parse(@"
    {{ if at_side_panel != """" }}
    <div class=""columns"">
      <div class=""column is-four-fifths"">
        <div class=""container is-fluid content"">
          <h1 class=""title is-1"">{{ page_name }}</h1>
          {{ content }}
        </div>
      </div>
      <div class=""column"">
        {{ at_side_panel }}
      </div>
    </div>
    {{ else }}
    <div class=""container is-fluid content"">
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

    _cache.Set(AllPagesKey, items, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(CacheAllPagesForMinutes)));
    return items;
  }

  public Page? GetPage(string path)
  {
    using var db = new LiteDatabase(GetDbPath());
    var coll = db.GetCollection<Page>(PageCollectionName);
    coll.EnsureIndex(x => x.Name);

    return coll.Query()
            .Where(x => x.Name.Equals(path, StringComparison.OrdinalIgnoreCase))
            .FirstOrDefault();
  }

  public (bool isOK, Page? page, Exception? ex) SavePage(Page page)
  {
    try
    {
      using var db = new LiteDatabase(GetDbPath());
      var coll = db.GetCollection<Page>(PageCollectionName);
      coll.EnsureIndex(x => x.Name);

      if (page.Id == default(int))
        coll.Insert(page);
      else
        coll.Update(page);

      _cache.Remove(AllPagesKey);
      return (true, page, null);
    }
    catch(Exception ex)
    {
      return (false, null, ex);
    }
  }
}

public record Page
{
  public int Id { get; set; }

  public string Name { get; set; } = string.Empty;

  public string Content { get; set; } = string.Empty;

  public DateTimeOffset LastModified { get; set; }

  public List<Attachment> Attachments = new();
}

public record Attachment 
{
  public int Id { get; set; }

  public string Name { get; set; } = string.Empty;

  public string MimeType { get; set; } = string.Empty;

  public DateTimeOffset LastModified { get; set; }

}

public record PageInput(int? Id, string Name, string Content, IFormFile? Attachment)
{
  public static PageInput From(IFormCollection form)
  {
      var id = form["Id"];
      var name = form["Name"];
      var content = form["Content"];

      int? pageId = null;
      
      if (!StringValues.IsNullOrEmpty(id))
        pageId = Convert.ToInt32(id);

      IFormFile? file = form.Files["Attachment"];

      return new PageInput(pageId, name, content, file);
  }
}

public class PageInputValidator: AbstractValidator<PageInput>
{
  public PageInputValidator(string pageName, string homePageName)
  { 
      RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
      if (pageName.Equals(homePageName, StringComparison.OrdinalIgnoreCase))
        RuleFor(x => x.Name).Must(name => name.Equals(homePageName)).WithMessage($"You cannot modify home page name. Please keep it {homePageName}");

      RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required");
  }
}
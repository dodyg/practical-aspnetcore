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
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

const string DisplayDateFormat = "MMMM dd, yyyy";
const string HomePageName = "home-page";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<Wiki>();
builder.Services.AddSingleton<Render>();
builder.Services.AddAntiforgery();
builder.Services.AddMemoryCache();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);

var app = builder.Build();

// Load home page
app.MapGet("/", async context =>
{
    var wiki = context.RequestServices.GetService<Wiki>()!;
    var render = context.RequestServices.GetService<Render>()!;
    Page? page = wiki.GetPage(HomePageName);

    if (page is not object)
    {
        context.Response.Redirect($"/{HomePageName}");
        return;
    }

    await context.Response.WriteAsync(render.BuildPage(HomePageName, atBody: () =>
        new[]
        {
        RenderPageContent(page),
        RenderPageAttachments(page),
        HtmlTags.A.Href($"/edit?pageName={HomePageName}").Append("Edit").ToHtmlString()
        },
        atSidePanel: () => AllPages(wiki)
      ).ToString());
});

// Edit a wiki page
app.MapGet("/edit", async context =>
{
    app.Logger.LogInformation("Editing");
    var wiki = context.RequestServices.GetService<Wiki>()!;
    var render = context.RequestServices.GetService<Render>()!;

    var antiForgery = context.RequestServices.GetService<IAntiforgery>()!;
    var pageName = context.Request.Query["pageName"];

    Page? page = wiki.GetPage(pageName);
    if (page is not object)
    {
        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
        return;
    }

    await context.Response.WriteAsync(render.BuildEditorPage(pageName,
      atBody: () =>
        new[]
        {
          BuildForm(new PageInput(page!.Id, pageName, page.Content, null), path: $"{pageName}", antiForgery: antiForgery.GetAndStoreTokens(context))
        },
      atSidePanel: () => AllPages(wiki)).ToString());
});

// Deal with attachment download
app.MapGet("/attachment", async context =>
{
    var fileId = context.Request.Query["fileId"];
    var wiki = context.RequestServices.GetService<Wiki>()!;

    var file = wiki.GetFile(fileId);
    if (file is not object)
    {
        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
        return;
    }

    app!.Logger.LogInformation("Attachment " + file.Value.meta.Id + " - " + file.Value.meta.Filename);
    context.Response.Headers.Append(HeaderNames.ContentType, file.Value.meta.MimeType);
    await context.Response.Body.WriteAsync(file.Value.file);
});

// Load a wiki page
app.MapGet("/{pageName}", async context =>
{
    var wiki = context.RequestServices.GetService<Wiki>()!;
    var render = context.RequestServices.GetService<Render>()!;
    var antiForgery = context.RequestServices.GetService<IAntiforgery>()!;

    var pageName = context.Request.RouteValues["pageName"] as string ?? "";

    Page? page = wiki.GetPage(pageName);

    if (page is object)
    {
        await context.Response.WriteAsync(render.BuildPage(pageName, atBody: () =>
          new[]
          {
        RenderPageContent(page),
        RenderPageAttachments(page),
        HtmlTags.Div.Class("last-modified").Append("Last modified: " + page!.LastModified.ToString(DisplayDateFormat)).ToHtmlString(),
        HtmlTags.A.Href($"/edit?pageName={pageName}").Append("Edit").ToHtmlString()
          },
          atSidePanel: () => AllPages(wiki)
        ).ToString());
    }
    else
    {
        await context.Response.WriteAsync(render.BuildEditorPage(pageName,
        atBody: () =>
          new[]
          {
        BuildForm(new PageInput(null, pageName, string.Empty, null), path: pageName, antiForgery: antiForgery.GetAndStoreTokens(context))
          },
        atSidePanel: () => AllPages(wiki)).ToString());
    }
});

// Add or update a wiki page
app.MapPost("/{pageName}", async context =>
{
    var pageName = context.Request.RouteValues["pageName"] as string ?? "";
    var wiki = context.RequestServices.GetService<Wiki>()!;
    var render = context.RequestServices.GetService<Render>()!;
    var antiForgery = context.RequestServices.GetService<IAntiforgery>()!;
    await antiForgery.ValidateRequestAsync(context);

    PageInput input = PageInput.From(context.Request.Form);

    var modelState = new ModelStateDictionary();
    var validator = new PageInputValidator(pageName, HomePageName);
    validator.Validate(input).AddToModelState(modelState, null);

    if (!modelState.IsValid)
    {
        await context.Response.WriteAsync(render.BuildEditorPage(pageName,
          atBody: () =>
            new[]
            {
            BuildForm(input, path: $"{pageName}", antiForgery: antiForgery.GetAndStoreTokens(context), modelState)
            },
          atSidePanel: () => AllPages(wiki)).ToString());
        return;
    }

    var (isOK, p, ex) = wiki.SavePage(input);
    if (!isOK)
    {
        app.Logger.LogError(ex, "Problem in saving page");
        return;
    }

    context.Response.Redirect($"/{p!.Name}");
});

await app.RunAsync();

// End of the web part

string[] AllPages(Wiki wiki) => new[]
{
  "<ul>",
  string.Join("",
    wiki.ListAllPages().OrderBy(x => x.Name)
      .Select(x => HtmlTags.Li.Append(HtmlTags.A.Href(x.Name).Append(x.Name)).ToHtmlString()
    )
  ),
  "</ul>"
};

string RenderMarkdown(string str) => Markdown.ToHtml(str, new MarkdownPipelineBuilder().UseSoftlineBreakAsHardlineBreak().UseAdvancedExtensions().Build());

string RenderPageContent(Page page) => RenderMarkdown(page.Content);

string RenderPageAttachments(Page page)
{
    var tag = HtmlTags.Ul;
    foreach (var attachment in page.Attachments)
    {
        tag = tag.Append(HtmlTags.Li.Append(HtmlTags.A.Href($"/attachment?fileId={attachment.FileId}").Append(attachment.FileName)));
    }
    return tag.ToHtmlString();
}

// Build the wiki input form 
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
            foreach (var er in modelState["Name"].Errors)
            {
                nameField = nameField.Append(HtmlTags.P.Class("help is-danger").Append(er.ErrorMessage));
            }
        }

        if (IsFieldOK("Content"))
        {
            foreach (var er in modelState["Content"].Errors)
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


class Render
{
  string KebabToNormalCase(string txt) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txt.Replace('-', ' '));

  string[] MarkdownEditorHead() => new[]
  {
    @"<link rel=""stylesheet"" href=""https://unpkg.com/easymde/dist/easymde.min.css"">",
    @"<script src=""https://unpkg.com/easymde/dist/easymde.min.js""></script>"
  };

  string[] MarkdownEditorFoot() => new[]
  {
    @"<script>
      var easyMDE = new EasyMDE({
        insertTexts: {
          link: [""["", ""]()""]
        }
      });
      </script>"
  };

  (Template head, Template body, Template layout) _templates = (
    Template.Parse(@"
        <meta charset=""utf-8"">
        <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
        <title>{{ title }}</title>
        <link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/bulma@0.9.0/css/bulma.min.css"">
        {{ header }}
        <style>
          .last-modified { font-size: small; }
          a:visited { color: blue; }
          a:link { color: red; }
        </style>
      "),
  Template.Parse(@"
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
      "),
  Template.Parse(@"
      <!DOCTYPE html>
        <head>
          {{ head }}
        </head>
        <body>
          {{ body }}
        </body>
      </html>
    ")
  );

  // Use only when the page requires editor
  public HtmlString BuildEditorPage(string title, Func<IEnumerable<string>> atBody, Func<IEnumerable<string>>? atSidePanel = null) =>
    BuildPage(
      title,
      atHead: () => MarkdownEditorHead(),
      atBody: atBody,
      atSidePanel: atSidePanel,
      atFoot: () => MarkdownEditorFoot()
      );

  // General page layout building function
  public HtmlString BuildPage(string title, Func<IEnumerable<string>>? atHead = null, Func<IEnumerable<string>>? atBody = null, Func<IEnumerable<string>>? atSidePanel = null, Func<IEnumerable<string>>? atFoot = null)
  {
      var head = _templates.head.Render(new
      {
          title = title,
          header = string.Join("\r", atHead?.Invoke() ?? new[] { "" })
      });

      var body = _templates.body.Render(new
      {
          PageName = KebabToNormalCase(title),
          Content = string.Join("\r", atBody?.Invoke() ?? new[] { "" }),
          AtSidePanel = string.Join("\r", atSidePanel?.Invoke() ?? new[] { "" }),
          AtFoot = string.Join("\r", atFoot?.Invoke() ?? new[] { "" })
      });

      return new HtmlString(_templates.layout.Render(new { head, body }));
  }
}

class Wiki
{
    DateTimeOffset Timestamp() => DateTimeOffset.UtcNow;

    const string PageCollectionName = "Pages";
    const string AllPagesKey = "AllPages";
    const double CacheAllPagesForMinutes = 30;

    readonly IWebHostEnvironment _env;
    readonly IMemoryCache _cache;
    readonly ILogger _logger;

    public Wiki(IWebHostEnvironment env, IMemoryCache cache, ILogger<Wiki> logger)
    {
        _env = env!;
        _cache = cache;
        _logger = logger;
    }

    // Get the location of the LiteDB file.
    string GetDbPath() => Path.Combine(_env.ContentRootPath, "wiki.db");

    // List all the available wiki pages. It is cached for 30 minutes.
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

    // Save or update a wiki page. Cache(AllPagesKey) will be destroyed.
    public (bool isOK, Page? page, Exception? ex) SavePage(PageInput input)
    {
        try
        {
            using var db = new LiteDatabase(GetDbPath());
            var coll = db.GetCollection<Page>(PageCollectionName);
            coll.EnsureIndex(x => x.Name);

            Page? existingPage = input.Id.HasValue ? coll.FindOne(x => x.Id == input.Id) : null;

            var sanitizer = new HtmlSanitizer();
            var properName = input.Name.ToString().Trim().Replace(' ', '-').ToLower();

            Attachment? attachment = null;
            if (!string.IsNullOrWhiteSpace(input.Attachment?.FileName))
            {
                attachment = new Attachment
                {
                    FileName = input.Attachment.FileName,
                    MimeType = input.Attachment.ContentType,
                    LastModified = Timestamp()
                };

                using var stream = input.Attachment.OpenReadStream();
                var res = db.FileStorage.Upload(attachment.FileId, input.Attachment.FileName, stream);
            }

            if (existingPage is not object)
            {
                var newPage = new Page
                {
                    Name = sanitizer.Sanitize(properName),
                    Content = sanitizer.Sanitize(input.Content),
                    LastModified = Timestamp()
                };

                if (attachment is object)
                    newPage.Attachments.Add(attachment);

                coll.Insert(newPage);
            }
            else
            {
                var updatedPage = existingPage with
                {
                    Name = sanitizer.Sanitize(properName),
                    Content = sanitizer.Sanitize(input.Content),
                    LastModified = Timestamp()
                };

                if (attachment is object)
                    updatedPage.Attachments.Add(attachment);

                _logger.LogInformation("Updated page attachments " + updatedPage.Attachments.Count);

                coll.Update(updatedPage);
            }

            _cache.Remove(AllPagesKey);
            return (true, existingPage, null);
        }
        catch (Exception ex)
        {
            return (false, null, ex);
        }
    }

    // Return null if file cannot be found.
    public (LiteFileInfo<string> meta, byte[] file)? GetFile(string fileId)
    {
        using var db = new LiteDatabase(GetDbPath());

        var meta = db.FileStorage.FindById(fileId);
        if (meta == null)
            return null;

        using var stream = new MemoryStream();
        db.FileStorage.Download(fileId, stream);
        return (meta, stream.ToArray());
    }
}

public record Page
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public DateTimeOffset LastModified { get; set; }

    public List<Attachment> Attachments { get; set; } = new();
}

public record Attachment
{
    public string FileId { get; set; } = Guid.NewGuid().ToString();

    public string FileName { get; set; } = string.Empty;

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

public class PageInputValidator : AbstractValidator<PageInput>
{
    public PageInputValidator(string pageName, string homePageName)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        if (pageName.Equals(homePageName, StringComparison.OrdinalIgnoreCase))
            RuleFor(x => x.Name).Must(name => name.Equals(homePageName)).WithMessage($"You cannot modify home page name. Please keep it {homePageName}");

        RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required");
    }
}
using FluentValidation;
using FluentValidation.AspNetCore;
using Ganss.XSS;
using HtmlBuilders;
using LiteDB;
using Markdig;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Scriban;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static HtmlBuilders.HtmlTags;

const string DisplayDateFormat = "MMMM dd, yyyy";
const string HomePageName = "home-page";

var builder = WebApplication.CreateBuilder(args);
builder.Services
  .AddSingleton<Wiki>()
  .AddSingleton<Render>()
  .AddAntiforgery()
  .AddMemoryCache();

builder.Logging.AddConsole().SetMinimumLevel(LogLevel.Information);

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
          A.Href($"/edit?pageName={HomePageName}").Class("uk-button uk-button-default uk-button-small").Append("Edit").ToHtmlString()
        },
        atSidePanel: () => AllPages(wiki)
      ).ToString());
});

app.MapGet("/new-page", context =>
{
    var pageName = context.Request.Query["pageName"];
    if (StringValues.IsNullOrEmpty(pageName))
    {
        context.Response.Redirect("/");
        return Task.CompletedTask;
    }

    // Copied from https://www.30secondsofcode.org/c-sharp/s/to-kebab-case
    string ToKebabCase(string str)
    {
        Regex pattern = new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
        return string.Join("-", pattern.Matches(str)).ToLower();
    }

    var page = ToKebabCase(pageName);
    context.Response.Redirect($"/{page}");
    return Task.CompletedTask;
});


// Edit a wiki page
app.MapGet("/edit", async context =>
{
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
          BuildForm(new PageInput(page!.Id, pageName, page.Content, null), path: $"{pageName}", antiForgery: antiForgery.GetAndStoreTokens(context)),
          RenderPageAttachmentsForEdit(page)
        },
      atSidePanel: () =>
      {
          var list = new List<string>();
          // Do not show delete button on home page
          if (!pageName!.ToString().Equals(HomePageName, StringComparison.Ordinal))
            list.Add(RenderDeletePageButton(page!, antiForgery: antiForgery.GetAndStoreTokens(context)));
          
          list.Add(Br.ToHtmlString());
          list.AddRange(AllPagesForEditing(wiki));
          return list;
      }).ToString());
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
            Div.Class("last-modified").Append("Last modified: " + page!.LastModifiedUtc.ToString(DisplayDateFormat)).ToHtmlString(),
            A.Href($"/edit?pageName={pageName}").Append("Edit").ToHtmlString()
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
        atSidePanel: () => AllPagesForEditing(wiki)).ToString());
    }
});

// Delete a page
app.MapPost("/delete-page", async context =>
{
    var antiForgery = context.RequestServices.GetService<IAntiforgery>()!;
    await antiForgery.ValidateRequestAsync(context);
    var wiki = context.RequestServices.GetService<Wiki>()!;
    var id = context.Request.Form["id"];

    if (StringValues.IsNullOrEmpty(id))
    {
        context.Response.Redirect("/");
        return;
    }

    var (isOk, exception ) = wiki.DeletePage(Convert.ToInt32(id), HomePageName);

    if (!isOk && exception is object)
      app.Logger.LogError(exception, $"Error in deleting page id {id}");
    else if (!isOk)
      app.Logger.LogError($"Unable to delete page id {id}");

    context.Response.Redirect("/");
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

static string[] AllPages(Wiki wiki) => new[]
{
  @"<span class=""uk-label"">Pages</span>",
  @"<ul class=""uk-list"">",
  string.Join("",
    wiki.ListAllPages().OrderBy(x => x.Name)
      .Select(x => Li.Append(A.Href(x.Name).Append(x.Name)).ToHtmlString()
    )
  ),
  "</ul>"
};

static string[] AllPagesForEditing(Wiki wiki)
{
    static string KebabToNormalCase(string txt) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txt.Replace('-', ' '));

    return new[]
    {
      @"<span class=""uk-label"">Pages</span>",
      @"<ul class=""uk-list"">",
      string.Join("",
        wiki.ListAllPages().OrderBy(x => x.Name)
          .Select(x => Li.Append(Div.Class("uk-inline")
              .Append(Span.Class("uk-form-icon").Attribute("uk-icon", "icon: copy"))
              .Append(Input.Text.Value($"[{KebabToNormalCase(x.Name)}](/{x.Name})").Class("uk-input uk-form-small").Style("cursor", "pointer").Attribute("onclick", "copyMarkdownLink(this);"))
          ).ToHtmlString()
        )
      ),
      "</ul>"
    };
}

static string RenderMarkdown(string str)
{
    var sanitizer = new HtmlSanitizer();
    return sanitizer.Sanitize(Markdown.ToHtml(str, new MarkdownPipelineBuilder().UseSoftlineBreakAsHardlineBreak().UseAdvancedExtensions().Build()));
}

static string RenderPageContent(Page page) => RenderMarkdown(page.Content);

static string RenderDeletePageButton(Page page, AntiforgeryTokenSet antiForgery)
{
    var antiForgeryField = Input.Hidden.Name(antiForgery.FormFieldName).Value(antiForgery.RequestToken);
    HtmlTag id = Input.Hidden.Name("Id").Value(page.Id.ToString());
    var submit = Div.Style("margin-top", "20px").Append(Button.Class("uk-button uk-button-danger").Append("Delete Page"));

    var form = Form
               .Attribute("method", "post")
               .Attribute("action", $"/delete-page")
               .Attribute("onsubmit", $"return confirm('Please confirm to delete this page');")
                 .Append(antiForgeryField)
                 .Append(id)
                 .Append(submit);

    return form.ToHtmlString();
}

static string RenderPageAttachmentsForEdit(Page page)
{
    if (page.Attachments.Count == 0)
        return string.Empty;

    var label = Span.Class("uk-label").Append("Attachments");
    var list = Ul.Class("uk-list");
    foreach (var attachment in page.Attachments)
    {
        list = list.Append(
          Li.Append(Div.Class("uk-inline")
          .Append(Span.Class("uk-form-icon").Attribute("uk-icon", "icon: copy"))
          .Append(Input.Text.Value($"[{attachment.FileName}](/attachment?fileId={attachment.FileId})").Class("uk-input uk-form-small uk-form-width-large").Style("cursor", "pointer").Attribute("onclick", "copyMarkdownLink(this);"))
      )
      );
    }
    return label.ToHtmlString() + list.ToHtmlString();
}

static string RenderPageAttachments(Page page)
{
    if (page.Attachments.Count == 0)
        return string.Empty;

    var label = Span.Class("uk-label").Append("Attachments");
    var list = Ul.Class("uk-list uk-list-disc");
    foreach (var attachment in page.Attachments)
    {
        list = list.Append(Li.Append(A.Href($"/attachment?fileId={attachment.FileId}").Append(attachment.FileName)));
    }
    return label.ToHtmlString() + list.ToHtmlString();
}

// Build the wiki input form 
static string BuildForm(PageInput input, string path, AntiforgeryTokenSet antiForgery, ModelStateDictionary? modelState = null)
{
    bool IsFieldOK(string key) => modelState!.ContainsKey(key) && modelState[key].ValidationState == ModelValidationState.Invalid;

    var antiForgeryField = Input.Hidden.Name(antiForgery.FormFieldName).Value(antiForgery.RequestToken);

    var nameField = Div
      .Append(Label.Class("uk-form-label").Append(nameof(input.Name)))
      .Append(Div.Class("uk-form-controls")
        .Append(Input.Text.Class("uk-input").Name("Name").Value(input.Name))
      );

    var contentField = Div
      .Append(Label.Class("uk-form-label").Append(nameof(input.Content)))
      .Append(Div.Class("uk-form-controls")
        .Append(Textarea.Name("Content").Class("uk-textarea").Append(input.Content))
      );

    var attachmentField = Div
      .Append(Label.Class("uk-form-label").Append(nameof(input.Attachment)))
      .Append(Div.Attribute("uk-form-custom", "target: true")
        .Append(Input.File.Name("Attachment"))
        .Append(Input.Text.Class("uk-input uk-form-width-large").Attribute("placeholder", "Click to select file").ToggleAttribute("disabled", true))
      );

    if (modelState is object && !modelState.IsValid)
    {
        if (IsFieldOK("Name"))
        {
            foreach (var er in modelState["Name"].Errors)
            {
                nameField = nameField.Append(Div.Class("uk-form-danger uk-text-small").Append(er.ErrorMessage));
            }
        }

        if (IsFieldOK("Content"))
        {
            foreach (var er in modelState["Content"].Errors)
            {
                contentField = contentField.Append(Div.Class("uk-form-danger uk-text-small").Append(er.ErrorMessage));
            }
        }
    }

    var submit = Div.Style("margin-top", "20px").Append(Button.Class("uk-button uk-button-primary").Append("Submit"));

    var form = Form
               .Class("uk-form-stacked")
               .Attribute("method", "post")
               .Attribute("enctype", "multipart/form-data")
               .Attribute("action", $"/{path}")
                 .Append(antiForgeryField)
                 .Append(nameField)
                 .Append(contentField)
                 .Append(attachmentField);

    if (input.Id.HasValue)
    {
        HtmlTag id = Input.Hidden.Name("Id").Value(input.Id.ToString());
        form = form.Append(id);
    }

    form = form.Append(submit);

    return form.ToHtmlString();
}

class Render
{
    static string KebabToNormalCase(string txt) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txt.Replace('-', ' '));

    static string[] MarkdownEditorHead() => new[]
    {
      @"<link rel=""stylesheet"" href=""https://unpkg.com/easymde/dist/easymde.min.css"">",
      @"<script src=""https://unpkg.com/easymde/dist/easymde.min.js""></script>"
    };

    static string[] MarkdownEditorFoot() => new[]
    {
      @"<script>
        var easyMDE = new EasyMDE({
          insertTexts: {
            link: [""["", ""]()""]
          }
        });

        function copyMarkdownLink(element) {
          element.select();
          document.execCommand(""copy"");
        }
        </script>"
    };

    (Template head, Template body, Template layout) _templates = (
      head: Scriban.Template.Parse(@"
        <meta charset=""utf-8"">
        <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
        <title>{{ title }}</title>
        <link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/uikit@3.5.5/dist/css/uikit.min.css"" />
        {{ header }}
        <style>
          .last-modified { font-size: small; }
          a:visited { color: blue; }
          a:link { color: red; }
        </style>
      "),
      body: Scriban.Template.Parse(@"
      <nav class=""uk-navbar-container"">
        <div class=""uk-container"">
          <div class=""uk-navbar"">
            <div class=""uk-navbar-left"">
              <ul class=""uk-navbar-nav"">
                <li class=""uk-active""><a href=""/""><span uk-icon=""home""></span></a></li>
              </ul>
            </div>
            <div class=""uk-navbar-center"">
              <div class=""uk-navbar-item"">
                <form action=""/new-page"">
                  <input class=""uk-input uk-form-width-large"" type=""text"" name=""pageName"" placeholder=""Type desired page title here""></input>
                  <input type=""submit""  class=""uk-button uk-button-default"" value=""Add New Page"">
                </form>
              </div>
            </div>
          </div>
        </div>
      </nav>
      {{ if at_side_panel != """" }}
        <div class=""uk-container"">
        <div uk-grid>
          <div class=""uk-width-4-5"">
            <h1>{{ page_name }}</h1>
            {{ content }}
          </div>
          <div class=""uk-width-1-5"">
            {{ at_side_panel }}
          </div>
        </div>
        </div>
      {{ else }}
        <div class=""uk-container"">
          <h1>{{ page_name }}</h1>
          {{ content }}
        </div>
      {{ end }}
            
      <script src=""https://cdn.jsdelivr.net/npm/uikit@3.5.5/dist/js/uikit.min.js""></script>
      <script src=""https://cdn.jsdelivr.net/npm/uikit@3.5.5/dist/js/uikit-icons.min.js""></script>    
      {{ at_foot }}
      "),
      layout: Scriban.Template.Parse(@"
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
            title,
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
    DateTime Timestamp() => DateTime.UtcNow;

    const string PageCollectionName = "Pages";
    const string AllPagesKey = "AllPages";
    const double CacheAllPagesForMinutes = 30;

    readonly IWebHostEnvironment _env;
    readonly IMemoryCache _cache;
    readonly ILogger _logger;

    public Wiki(IWebHostEnvironment env, IMemoryCache cache, ILogger<Wiki> logger)
    {
        _env = env;
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

    // Get a wiki page based on its path
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
                (
                    FileId: Guid.NewGuid().ToString(),
                    FileName: input.Attachment.FileName,
                    MimeType: input.Attachment.ContentType,
                    LastModifiedUtc: Timestamp()
                );

                using var stream = input.Attachment.OpenReadStream();
                var res = db.FileStorage.Upload(attachment.FileId, input.Attachment.FileName, stream);
            }

            if (existingPage is not object)
            {
                var newPage = new Page
                {
                    Name = sanitizer.Sanitize(properName),
                    Content = input.Content, //Do not sanitize on input because it will impact some markdown tag such as >. We do it on the output instead.
                    LastModifiedUtc = Timestamp()
                };

                if (attachment is object)
                    newPage.Attachments.Add(attachment);

                coll.Insert(newPage);

                _cache.Remove(AllPagesKey);
                return (true, newPage, null);
            }
            else
            {
                var updatedPage = existingPage with
                {
                    Name = sanitizer.Sanitize(properName),
                    Content = input.Content, //Do not sanitize on input because it will impact some markdown tag such as >. We do it on the output instead.
                    LastModifiedUtc = Timestamp()
                };

                if (attachment is object)
                    updatedPage.Attachments.Add(attachment);

                coll.Update(updatedPage);

                _cache.Remove(AllPagesKey);
                return (true, updatedPage, null);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"There is an exception in trying to save page name '{input.Name}'");
            return (false, null, ex);
        }
    }

    public (bool isOK, Exception? ex) DeletePage(int id, string homePageName)
    {
      try
      {
        using var db = new LiteDatabase(GetDbPath());
        var coll = db.GetCollection<Page>(PageCollectionName);

        var page = coll.FindById(id);

        if (page is not object)
        {
          _logger.LogInformation($"Delete operation fails because page id {id} cannot be found in the database");
          return (false, null);
        }

        if (page.Name.Equals(homePageName, StringComparison.OrdinalIgnoreCase))
        {
          _logger.LogInformation($"Page id {id}  is a home page and elete operation on home page is not allowed");
          return (false, null);
        }

        //Delete all the attachments
        foreach(var a in page.Attachments)
        {
          db.FileStorage.Delete(a.FileId);
        }

        if (coll.Delete(id))
        {
          _cache.Remove(AllPagesKey);
          return (true, null);
        }

        _logger.LogInformation($"Somehow we cannot delete page id {id} and it's a mistery why.");
        return (false, null);
      }
      catch(Exception ex)
      {
        _logger.LogError(ex, $"Exception in trying to delete page id {id}");
        return (false, ex);
      }
    }

    // Return null if file cannot be found.
    public (LiteFileInfo<string> meta, byte[] file)? GetFile(string fileId)
    {
        using var db = new LiteDatabase(GetDbPath());

        var meta = db.FileStorage.FindById(fileId);
        if (meta is not object)
            return null;

        using var stream = new MemoryStream();
        db.FileStorage.Download(fileId, stream);
        return (meta, stream.ToArray());
    }
}

record Page
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public DateTime LastModifiedUtc { get; set; }

    public List<Attachment> Attachments { get; set; } = new();
}

record Attachment
(
    string FileId,

    string FileName,

    string MimeType,

    DateTime LastModifiedUtc
);

record PageInput(int? Id, string Name, string Content, IFormFile? Attachment)
{
    public static PageInput From(IFormCollection form)
    {
        var (id, name, content) = (form["Id"], form["Name"], form["Content"]);

        int? pageId = null;

        if (!StringValues.IsNullOrEmpty(id))
            pageId = Convert.ToInt32(id);

        IFormFile? file = form.Files["Attachment"];

        return new PageInput(pageId, name, content, file);
    }
}

class PageInputValidator : AbstractValidator<PageInput>
{
    public PageInputValidator(string pageName, string homePageName)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        if (pageName.Equals(homePageName, StringComparison.OrdinalIgnoreCase))
            RuleFor(x => x.Name).Must(name => name.Equals(homePageName)).WithMessage($"You cannot modify home page name. Please keep it {homePageName}");

        RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required");
    }
}
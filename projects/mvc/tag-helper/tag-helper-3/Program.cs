using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.MapDefaultControllerRoute();
app.Run();

public enum AlertType
{
    Success,
    Warning,
    Info,
    Danger
}

[HtmlTargetElement("alert")]
public class AlertHelper : TagHelper
{
    public AlertType Type { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.Add("class", $"alert {GetAlertType()}");
    }

    string GetAlertType()
    {
        switch (Type)
        {
            case AlertType.Success: return "alert-success";
            case AlertType.Warning: return "alert-warning";
            case AlertType.Info: return "alert-info";
            case AlertType.Danger: return "alert-danger";
            default: throw new System.ArgumentOutOfRangeException();
        }
    }
}

public class HomeController : Controller
{
    public ActionResult Index()
    {
        return View("Index");
    }
}

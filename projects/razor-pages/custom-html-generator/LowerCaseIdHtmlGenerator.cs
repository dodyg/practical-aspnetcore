using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;

public class LowerCaseIdHtmlGenerator : DefaultHtmlGenerator
{
    public LowerCaseIdHtmlGenerator(
        IAntiforgery antiforgery,
        IOptions<MvcViewOptions> optionsAccessor,
        IModelMetadataProvider metadataProvider,
        IUrlHelperFactory urlHelper,
        HtmlEncoder htmlEncoder,
        ValidationHtmlAttributeProvider validationProvider)
       : base(antiforgery, optionsAccessor, metadataProvider, urlHelper, htmlEncoder, validationProvider)
    {

    }

    public override TagBuilder GenerateCheckBox(ViewContext viewContext, ModelExplorer modelExplorer, string expression, bool? isChecked, object htmlAttributes)
    {
        var builder = base.GenerateCheckBox(viewContext, modelExplorer, expression, isChecked, htmlAttributes);

        if (!builder.Attributes.TryGetValue("id", out string id))
            return builder;

        builder.Attributes["id"] = ConverToLowerCase(id);

        return builder;
    }

    public override TagBuilder GenerateForm(ViewContext viewContext, string actionName, string controllerName, object routeValues, string method, object htmlAttributes)
    {
        var builder = base.GenerateForm(viewContext, actionName, controllerName, routeValues, method, htmlAttributes);

        if (!builder.Attributes.TryGetValue("id", out string id))
            return builder;

        builder.Attributes["id"] = ConverToLowerCase(id);

        return builder;
    }

    public override TagBuilder GenerateHidden(ViewContext viewContext, ModelExplorer modelExplorer, string expression, object value, bool useViewData, object htmlAttributes)
    {
        var builder = base.GenerateHidden(viewContext, modelExplorer, expression, value, useViewData, htmlAttributes);

        if (!builder.Attributes.TryGetValue("id", out string id))
            return builder;

        builder.Attributes["id"] = ConverToLowerCase(id);

        return builder;
    }

    public override TagBuilder GenerateHiddenForCheckbox(ViewContext viewContext, ModelExplorer modelExplorer, string expression)
    {
        var builder = base.GenerateHiddenForCheckbox(viewContext, modelExplorer, expression);

        if (!builder.Attributes.TryGetValue("id", out string id))
            return builder;

        builder.Attributes["id"] = ConverToLowerCase(id);

        return builder;
    }

    public override TagBuilder GenerateLabel(ViewContext viewContext, ModelExplorer modelExplorer, string expression, string labelText, object htmlAttributes)
    {
        var builder = base.GenerateLabel(viewContext, modelExplorer, expression, labelText, htmlAttributes);

        if (!builder.Attributes.TryGetValue("id", out string id))
            return builder;

        builder.Attributes["id"] = ConverToLowerCase(id);

        return builder;
    }

    public override TagBuilder GeneratePassword(ViewContext viewContext, ModelExplorer modelExplorer, string expression, object value, object htmlAttributes)
    {
        var builder = base.GeneratePassword(viewContext, modelExplorer, expression, value, htmlAttributes);

        if (!builder.Attributes.TryGetValue("id", out string id))
            return builder;

        builder.Attributes["id"] = ConverToLowerCase(id);

        return builder;
    }

    public override TagBuilder GenerateRadioButton(ViewContext viewContext, ModelExplorer modelExplorer, string expression, object value, bool? isChecked, object htmlAttributes)
    {
        var builder = base.GenerateRadioButton(viewContext, modelExplorer, expression, value, isChecked, htmlAttributes);

        if (!builder.Attributes.TryGetValue("id", out string id))
            return builder;

        builder.Attributes["id"] = ConverToLowerCase(id);

        return builder;
    }

    public override TagBuilder GenerateSelect(ViewContext viewContext, ModelExplorer modelExplorer, string optionLabel, string expression, IEnumerable<SelectListItem> selectList, ICollection<string> currentValues, bool allowMultiple, object htmlAttributes)
    {
        var builder = base.GenerateSelect(viewContext, modelExplorer, optionLabel, expression, selectList, currentValues, allowMultiple, htmlAttributes);

        if (!builder.Attributes.TryGetValue("id", out string id))
            return builder;

        builder.Attributes["id"] = ConverToLowerCase(id);

        return builder;
    }


    public override TagBuilder GenerateTextArea(ViewContext viewContext, ModelExplorer modelExplorer, string expression, int rows, int columns, object htmlAttributes)
    {
        var builder = base.GenerateTextArea(viewContext, modelExplorer, expression, rows, columns, htmlAttributes);

        if (!builder.Attributes.TryGetValue("id", out string id))
            return builder;

        builder.Attributes["id"] = ConverToLowerCase(id);

        return builder;
    }

    public override TagBuilder GenerateTextBox(ViewContext viewContext, ModelExplorer modelExplorer, string expression, object value, string format, object htmlAttributes)
    {
        var builder = base.GenerateTextBox(viewContext, modelExplorer, expression, value, format, htmlAttributes);

        if (!builder.Attributes.TryGetValue("id", out string id))
            return builder;

        builder.Attributes["id"] = ConverToLowerCase(id);

        return builder;
    }

    private static string ConverToLowerCase(in string id)
    {
        var split = id.Split('_');

        string newId = PascalToSnakeCase(split[0]) + '_';

        foreach (var x in split.Skip(1))
            newId += PascalToSnakeCase(x);

        return newId;
    }

    public static string PascalToSnakeCase(in string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        return Regex.Replace(
            value,
            "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])",
            "_$1",
            RegexOptions.Compiled)
            .Trim()
            .ToLower();
    }
}


using Microsoft.AspNetCore.WebUtilities;
var app = WebApplication.Create();
app.Run(async context =>
{
    var queryString = QueryHelpers.ParseQuery(context.Request.QueryString.ToString());

    var output = "";

    foreach (var qs in queryString)
    {
        output += qs.Key + " = " + qs.Value + "<br/>";
    }
    await context.Response.WriteAsync($@"<html>
    <body>
    <h1>Parsing Raw Query String</h1>
    <ul>
        <li><a href=""?name=anne"">?name=anne</a></li>
        <li><a href=""?name=anne&name=annie"">?name=anne&name=annie</a></li>
        <li><a href=""?age=25&smart=true"">?age=25&smart=true</a></li>
        <li><a href=""?country=zambia&country=senegal&country="">?country=zambia&country=senegal&country=</a></li>
        <li><a href=""?"">?</a></li>
        <br /><br /> 
        <strong>Query String</strong><br/> 
        {output}
    </ul>
    </body>
    </html>");
});

app.Run();
using Microsoft.AspNetCore.Http.Extensions;

var app = WebApplication.Create();

app.Run(context =>
{
    context.Response.Headers.Add("Content-Type", "text/html");

    var requestUrl = context.Request.GetEncodedUrl();
    UriHelper.FromAbsolute(requestUrl, out string scheme, out HostString host, out PathString path, out QueryString queryString, out FragmentString fragment);
    return context.Response.WriteAsync($@"<html>
<body>                
    <h1>From Absolute</h1>
    <i>Separates the given absolute URI string into components. Assumes no PathBase.</i>
    <a href=""https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.extensions.urihelper.fromabsolute?view=aspnetcore-6.0#Microsoft_AspNetCore_Http_Extensions_UriHelper_FromAbsolute_System_String_System_String__Microsoft_AspNetCore_Http_HostString__Microsoft_AspNetCore_Http_PathString__Microsoft_AspNetCore_Http_QueryString__Microsoft_AspNetCore_Http_FragmentString__"">Doc</a><br/><br/>
    <p style=""color:red;"">{ requestUrl }</p>

    <p>Click on the links to see what the helper method shows</p>
    <ul>
        <li><a href=""/about/us"">/about/us</a></li>
        <li><a href=""/city/?id=30"">/city/?id=30</a></li>
        <li><a href=""/continent/?id=300&lat=30&lng=87"">/continent/?id=300&lat=30&lng=87</a></li>
        <li><a href=""/person?name=annie"">/person?name=annie</a></li>
        <li><a href=""/president?name=Franklin D. Roosevelt"">/president?name=Franklin D. Roosevelt</a></li>
        <li><a href=""/"">/</a></li>
        <li><a href=""/#header"">/#header</a></li>
    </ul>
    <table border=""1"">
        <tr>
           <td>Scheme</td>
           <td>{scheme}</td>
        </tr>
        <tr>
           <td>Host</td>
           <td>{host}</td>
        </tr>
        <tr>
           <td>Path</td>
           <td>{path}</td>
        </tr>
        <tr>
           <td>QueryString</td>
           <td>{queryString}</td>
        </tr>
        <tr>
           <td>Fragment String <br/>
           (This is always empty because URL fragment is never sent to the server)
           </td>
           <td>{fragment}</td>
        </tr>
    </table>
</body>          
</html>         ");
});

app.Run();
using Microsoft.AspNetCore.Http.Extensions;

var app = WebApplication.Create();
app.Run(context =>
{
    context.Response.Headers.Add("Content-Type", "text/html");

    var url1 = UriHelper.BuildAbsolute(scheme: " http", host: new HostString("localhost:5000"));
    var url2 = UriHelper.BuildAbsolute(scheme: "http", host: new HostString("localhost:5000"), pathBase: new PathString("/admin"));
    var url3 = UriHelper.BuildAbsolute(scheme: "http", host: new HostString("localhost:5000"), pathBase: new PathString("/admin"), path: new PathString("/index"));
    var url4 = UriHelper.BuildAbsolute(scheme: "http", host: new HostString("localhost:5000"), pathBase: new PathString("/admin"), path: new PathString("/index"),
    query: new QueryString("?greeting=Annie&age=32"));

    var query5 = new QueryString()
    .Add("greeting", "Annie")
    .Add("age", "32");

    var url5 = UriHelper.BuildAbsolute(scheme: "http", host: new HostString("localhost:5000"), pathBase: new PathString("/admin"), path: new PathString("/index"),
    query: query5);

    var url6 = UriHelper.BuildAbsolute(scheme: "http", host: new HostString("localhost:5000"), pathBase: new PathString("/admin"), path: new PathString("/index"),
    query: new QueryString("?greeting=Annie&age=32"), fragment: new FragmentString("#phd"));

    var url7 = UriHelper.BuildAbsolute(scheme: "http", host: new HostString("localhost:5000"), pathBase: null, path: new PathString("/index"),
    query: new QueryString("?greeting=Annie&age=32"), fragment: new FragmentString("#phd"));

    var url8 = UriHelper.BuildAbsolute(scheme: "http", host: new HostString("localhost:5000"), pathBase: null, path: null,
    query: new QueryString("?greeting=Annie&age=32"), fragment: new FragmentString("#phd"));

    var url9 = UriHelper.BuildAbsolute(scheme: "http", host: new HostString("localhost:5000"), pathBase: null, path: null,
    query: QueryString.Empty, fragment: new FragmentString("#phd"));

    return context.Response.WriteAsync($@"<html>
<body>                
    <h1>UriHelper.BuildAbsolute</h1>
    Combines the given URI components into a string that is properly encoded for use in HTTP headers.
    <a href=""https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.extensions.urihelper.buildabsolute?view=aspnetcore-6-0"">doc</a>
    <ul>
        <li><a href=""{url1}"">{url1}</a></li>
        <li><a href=""{url2}"">{url2}</a></li>
        <li><a href=""{url3}"">{url3}</a></li>
        <li><a href=""{url4}"">{url4}</a></li>
        <li><a href=""{url5}"">{url5}</a></li>
        <li><a href=""{url6}"">{url6}</a></li>
        <li><a href=""{url7}"">{url7}</a></li>
        <li><a href=""{url8}"">{url8}</a></li>
        <li><a href=""{url9}"">{url9}</a></li>
    </ul>
</body>          
</html>         ");
});

app.Run();
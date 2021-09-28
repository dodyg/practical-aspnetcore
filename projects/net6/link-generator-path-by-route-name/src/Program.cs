using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

static IResult Index(LinkGenerator linker) => 
    Results.Text(@$"<html><body>
    <ul>
        <li><a href=""{linker.GetPathByName("about", values: null)}"">About</a></li>
        <li><a href=""{linker.GetPathByName("about", values: new { name = "anne"})}"">About Anne</a></li>
        <li><a href=""{linker.GetPathByName("about", values: new { name = "babka"})}"">About Babka</a></li>
    </ul>
    </body></html>" , "text/html");

static IResult About(string? name) => Results.Text(@$"<html><body><h1>About {name}</h1></body></html>"  , "text/html");

var app = WebApplication.Create();

app.Map("/", Index);
app.Map("/about", About).WithName("about");

app.Run();

   
 
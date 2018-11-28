Razor Class Libraries
=====================

With static files serving
-------------------------

'Hello World' sample with Razor Class Libraries serving some static files (css, images and JavaScript files).

List of commands to execute to build this sample from scratch:
* cd /src
* dotnet new razorclasslib -n RazorClassLibrary1
* dotnet new razorclasslib -n RazorClassLibrary2
* dotnet new classlib -n RazorClassLibraries.Mvc.Core
* dotnet new web -n WebApplication
* dotnet add RazorClassLibrary1/RazorClassLibrary1.csproj reference RazorClassLibraries.Mvc.Core/RazorClassLibraries.Mvc.Core.csproj
* dotnet add RazorClassLibrary2/RazorClassLibrary2.csproj reference RazorClassLibraries.Mvc.Core/RazorClassLibraries.Mvc.Core.csproj
* dotnet add WebApplication/WebApplication.csproj reference RazorClassLibrary1/RazorClassLibrary1.csproj RazorClassLibrary2/RazorClassLibrary2.csproj
* cd RazorClassLibrary1
* dotnet new page -n Index -o Areas/Module1/Pages
* cd ../
* cd RazorClassLibrary2
* dotnet new page -n Index -o Areas/Module2/Pages
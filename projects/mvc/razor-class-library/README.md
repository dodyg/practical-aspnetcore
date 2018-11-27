Razor Class Libraries
=====================

'Hello World' sample with Razor Class Libraries.

List of commands to execute to build this sample from scratch:
* cd /src
* dotnet new razorclasslib -n RazorClassLibrary1
* dotnet new razorclasslib -n RazorClassLibrary2
* dotnet new web -n WebApplication
* dotnet add WebApplication/WebApplication.csproj reference RazorClassLibrary1/RazorClassLibrary1.csproj RazorClassLibrary2/RazorClassLibrary2.csproj
* cd RazorClassLibrary1
* dotnet new page -n Index -o Areas/Module1/Pages
* cd ../
* cd RazorClassLibrary2
* dotnet new page -n Index -o Areas/Module2/Pages
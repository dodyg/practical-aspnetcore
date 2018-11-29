Razor Class Libraries
=====================

With Controllers and Views
--------------------------

List of commands to execute to build this sample from scratch:
* cd /src
* dotnet new razorclasslib -n RazorClassLibrary1
* dotnet new web -n WebApplication
* dotnet add WebApplication/WebApplication.csproj reference RazorClassLibrary1/RazorClassLibrary1.csproj
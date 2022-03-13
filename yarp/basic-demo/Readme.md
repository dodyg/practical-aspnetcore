YARP Demo
===========
This demo showcases creating reverse proxy in asp.net core by making use of YARP package. From the official documentation [here](https://microsoft.github.io/reverse-proxy/index.html), YARP is:    
    
    A library to help create reverse proxy servers that are high-performance, production-ready, and highly customizable
    
## Demo Details:
- BackEnd: 
    - File > New > ASP.NET Web API project with Weather controller.
    - Runs on `https://localhost:9004;http://localhost:9005`.
- FrontEnd: 
    - File > New > Blazor WASM project. 
    - Runs on `https://localhost:9002;http://localhost:9003`.
    - Connects to BackEnd to get weather data and displays in a table.
- Proxy: 
    - YARP reverse proxy implementation. Proxies FrontEnd & BackEnd
    - Runs on `https://localhost:9000;http://localhost:9001`.
    - YARP Configuration is set up in appsettings.json:
        ```
        "ReverseProxy": {
            "Routes": {
            "allrouteprops": {
                "ClusterId": "allclusterprops",
                "Match": {
                "Path": "{**catch-all}"
                }
            },
            "api": {
                "ClusterId": "api",
                "Match": {
                "Path": "/api/{**slug}"
                }
            }
            },
            "Clusters": {
            "allclusterprops": {
                "Destinations": {
                "frontend": {
                    "Address": "https://localhost:9002"
                }
                }
            },
            "api": {
                "Destinations": {
                "backend": {
                    "Address": "https://localhost:9004"
                }
                }
            }
            }
        }
        ```
    - YARP is configured using Minimal API design in `Program.cs`
        ```
        var builder = WebApplication.CreateBuilder(args);
        var yarpConfigSection = builder.Configuration.GetSection("ReverseProxy");
        builder.Services.AddReverseProxy()
                        .LoadFromConfig(yarpConfigSection);
        var app = builder.Build();
        app.MapReverseProxy();
        app.Run();
        ```
## Running the Demo
- Start `Yarp.Demo.BackEnd`
    - Open a terminal window and navigate to Yarp.Demo.BackEnd folder.
    - Execute command `dotnet run`.
- Start `Yarp.Demo.FrontEnd`
    - Open a terminal window and navigate to Yarp.Demo.FrontEnd folder.
    - Execute command `dotnet run`.
- Start `Yarp.Demo.Proxy`
    - Open a terminal window and navigate to Yarp.Demo.Proxy folder.
    - Execute command `dotnet run`.
- Open a browser. Navigate to `https://localhost:9000`

## Screenshots
* Main Page
<img src="assets/main-page.png" alt="main page" title="Main Page">

* Proxy Logs
<img src="assets/proxy-logs.png" alt="proxy logs" title="proxy logs">


## Credits
[Lohith GN](https://github.com/lohithgn)
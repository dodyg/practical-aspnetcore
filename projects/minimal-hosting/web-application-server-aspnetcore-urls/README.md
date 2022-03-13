# WebApplication - Set urls and ports via ASPNETCORE_URLS environment variable

This sample uses the brand new `WebApplication` hosting class. This sample shows how to set the Kestrel web server to listen to a specific url and port via `ASPNETCORE_URLS` environment variable.

## How to set Environment Variable ASPNETCORE_URLS on Windows using PowerShell

`dir env:` will show all environment variables.

`$env:ASPNETCORE_URLS = "http://localhost:3000;http://localhost:5000"` will set the environment variable `ASPNETCORE_URLS` to `http://localhost:3000;http://localhost:5000`.
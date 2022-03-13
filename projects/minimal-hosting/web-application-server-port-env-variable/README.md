# WebApplication - Reading the port from environment

This sample uses the brand new `WebApplication` hosting class. In this example we set Kestrel port from an environment variable `PORT`. You can open the page at `http://localhost:3000` or `http://localhost:5000` if you have set up the environment variable PORT to 5000.


## How to set Environment Variable PORT on Windows using PowerShell

`dir env:` will show all environment variables.

`$env:PORT = 5000` will set the environment variable `PORT` to `5000`.
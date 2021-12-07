# File Provider (10)

* [Serve static files](serve-static-files-1)

  Simply serve static files (html, css, images, etc).     

  There are two static files being served in this project, index.html and hello.css. They are stored under ```wwwroot``` folder, which is the default folder location for this library. 

  To access them you have to refer them directly e.g. ```localhost:5000/index.html``` and ```localhost:5000/hello.css```.

* [Allow Directory Browsing](serve-static-files-2)

  Allow listing and browsing of your ```wwwroot``` folder.

* [Use File Server](serve-static-files-3)

  Combines the functionality of ```UseStaticFiles, UseDefaultFiles, and UseDirectoryBrowser```.

* [Custom Directory Formatter](serve-static-files-4)

  Customize the way Directory Browsing is displayed. In this sample the custom view only handles flat directory. We will deal with 
more complex scenario in the next sample.

* [Custom Directory Formatter - 2](serve-static-files-5)

  Show custom Directory Browsing and handle directory listing as well as files.

* [Allow Directory Browsing](serve-static-files-6)

  Use Directory Browsing on a certain path using ```DirectoryBrowserOptions.RequestPath```, e.g. ```/browse```.

* [Serve Static Files from more than one folders](serve-static-files-7)

  This example shows how to serve static files from multiple directories (even outside your application path).

* [Physical File Provider - Content and Web roots](file-provider-physical)

  Access the file information on your Web and Content roots. 

* [Custom File Provider](file-provider-custom)

  Implement a simple and largely nonsense file provider. It is a good starting point to implement your own proper File Provider.

* [Log Static File Servings](serve-static-files-8)

  Log information about the static file being served.
    

dotnet6
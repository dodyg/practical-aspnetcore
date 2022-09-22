# Request (15)

  This section shows all the different ways you capture input and examine request to your web application.

* [Anti Forgery on Form](/projects/request/anti-forgery)

  This exists on since .NET Core 1.0 however the configuration for the cookie has changed slightly. We are using ```IAntiForgery``` interface to store and generate anti forgery token to prevent XSRF/CSRF attacks. 


* **HTTP Verb (1)**
  * [Get request verb](/projects/request/request-verb)
    
    Detect the verb/method of the current request. 

* **Headers (3)**
  * [Access Request Headers](/projects/request/request-headers)
    
    Enumerate all the available headers in a request.

  * [Access Request Headers using common HTTP header names contained in HeaderNames](/projects/request/request-headers-names)

    This sample shows all the common HTTP header names contained in `HeaderNames` class. So instead of using string to obtain a HTTP Header, you can just use a convenient constant such as `HeaderNames.ContentType`.

  * [Type Safe Access to Request Headers](/projects/request/request-headers-typed)
    
    Instead of using string to access HTTP headers, use type safe object properties to access common HTTP headers.

* **Query String (5)**
  * [Single value query string](/projects/request/query-string-1)

    Access single value query string.

  * [Multiple values query string](/projects/request/query-string-2)

    Access multiples values query string.

  * [List all query string values](/projects/request/query-string-3)

    List all query string values. Also shows the implicat conversion from ```StringValues``` to ```string```.

  There are multiple ways to generate query strings.

  * [Generate query string](/projects/request/form-url-encoded-content)

    Use `System.Net.Http.FormUrlEncodedContent` to generate URL encoded query string.

  * [Generate query string 2](/projects/request/query-string-create)

    Use `Microsoft.AspNetCore.Http.QueryString.Create` to generate URL encoded query string. 

  * More functionalities to generate and parse query string is available at [Web Utilities](/projects/web-utilities) section.

* **Form (2)**

  * [Form Values](/projects/request/form-values) 
    
    Handles the values submitted via a form.

  * [Form Upload File](/projects/request/form-upload-file) 
    
    Upload a single file and save it to the current directory (check out the usage of ```.UseContentRoot(Directory.GetCurrentDirectory())```)

* **Cookies (3)**
        
    * [Cookies](/projects/request/cookies-1)

      Read and write cookies.

    * [Removing cookies](/projects/request/cookies-2)

      Simply demonstrates on how to remove cookies.

    * [Accessing cookie issues by remote API via AJAX call](/projects/request/cookies-3)

      Demonstrates on how to access cookie values issued by remote API via AJAX call.

dotnet6
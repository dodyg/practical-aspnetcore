# Response (3)

* [Adding HTTP Response Header](/projects/response/response-header)

  Demonstrate on how to add a response header and where is allowed place to do it.

* [Default Gzip Output Compression](/projects/response/compression-response) 
   
  Compress everything using the default Gzip compression.

  _Everything_ means the following MIME output  

  * text/plain
  * text/css
  * application/javascript
  * text/html
  * application/xml
  * text/xml
  * application/json
  * text/json 

* [Trailing headers](/projects/response/trailing-headers)

  This example shows how to issue trailing HTTP headers. Normal HTTP headers must be issued before body of the HTTP Response starts being written. Trailing headers allows you to issue headers after the HTTP body has been written. 

  dotnet6
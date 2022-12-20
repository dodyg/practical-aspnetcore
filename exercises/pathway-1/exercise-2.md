# Service Driven Recipe Console

Take the recipe console application you have created and split it into two, one for backend with REST API and another is for the console.

* Use JSON to communicate data between the backend and the console.
* Backend handles all the business logic including the JSON reading/writing, creating a new recipe, etc.
* The console will make HTTP calls to the backend.
* The backend will provide REST APIs that the console will use.
* Relevant resources:
  * You will need to use this in your console app https://github.com/dodyg/practical-aspnetcore/tree/net6.0/projects/httpclientfactory
  * You can use the minimum Web API in ASP.NET Core 6 https://github.com/dodyg/practical-aspnetcore/
  * Make sure you have read and understood how HTTP protocol works and how to design Web APIs.
  

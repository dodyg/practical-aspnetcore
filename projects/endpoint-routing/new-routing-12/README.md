# New Routing - Obtaining an Endpoint metadata from your Razor Page depending on the request method

  Unlike in MVC, you can't use `Attribute` from the method of a Razor Page. You can only use it from the Model class. This makes getting obtaining the appropriate metadata for each request require an extra step.
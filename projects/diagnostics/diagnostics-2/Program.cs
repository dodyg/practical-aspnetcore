var app = WebApplication.Create();

app.UseDeveloperExceptionPage(); //Don't use this in production
app.Run(context => throw new ApplicationException("Hello World Exception"));

app.Run();
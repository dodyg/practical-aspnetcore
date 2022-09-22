var app = WebApplication.Create();

app.UseDeveloperExceptionPage();
app.Run(_ => throw new ApplicationException("Fake exception"));

app.Run();

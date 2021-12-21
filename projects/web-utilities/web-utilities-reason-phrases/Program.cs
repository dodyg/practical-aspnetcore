using Microsoft.AspNetCore.WebUtilities;

var app = WebApplication.Create();
app.Run(context =>
{
    return context.Response.WriteAsync($"{ReasonPhrases.GetReasonPhrase(200)} : {ReasonPhrases.GetReasonPhrase(500)}");
});

app.Run();
using CoreWCF;
using CoreWCF.Configuration;

var builder = WebApplication.CreateBuilder();
builder.WebHost.ConfigureKestrel(options => { options.ListenLocalhost(8080); });
builder.Services.AddServiceModelServices();

var app = builder.Build();
app.UseServiceModel(builder =>
{
    builder
        .AddService<EchoService>()
        .AddServiceEndpoint<EchoService, Contracts.IEchoService>(new BasicHttpBinding(), "/basichttp");
});

app.Run();
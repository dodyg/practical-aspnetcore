using Wangkanai.Detection;
using Wangkanai.Detection.Services;

var builder = WebApplication.CreateBuilder();
builder.Services.AddDetection();

var app = builder.Build();

//These are the four default services available at Configure
app.Run(context =>
{
    var device = context.RequestServices.GetService<IDetectionService>();

    return context.Response.WriteAsync($@"
                Useragent : {device.UserAgent}
                Device Type: {device.Device.Type}
                ");
});

app.Run();
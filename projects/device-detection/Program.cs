using Wangkanai.Detection;

var builder = WebApplication.CreateBuilder();
builder.Services.AddDetectionCore().AddDevice();

var app = builder.Build();

//These are the four default services available at Configure
app.Run(context =>
{
    var device = context.RequestServices.GetService<IDeviceResolver>();

    return context.Response.WriteAsync($@"
                Useragent : {device.UserAgent}
                Device Type: {device.Device.Type}
                ");
});

app.Run();
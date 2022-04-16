using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder();
builder.Services.AddCors(options =>
        {
            options.AddPolicy("all",
                policy => policy.WithOrigins("http://localhost:5002")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials());
        });

builder.Services.AddSignalR().AddJsonProtocol();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("all");
app.MapHub<ChatHub>("/chatHub");

app.Run(async context =>
{
    await context.Response.WriteAsync("This site host signalR server that allows remote clients to access thanks to CORS all policy.");
});

app.Run();

public class ChatHub : Hub
{
    public async Task Send(string message)
    {
        await this.Clients.All.SendAsync("Send", message);
    }
}

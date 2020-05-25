using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace ChatR.Services
{
    public class NotificationService : IAsyncDisposable
    {
        HubConnection _connection;

        ILogger _logger;

        public NotificationService(ILogger<NotificationService> logger)
        {
            _logger = logger;    
        }

        public async Task ConnectAsync()
        {
            _logger.LogInformation("ConnectAsync()");
            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/notificationhub")
                .Build();

            _connection.On<string, string>("BroadcastChannel", (user, message) =>
            {
                this.OnBroadcastMessage?.Invoke(user, message);
            });

            _connection.On<string>("ServerChannel", (message) => 
            {
                this.OnServerMessage?.Invoke(message);
            });

            if (_connection.State != HubConnectionState.Connected)
            {
                _logger.LogInformation("Start connecting to the SignalR Hub");
                await _connection.StartAsync();
            }
        }

        public Action<string> OnServerMessage { get; set; }

        public Action<string, string> OnBroadcastMessage { get; set; }

        public async Task BroadcastAsync(string sender, string message)
        {
            await _connection.InvokeAsync("Broadcast", sender, message);
        }

        public async ValueTask DisposeAsync()
        {
            if (_connection != null)
                await _connection.DisposeAsync();
        }
    }
}
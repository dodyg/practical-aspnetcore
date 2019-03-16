using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatR.Services
{
    public class NotificationService
    {
        HubConnection _connection;

        public async Task ConnectAsync()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/notificationhub")
                .Build();

            _connection.On<string, string>("BroadcastChannel", (user, message) =>
            {
                this.OnMessage?.Invoke(user, message);
            });

            if (_connection.State != HubConnectionState.Connected)
            {
                await _connection.StartAsync();
            }
        }

        public Action<string, string> OnMessage { get; set; }

        public async Task BroadcastAsync(string sender, string message)
        {
            await _connection.InvokeAsync("Broadcast", sender, message);
        }
    }
}
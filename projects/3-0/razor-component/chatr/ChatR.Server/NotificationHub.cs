using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace ChatR.Server
{
    public class NotificationHub : Hub
    {
        ILogger<NotificationHub> _logger;

        public NotificationHub(ILogger<NotificationHub> logger)
        {
            _logger = logger;    
            _logger.LogInformation("NotificationHub Created");
        }

        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation("New Connection from Client");
            await base.OnConnectedAsync();
        }

        public async Task Broadcast(string from, string message)
        {
            _logger.LogInformation("Broadcast");
            await Clients.Caller.SendAsync("BrodcastChannel", from, message);
        }
    }
}
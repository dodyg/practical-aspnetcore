namespace ChatR.App
{
    public class NotificationHub : Hub
    {
        public async Task Broadcast(string from, string message)
        {
            await Clients.Caller.SendAsync("BrodcastChannel", from, message);
        }
    }
}
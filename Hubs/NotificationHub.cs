using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using myapp.Data;
using myapp.Models;

namespace myapp.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly AppDbContext _context;

        public NotificationHub(AppDbContext context)
        {
            _context = context;
        }

        public async Task SendNotificationToAll(string message)
        {
            await Clients.All.SendAsync("ReceivedNotification", message);
        }
        public async Task SendNotificationToClient(string username, string message)
        {
            var hubConnections = _context.HubConnections.Where(w => w.UserName == username).ToListAsync();
            foreach (var hubConnection in await hubConnections)
            {
                await Clients.Client(hubConnection.ConnectionId).SendAsync("ReceivedPersonalNotification", username, message);
            }
        }
        public async Task SendNotificationToGroup(string groups, string message)
        {

            var hubConnections = _context
                .HubConnections
                .Join(
                _context.Users,
                hc => hc.UserName,
                u => u.UserName,
                    (hc, u) => new
                    {
                        hc.UserName,
                        hc.ConnectionId,
                        u.Dept
                    }
                ).Where(u => u.Dept == groups).ToListAsync();

            foreach (var hubConnection in await hubConnections)
            {
                string username = hubConnection.UserName;
                await Clients.Client(hubConnection.ConnectionId).SendAsync("ReceivedPersonalNotification", username, message);
            }
        }
        public async Task SaveUserConnection(string username)
        {
            var connectionId = Context.ConnectionId;
            var hubConnection = new HubConnection
            {
                ConnectionId = connectionId,
                UserName = username,
            };
            await _context.HubConnections.AddAsync(hubConnection);
            await _context.SaveChangesAsync();
        }
        public override async Task OnConnectedAsync()
        {
            // await Clients.All.SendAsync("Connected", $"{Context.ConnectionId} has connected to server !");
            await Clients.Caller.SendAsync("Connected", $"{Context.ConnectionId} has connected to server ");
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            var hubConnection = await _context.HubConnections.FirstOrDefaultAsync(hc => hc.ConnectionId == Context.ConnectionId);
            if (hubConnection != null)
            {
                _context.HubConnections.Remove(hubConnection);
                await _context.SaveChangesAsync();

            }
            await Clients.All.SendAsync("Disconnected", $"{Context.ConnectionId} disconnected !");
            await base.OnDisconnectedAsync(exception);
        }
    }
}

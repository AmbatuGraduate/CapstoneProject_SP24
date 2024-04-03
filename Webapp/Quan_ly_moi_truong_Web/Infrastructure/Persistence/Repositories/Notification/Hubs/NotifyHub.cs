using Domain.Entities.HubConnection;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Notification.Hubs
{
    public class NotifyHub : Hub
    {
        private readonly WebDbContext webDbContext;
        protected IHubContext<NotifyHub> _context;

        public NotifyHub(WebDbContext webDbContext, IHubContext<NotifyHub> context)
        {
            this.webDbContext = webDbContext;
            _context = context;
        }

        //Message to all
        public async Task SendNotificationToAll(string msg)
        {
            Console.WriteLine("checking send to All ");
            await Clients.All.SendAsync("ReceivedNotification", msg);
        }

        //Message to single user -> but 1 user can have many device
        public async Task SendNotificationToSingle(string user, string msg)
        {
            var hubConns = await webDbContext.HubConnections.Where(conn => conn.Username == user).ToListAsync();
            Console.WriteLine("checking send to: " + hubConns.Count);
            foreach (var hubConn in hubConns)
            {
                await _context.Clients.Client(hubConn.ConnectionId).SendAsync("ReceivedPersonalNotification", msg, user);
            }
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("Testing add connection");
            Clients.Caller.SendAsync("OnConnected");
            return base.OnConnectedAsync();
        }

        // Luu hub connection trg Db
        public async Task SaveUserConnection(string user)
        {
            var connectionId = Context.ConnectionId;
            var hub = new HubConnections
            {
                ConnectionId = connectionId,
                Username = user,
            };
            await webDbContext.HubConnections.AddAsync(hub);
            await webDbContext.SaveChangesAsync();
        }

        // Xoa hub connection trg Db moi khi mat ket noi
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var hubConn = webDbContext.HubConnections.FirstOrDefault(conn => conn.ConnectionId == Context.ConnectionId);
            if (hubConn != null)
            {
                webDbContext.HubConnections.Remove(hubConn);
                webDbContext.SaveChanges();
            }
            return base.OnDisconnectedAsync(exception);
        }
    }
}
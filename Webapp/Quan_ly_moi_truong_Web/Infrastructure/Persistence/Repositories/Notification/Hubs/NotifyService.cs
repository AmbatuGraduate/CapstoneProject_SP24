using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Notifiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.Notification.Hubs
{
    public class NotifyService : INotifyService
    {
        private readonly NotifyHub notifyHub;
        public NotifyService(NotifyHub notifyHub) 
        {
            this.notifyHub = notifyHub;
        }

        public async Task SendToAll(string msg)
        {
            await notifyHub.SendNotificationToAll(msg);
        }

        public async Task SendToUser(string user,string msg)
        {
            await notifyHub.SendNotificationToSingle(user, msg);
        }
    }
}

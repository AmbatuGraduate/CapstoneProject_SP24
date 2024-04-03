using Application.Common.Interfaces.Persistence.Notifiy;
using Domain.Entities.Notification;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Notification
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly WebDbContext webDbContext;

        public NotificationRepository(WebDbContext webDbContext)
        {
            this.webDbContext = webDbContext;
        }

        public async Task<Notifications> CreateNotification(Notifications notification)
        {
            await webDbContext.Notifications.AddAsync(notification);
            await webDbContext.SaveChangesAsync();
            return notification;
        }

        public Notifications GetlNotification(Guid Id)
        {
            return webDbContext.Notifications.FirstOrDefault(o => o.Id == Id);
        }

        public async Task<List<Notifications>> GetlNotifications()
        {
            return await webDbContext.Notifications.ToListAsync();
        }

        public async Task<List<Notifications>> GetNotificationsByUseranme(string username, int page)
        {
            int pageSize = 5; 
            return await webDbContext.Notifications
                .Where(x => x.Username == username)
                .OrderByDescending(x => x.NotificationDateTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
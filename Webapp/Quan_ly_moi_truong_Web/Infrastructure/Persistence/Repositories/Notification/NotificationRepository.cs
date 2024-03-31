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

        public Notifications CreateNotification(Notifications notification)
        {
            webDbContext.Notifications.Add(notification);
            webDbContext.SaveChanges();
            return notification;
        }

        public Notifications GetlNotification(Guid Id)
        {
            return webDbContext.Notifications.FirstOrDefault(o => o.Id == Id);
        }

        public List<Notifications> GetlNotifications()
        {
            return webDbContext.Notifications.ToList();
        }

        public async Task<List<Notifications>> GetNotificationsByUseranme(string username)
        {
            return await webDbContext.Notifications.Where(x => x.Username == username).ToListAsync();
        }
    }
}
using Domain.Entities.Notification;

namespace Application.Common.Interfaces.Persistence.Notifiy
{
    public interface INotificationRepository
    {
        Task<List<Notifications>> GetlNotifications();

        Task<List<Notifications>> GetNotificationsByUseranme(string username);

        Notifications GetlNotification(Guid Id);

        Task<Notifications> CreateNotification(Notifications notification);
    }
}
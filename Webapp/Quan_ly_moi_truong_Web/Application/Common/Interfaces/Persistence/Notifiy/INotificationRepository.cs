using Domain.Entities.Notification;

namespace Application.Common.Interfaces.Persistence.Notifiy
{
    public interface INotificationRepository
    {
        Task<List<Notifications>> GetlNotifications();

        Task<List<Notifications>> GetNotificationsByUseranme(string username, int page);

        Notifications GetlNotification(Guid Id);

        Task<Notifications> CreateNotification(Notifications notification);
    }
}
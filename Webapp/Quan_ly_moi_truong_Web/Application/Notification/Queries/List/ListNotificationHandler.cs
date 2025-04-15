using Application.Common.Interfaces.Persistence.Notifiy;
using Application.Notification.Common;
using ErrorOr;
using MediatR;

namespace Application.Notification.Queries.List
{
    public class ListNotificationHandler :
        IRequestHandler<ListNotificationQuery, ErrorOr<List<NotificationResult>>>
    {
        private readonly INotificationRepository _notificationRepository;

        public ListNotificationHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<ErrorOr<List<NotificationResult>>> Handle(ListNotificationQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var listNotification = await _notificationRepository.GetlNotifications();
            var results = new List<NotificationResult>();

            foreach (var notification in listNotification)
            {
                results.Add(new NotificationResult(notification));
            }

            return results;
        }
    }
}
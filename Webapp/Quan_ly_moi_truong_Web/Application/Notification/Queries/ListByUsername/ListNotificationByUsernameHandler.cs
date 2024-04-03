using Application.Common.Interfaces.Persistence.Notifiy;
using Application.Notification.Common;
using ErrorOr;
using MediatR;

namespace Application.Notification.Queries.ListByUsername
{
    public class ListNotificationByUsernameHandler :
        IRequestHandler<ListNotificationByUsernameQuery, ErrorOr<List<NotificationResult>>>
    {
        private readonly INotificationRepository _notificationRepository;

        public ListNotificationByUsernameHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<ErrorOr<List<NotificationResult>>> Handle(ListNotificationByUsernameQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var listNotification = await _notificationRepository.GetNotificationsByUseranme(request.Username, request.Page);
            var results = new List<NotificationResult>();

            foreach (var notification in listNotification)
            {
                results.Add(new NotificationResult(notification));
            }
            return results;
        }
    }
}